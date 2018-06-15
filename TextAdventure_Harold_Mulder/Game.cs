using System;
using TextAdventure_Harold_Mulder;
using System.Collections.Generic;

namespace ZuulCS
{
    public class Game
    {
        private Parser parser;
        private Player player;

        public Game()
        {

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

            Room destroyedTower, mudCreek1, mudCreek2, mudCreekSide1;

            
           
            // create the rooms
            destroyedTower = new Room("in the remains of an old watchtower");
            mudCreek1 = new Room("in a hidden cavern under the old watch tower");
            mudCreekSide1 = new Room("in a small back area hidden at the side of the cave");
            mudCreek2 = new Room("in a deeper part of the muddy cave");

            // initialise room exits
            destroyedTower.setExit("down", mudCreek1, "As you jump down into the muddy cave below you realise you won't be able to get up again!");

            mudCreek1.setExit("north", mudCreekSide1);
            mudCreek1.setExit("east", mudCreek2);

            mudCreekSide1.setExit("south", mudCreek1);

            mudCreek2.setExit("west", mudCreek1);

            // Set locked states
            mudCreekSide1.setBarred("A pile of rocks block the entrance");

            // Set room items
            destroyedTower.Inventory.addItem(new Item());

            player.CurrentRoom = destroyedTower;  // start game outside
            //theatre.Inventory.addItem(new CursedCrystal());

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
            GeneralDataLibrary.Break();
            Console.WriteLine("Welcome to Dynasty!");
            Console.WriteLine("You wake up with a headache looking around you seem to be in a tower of sorts.");
            Console.WriteLine("Type 'help' if you need help.");
            GeneralDataLibrary.Break();
            Console.WriteLine(player.CurrentRoom.getLongDescription());
            GeneralDataLibrary.Break();
            GeneralDataLibrary.LongLine();
            GeneralDataLibrary.Break();
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
                Console.WriteLine(GeneralDataLibrary.I() + "I don't know what you mean...");
                Console.WriteLine(GeneralDataLibrary.I() + "Type: 'help' if you need help.");
                return false;
            }

            string commandWord = command.getCommandWord();
            switch (commandWord)
            {
                case "help":
                    GeneralDataLibrary.Break();
                    printHelp(command);
                    GeneralDataLibrary.Break();
                    break;
                case "go":
                    GeneralDataLibrary.Break();
                    goRoom(command);
                    GeneralDataLibrary.Break(2);
                    break;
                case "quit":
                    GeneralDataLibrary.Break();
                    wantToQuit = true;
                    GeneralDataLibrary.Break();
                    GeneralDataLibrary.LongLine();
                    GeneralDataLibrary.Break();
                    break;
                case "look":
                    GeneralDataLibrary.Break();
                    lookAround();
                    GeneralDataLibrary.Break();
                    GeneralDataLibrary.LongLine();
                    GeneralDataLibrary.Break();
                    break;
                case "clear":
                    GeneralDataLibrary.Break();
                    ClearConsole(command);
                    GeneralDataLibrary.Break();
                    GeneralDataLibrary.LongLine();
                    GeneralDataLibrary.Break();
                    break;
                case "take":
                    GeneralDataLibrary.Break();
                    takeItem(command);
                    GeneralDataLibrary.Break();
                    GeneralDataLibrary.LongLine();
                    GeneralDataLibrary.Break();
                    break;
                case "drop":
                    GeneralDataLibrary.Break();
                    dropItem(command);
                    GeneralDataLibrary.Break();
                    GeneralDataLibrary.LongLine();
                    GeneralDataLibrary.Break();
                    break;
                case "bag":
                    GeneralDataLibrary.Break();
                    displayBag();
                    GeneralDataLibrary.Break();
                    GeneralDataLibrary.LongLine();
                    GeneralDataLibrary.Break();
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

                if(parser.Commands.CommandData.ContainsKey(command.getSecondWord()))
                {

                    Console.Write(parser.Commands.CommandData[command.getSecondWord()]);
                    GeneralDataLibrary.Break(2);
                    GeneralDataLibrary.LongLine();

                }

            }
            else
            {

                Console.WriteLine("Type: 'help <command>' to show more information on a command.");
                Console.WriteLine("Your command words are:");
                GeneralDataLibrary.Break();
                parser.showCommands();
                GeneralDataLibrary.Break();
                
            }
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

