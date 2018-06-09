using System;
using TextAdventure_Harold_Mulder;
using System.Collections.Generic;

namespace ZuulCS
{
    public class Game
    {
        private Parser parser;
        private Player player;
        private GeneralDataLibrary GDL;

        public Game()
        {

            GDL = new GeneralDataLibrary();

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
            while (!finished)
            {
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
            GDL.Break();
            Console.WriteLine("Welcome to Zuul!");
            Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
            Console.WriteLine("Type 'help' if you need help.");
            GDL.Break();
            Console.WriteLine(player.CurrentRoom.getLongDescription());
            GDL.Break();
            GDL.LongLine();
            GDL.Break();
        }

        /**
	     * Given a command, process (that is: execute) the command.
	     * If this command ends the game, true is returned, otherwise false is
	     * returned.
	     */
        private bool processCommand(Command command)
        {
            bool wantToQuit = false;

            if (command.isUnknown())
            {
                Console.WriteLine(GDL.I() + "I don't know what you mean...");
                return false;
            }

            string commandWord = command.getCommandWord();
            switch (commandWord)
            {
                case "help":
                    GDL.Break();
                    printHelp(command);
                    GDL.Break();
                    break;
                case "go":
                    GDL.Break();
                    goRoom(command);
                    GDL.Break();

                    GDL.Break();
                    break;
                case "quit":
                    GDL.Break();
                    wantToQuit = true;
                    GDL.Break();
                    GDL.LongLine();
                    GDL.Break();
                    break;
                case "look":
                    GDL.Break();
                    lookAround();
                    GDL.Break();
                    GDL.LongLine();
                    GDL.Break();
                    break;
                case "clear":
                    GDL.Break();
                    ClearConsole(command);
                    GDL.Break();
                    GDL.LongLine();
                    GDL.Break();
                    break;
                case "take":
                    GDL.Break();
                    takeItem(command);
                    GDL.Break();
                    GDL.LongLine();
                    GDL.Break();
                    break;
                case "drop":
                    GDL.Break();
                    dropItem(command);
                    GDL.Break();
                    GDL.LongLine();
                    GDL.Break();
                    break;
                case "bag":
                    GDL.Break();
                    displayBag();
                    GDL.Break();
                    GDL.LongLine();
                    GDL.Break();
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
            if (command.hasSecondWord())
            {

                List<List<string>> commandDescriptions = new List<List<string>>(parser.Commands.CommandDescriptions);
                List<List<string>> commandUsages = new List<List<string>>(parser.Commands.CommandUsages);

                switch (command.getSecondWord())
                {

                    case "help":
                        printCommandHelp(commandDescriptions[0][0], commandUsages[0]);
                        break;
                    case "go":
                        printCommandHelp(commandDescriptions[1][0], commandUsages[1]);
                        break;
                    case "quit":
                        printCommandHelp(commandDescriptions[2][0], commandUsages[2]);
                        break;
                    case "look":
                        printCommandHelp(commandDescriptions[3][0], commandUsages[3]);
                        break;
                    case "clear":
                        printCommandHelp(commandDescriptions[4][0], commandUsages[4]);
                        break;
                    case "take":
                        printCommandHelp(commandDescriptions[5][0], commandUsages[5]);
                        break;
                    case "drop":
                        printCommandHelp(commandDescriptions[6][0], commandUsages[6]);
                        break;
                }

            }
            else
            {
                Console.WriteLine("Your command words are:");
                parser.showCommands();
            }
        }

        private void printCommandHelp(string description, List<string> usage)
        {

            GDL.Break();
            Console.WriteLine(GDL.I() + "Description: ");
            GDL.MidLine(GDL.I());
            GDL.Break();
            Console.WriteLine(GDL.I(2) + description);
            GDL.Break(2);
            Console.WriteLine(GDL.I() +  "Usage: ");
            GDL.MidLine(GDL.I());
            GDL.Break();
            for (int i = 0; i < usage.Count; i++)
            {
                Console.WriteLine(GDL.I(2) + usage[i]);
            }
            GDL.Break(2);
            GDL.LongLine();

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

            }
            else
            {

                Console.WriteLine(GDL.I() + "The clear function does not use a second word!");

            }
        }

        private void goRoom(Command command)
        {
            if (!command.hasSecondWord())
            {
                // if there is no second word, we don't know where to go...
                Console.WriteLine(GDL.I() + "Go where?");
                return;
            }

            string direction = command.getSecondWord();

            // Try to leave current room.
            Room nextRoom = player.CurrentRoom.getExit(direction);

            if (nextRoom == null)
            {
                Console.WriteLine(GDL.I() + "There is no door to " + direction + "!");
            }
            else
            {
                player.CurrentRoom = nextRoom;
                Console.WriteLine(player.CurrentRoom.getLongDescription());
                player.CheckTriggers(0);

            }
        }

        private void takeItem(Command command)
        {

            if (!command.hasSecondWord())
            {

                if (player.CurrentRoom.Inventory.Items.Count < player.Inventory.SpaceLeft)
                {

                    Console.WriteLine(GDL.I() + "You take all items on the ground and put them in your bag.");
                    GDL.Break();
                    for (int i = player.CurrentRoom.Inventory.Items.Count - 1; i >= 0; i--)
                    {

                        Item currentItem = player.CurrentRoom.Inventory.Items[i];
                        Console.WriteLine(GDL.I(2) + "Added " + currentItem.Name);
                        player.CurrentRoom.Inventory.sendItem(player.Inventory, currentItem.Name);
                        player.checkBadItem(currentItem);

                    }


                }
                else
                {

                    Console.WriteLine(GDL.I() + "Not enough space in inventory to take item(s)!");

                }


            }
            else
            {

                if (player.Inventory.SpaceLeft > 0)
                {
                    int num;
                    Item item = null;
                    if (int.TryParse(command.getSecondWord(), out num))
                    {

                        num--;

                        if (num > -1 && num < player.CurrentRoom.Inventory.Items.Count)
                        {

                            item = player.CurrentRoom.Inventory.sendItem(player.Inventory, num);

                        } else
                        {

                            Console.WriteLine(GDL.I() + "index is out of bounds!");

                        }
                    } else {

                        item = player.CurrentRoom.Inventory.sendItem(player.Inventory, command.getSecondWord());

                    }

                    if (item != null)
                    {

                        Console.WriteLine(GDL.I(2) + "Added " + item.Name);
                        player.checkBadItem(item);

                    }


                }
                else
                {

                    Console.WriteLine(GDL.I() + "Not enough space in inventory to take item!");

                }

            }

        }

        private void dropItem(Command command)
        {

            if (!command.hasSecondWord())
            {

                Console.WriteLine(GDL.I() + "drop what?");

            }
            else
            {
                int num;
                Item item = null;
                if (int.TryParse(command.getSecondWord(), out num))
                {

                    num--;

                    if (num > -1 && num < player.Inventory.Items.Count)
                    {

                        item = player.Inventory.sendItem(player.CurrentRoom.Inventory, num);

                    }
                    else
                    {

                        Console.WriteLine(GDL.I() + "index is out of bounds!");

                    }

                } else
                {

                    item = player.Inventory.sendItem(player.CurrentRoom.Inventory, command.getSecondWord());

                }
                    if (item != null)
                {

                        Console.WriteLine(GDL.I(2) + "Dropped " + item.Name);

                }

            }

        }

        private void lookAround()
        {

            Console.WriteLine(player.CurrentRoom.getLongDescription());
            GDL.Break();
            Console.WriteLine(GDL.I() + player.CurrentRoom.Inventory.Items.Count + " Item(s) in the room!");
            GDL.Break();
            for (int i = 0; i < player.CurrentRoom.Inventory.Items.Count; i++)
            {

                Console.WriteLine(GDL.I(2) + (i + 1) + " | " + player.CurrentRoom.Inventory.Items[i].Name + ": " + player.CurrentRoom.Inventory.Items[i].Description);

            }
            GDL.Break(2);
            Console.WriteLine(GDL.I() + "health: " + player.Health.ToString());
            Console.WriteLine(GDL.I() + "room in bag: " + player.Inventory.SpaceLeft);

        }

        public void displayBag()
        {

            int iC = player.Inventory.Items.Count;
            string str = "";

            if (iC > 1 || iC <= 0)
            {
                str = "s";
            }
            Console.WriteLine(GDL.I() + iC + " Item" + str + " found in bag.");
            GDL.ShortLine(GDL.I());
            GDL.Break();
            for (int i = 0; i < iC; i++)
            {

                Console.WriteLine(GDL.I(2) + (i+1) + ": " + player.Inventory.Items[i].Name);

            }
            GDL.Break();
            Console.WriteLine(GDL.I() + player.Inventory.SpaceLeft + " space left in bag.");
            GDL.Break();
        }
    }
}
