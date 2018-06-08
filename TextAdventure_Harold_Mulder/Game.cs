using System;
using TextAdventure_Harold_Mulder;
using System.Collections.Generic;

namespace ZuulCS
{
	public class Game
	{
		private Parser parser;
        private Player player;
        private string indent;
        
        public Game ()
		{

            indent = "    ";

            parser = new Parser();
            player = new Player();
            createRooms();

            //i1 = new Inventory(5);
            //i2 = new Inventory(5);
            //item = new Item();

            //i1.addItem(item);
            //i1.sendItem(i2, "item");

            //Console.WriteLine(i1.Items.Count);
            //Console.WriteLine(i2.Items.Count);
        }
        /**
         * Creates all the rooms used in the game
         */
        private void createRooms()

        {
            Room outside, theatre, pub, lab, office, bunker;

            // create the rooms
            outside = new Room("outside the main entrance of the university");
            theatre = new Room("in a lecture theatre");
            pub = new Room("in the campus pub");
            lab = new Room("in a computing lab");
            office = new Room("in the computing admin office");
            bunker = new Room("A secret bunker hidden under the school");

            // initialise room exits
            outside.setExit("east", theatre);
            outside.setExit("south", lab);
            outside.setExit("west", pub);

            theatre.setExit("west", outside);

            pub.setExit("east", outside);

            lab.setExit("north", outside);
            lab.setExit("east", office);
            lab.setExit("down", bunker);

            bunker.setExit("up", lab);

            office.setExit("west", lab);

            player.CurrentRoom = outside;  // start game outside
            theatre.Inventory.addItem(new CursedCrystal());

        }
        /**
	     *  Main play routine.  Loops until end of play.
	     */
        public void play()
		{
			printWelcome();

			// Enter the main command loop.  Here we repeatedly read commands and
			// execute them until the game is over.
			bool finished = false;
			while (! finished) {
				Command command = parser.getCommand();
				finished = processCommand(command);
			}
			Console.WriteLine("Thank you for playing.");
		}

		/**
	     * Print out the opening message for the player.
	     */
		private void printWelcome()
		{
			Console.WriteLine();
			Console.WriteLine("Welcome to Zuul!");
			Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
			Console.WriteLine("Type 'help' if you need help.");
			Console.WriteLine();
			Console.WriteLine(player.CurrentRoom.getLongDescription());
            Console.WriteLine();
            Console.WriteLine("______________________________________________________________________________");
            Console.WriteLine();
        }

		/**
	     * Given a command, process (that is: execute) the command.
	     * If this command ends the game, true is returned, otherwise false is
	     * returned.
	     */
		private bool processCommand(Command command)
		{
			bool wantToQuit = false;

			if(command.isUnknown()) {
				Console.WriteLine(indent + "I don't know what you mean...");
				return false;
			}

			string commandWord = command.getCommandWord();
			switch (commandWord) {
				case "help":
                    Console.WriteLine();
                    printHelp(command);
                    Console.WriteLine();
                    Console.WriteLine("______________________________________________________________________________");
                    Console.WriteLine();
                    break;
				case "go":
                    Console.WriteLine();
                    goRoom(command);
                    Console.WriteLine();
                    Console.WriteLine("______________________________________________________________________________");
                    Console.WriteLine();
                    break;
				case "quit":
                    Console.WriteLine();
                    wantToQuit = true;
                    Console.WriteLine();
                    Console.WriteLine("______________________________________________________________________________");
                    Console.WriteLine();
                    break;
                case "look":
                    Console.WriteLine();
                    lookAround();
                    Console.WriteLine();
                    Console.WriteLine("______________________________________________________________________________");
                    Console.WriteLine();
                    break;
                case "yomom":
                    Console.WriteLine();
                    Console.WriteLine(indent + "no u");
                    Console.WriteLine();
                    Console.WriteLine("______________________________________________________________________________");
                    Console.WriteLine();
                    break;
                case "clear":
                    Console.WriteLine();
                    ClearConsole(command);
                    Console.WriteLine();
                    Console.WriteLine("______________________________________________________________________________");
                    Console.WriteLine();
                    break;
                case "take":
                    Console.WriteLine();
                    takeItem(command);
                    Console.WriteLine();
                    Console.WriteLine("______________________________________________________________________________");
                    Console.WriteLine();
                    break;
                case "drop":
                    Console.WriteLine();
                    dropItem(command);
                    Console.WriteLine();
                    Console.WriteLine("______________________________________________________________________________");
                    Console.WriteLine();
                    break;
			}

			return wantToQuit;
		}