                Console.WriteLine(GeneralDataLibrary.I() + "The clear function does not use a second word!");

            }
        }

        private void goRoom(Command command)
        {
            if (!command.hasSecondWord())
            {
                // if there is no second word, we don't know where to go...
                Console.WriteLine(GeneralDataLibrary.I() + "Go where?");
                return;
            }

            string direction = command.getSecondWord();

            // Try to leave current room.
            Room nextRoom = player.CurrentRoom.getExit(direction);

            if (nextRoom == null)
            {
                Console.WriteLine(GeneralDataLibrary.I() + "You can not go there '" + direction + "' doesn't exist!");
            }
            else
            {

                if (nextRoom.IsTutorialLocked)
                {

                    Console.WriteLine(GeneralDataLibrary.I() + nextRoom.TutorialDescription);

                }
                else if (nextRoom.IsBarred)
                {

                    Console.WriteLine(GeneralDataLibrary.I() + nextRoom.BarredDescription);

                }
                else if (nextRoom.IsCutable)
                {

                    Console.WriteLine(GeneralDataLibrary.I() + nextRoom.CutableDescription);

                }
                else if (nextRoom.IsLocked)
                {

                    Console.WriteLine(GeneralDataLibrary.I() + "You will need a '" + nextRoom.KeyToUnlock + "' key!");

                }
                else
                {
                    if (player.CurrentRoom.ExitEvents.ContainsKey(direction))
                    {

                        Console.WriteLine(player.CurrentRoom.ExitEvents[direction]);
                        GeneralDataLibrary.Break();

                    }

                        player.CurrentRoom = nextRoom;
                        Console.WriteLine(player.CurrentRoom.getLongDescription());
                        player.CheckTriggers(0);
                }


            }
        }

        private void takeItem(Command command)
        {

            if (!command.hasSecondWord())
            {

                if (player.CurrentRoom.Inventory.Items.Count < player.Inventory.SpaceLeft)
                {

                    if (player.CurrentRoom.Inventory.Items.Count > 0)
                    {

                        Console.WriteLine(GeneralDataLibrary.I() + "You take all items on the ground and put them in your bag.");
                        GeneralDataLibrary.Break();
                        for (int i = player.CurrentRoom.Inventory.Items.Count - 1; i >= 0; i--)
                        {

                            Item currentItem = player.CurrentRoom.Inventory.Items[i];
                            Console.WriteLine(GeneralDataLibrary.I(2) + "Added " + currentItem.Name);
                            player.CurrentRoom.Inventory.sendItem(player.Inventory, currentItem.Name);
                            if (player.checkBadItem(currentItem, 0))
                            {

                                player.Inventory.Items.RemoveAt(i);
                                currentItem = null;

                            }

                        }

                    }
                    else
                    {

                        Console.WriteLine(GeneralDataLibrary.I() + "Despite your hard work you find no item to pick up!");

                    }

                }
                else
                {

                    Console.WriteLine(GeneralDataLibrary.I() + "Not enough space in inventory to take item(s)!");

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

                            Console.WriteLine(GeneralDataLibrary.I() + "index is out of bounds!");

                        }
                    } else {

                        item = player.CurrentRoom.Inventory.sendItem(player.Inventory, command.getSecondWord());

                    }

                    if (item != null)
                    {

                        Console.WriteLine(GeneralDataLibrary.I(2) + "Added " + item.Name);
                        player.checkBadItem(item, 0);

                    }


                }
                else
                {

                    Console.WriteLine(GeneralDataLibrary.I() + "Not enough space in inventory to take item!");

                }

            }

        }

        private void dropItem(Command command)
        {

            if (!command.hasSecondWord())
            {

                Console.WriteLine(GeneralDataLibrary.I() + "drop what?");

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

                        Console.WriteLine(GeneralDataLibrary.I() + "index is out of bounds!");

                    }

                } else
                {

                    item = player.Inventory.sendItem(player.CurrentRoom.Inventory, command.getSecondWord());

                }
                    if (item != null)
                {

                        Console.WriteLine(GeneralDataLibrary.I(2) + "Dropped " + item.Name);

                }

            }

        }

        private void lookAround()
        {

            Console.WriteLine(player.CurrentRoom.getLongDescription());
            GeneralDataLibrary.Break();
            if(player.CurrentRoom.Inventory.Items.Count > 0) {
                Console.WriteLine(GeneralDataLibrary.I() + player.CurrentRoom.Inventory.Items.Count + " Item(s) in the room!");
                GeneralDataLibrary.Break();
                for (int i = 0; i < player.CurrentRoom.Inventory.Items.Count; i++)
                {

                    Console.WriteLine(GeneralDataLibrary.I(2) + (i + 1) + " | " + player.CurrentRoom.Inventory.Items[i].Name + ": " + player.CurrentRoom.Inventory.Items[i].Description);

                }
                GeneralDataLibrary.Break(2);
            }
            Console.WriteLine(GeneralDataLibrary.I() + "health: " + player.Health.ToString());
            Console.WriteLine(GeneralDataLibrary.I() + "room in bag: " + player.Inventory.SpaceLeft);

        }

        public void displayBag()
        {

            int iC = player.Inventory.Items.Count;
            string str = "";

            if (iC > 1 || iC <= 0)
            {
                str = "s";
            }
            Console.WriteLine(GeneralDataLibrary.I() + iC + " Item" + str + " found in bag.");
            GeneralDataLibrary.ShortLine(GeneralDataLibrary.I());
            GeneralDataLibrary.Break();
            for (int i = 0; i < iC; i++)
            {

                Console.WriteLine(GeneralDataLibrary.I(2) + (i+1) + ": " + player.Inventory.Items[i].Name);

            }
            GeneralDataLibrary.Break();
            Console.WriteLine(GeneralDataLibrary.I() + player.Inventory.SpaceLeft + " space left in bag.");
            GeneralDataLibrary.Break();
        }
    }
}