		// implementations of user commands:

		/**
	     * Print out some help information.
	     * Here we print some stupid, cryptic message and a list of the
	     * command words.
	     */
		private void printHelp(Command command)
		{
			Console.WriteLine("You are lost. You are alone.");
			Console.WriteLine("You wander around at the university.");
			Console.WriteLine();
            Console.WriteLine("Your command words are:");
            parser.showCommands();
		}

		/**
	     * Try to go to one direction. If there is an exit, enter the new
	     * room, otherwise print an error message.
	     */

        private void ClearConsole(Command command)
        {

            if (!command.hasSecondWord())
            {

                Console.Clear();
                Console.WriteLine(player.CurrentRoom.getLongDescription());

            } else
            {

                Console.WriteLine(indent + "The clear function does not use a second word!");

            }
        }

		private void goRoom(Command command)
		{
			if(!command.hasSecondWord()) {
				// if there is no second word, we don't know where to go...
				Console.WriteLine(indent + "Go where?");
				return;
			}

			string direction = command.getSecondWord();

			// Try to leave current room.
			Room nextRoom = player.CurrentRoom.getExit(direction);

			if (nextRoom == null) {
				Console.WriteLine(indent + "There is no door to " + direction+"!");
			} else {
                player.CurrentRoom = nextRoom;
				Console.WriteLine(player.CurrentRoom.getLongDescription());
                player.CheckTriggers(0);
                
			}
		}

        private void takeItem(Command command)
        {

            if (!command.hasSecondWord())
            {

                if(player.CurrentRoom.Inventory.Items.Count < player.Inventory.SpaceLeft)
                {

                    Console.WriteLine(indent + "You take all items on the ground and put them in your bag.");
                    Console.WriteLine();
                    for (int i = player.CurrentRoom.Inventory.Items.Count-1; i >= 0; i --)
                    {

                        Item currentItem = player.CurrentRoom.Inventory.Items[i];
                        Console.WriteLine(indent + indent + "Added " + currentItem.Name);
                        player.CurrentRoom.Inventory.sendItem(player.Inventory, currentItem.Name);
                        player.checkBadItem(currentItem);

                    }
                    

                } else
                {

                    Console.WriteLine(indent + "Not enough space in inventory to take item(s)!");

                }


            } else
            {
                
                if(player.Inventory.SpaceLeft > 0)
                {

                    Item item = player.CurrentRoom.Inventory.sendItem(player.Inventory, command.getSecondWord());

                    if (item != null)
                    {

                        Console.WriteLine(indent + indent + "Added " + command.getSecondWord());
                        player.checkBadItem(item);

                    }

                    
                } else
                {

                    Console.WriteLine(indent + "Not enough space in inventory to take item!");

                }

            }

        }

        private void dropItem(Command command)
        {

            if (!command.hasSecondWord())
            {

                Console.WriteLine(indent + "drop what?");

            } else
            {

                if(player.Inventory.sendItem(player.CurrentRoom.Inventory, command.getSecondWord()) != null )
                {

                    Console.WriteLine(indent + indent + "Dropped " + command.getSecondWord());

                }
                
            }

        }

        private void lookAround()
        {

            Console.WriteLine(player.CurrentRoom.getLongDescription());
            Console.WriteLine();
            Console.WriteLine(indent + player.CurrentRoom.Inventory.Items.Count + " Item(s) in the room!");
            Console.WriteLine();
            for (int i = 0; i < player.CurrentRoom.Inventory.Items.Count; i++)
            {

                Console.WriteLine(indent + indent + (i + 1) + " | " + player.CurrentRoom.Inventory.Items[i].Name + ": " + player.CurrentRoom.Inventory.Items[i].Description);

            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(indent + "health: " + player.Health.ToString());
            Console.WriteLine(indent + "room in bag: " + player.Inventory.SpaceLeft);

        }

	}
}
