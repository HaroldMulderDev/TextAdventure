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
            mudCreekSide1.setBarred("A pile of rocks block the entrance", "The rock formation shatters to pieces!");

            mudCreek2.setTutorialLock("I should probably get a weapon first!");

            // Set room items
            destroyedTower.Inventory.addItem(new Rock());
            destroyedTower.Inventory.addItem(new Apple());

            // Mudcreek1
            Weapon sword = new TutorialSword();
            sword.setPickupTutorialUnlock(mudCreek2);
            mudCreek1.Inventory.addItem(sword);

            //mudcreekside1
            mudCreekSide1.Inventory.addItem(new Apple());
            mudCreekSide1.Inventory.addItem(new Apple());
            mudCreekSide1.Inventory.addItem(new WoodArmor());

            // set starting room
            player.CurrentRoom = destroyedTower;  // start game outside

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

            GeneralDataLibrary.Break();

            switch (commandWord)
            {
                case "help":
                    
                    printHelp(command);

                    break;
                case "go":

                    goRoom(command);

                    break;
                case "quit":

                    wantToQuit = true;
                    
                    break;
                case "look":

                    lookAround(command);

                    break;
                case "clear":

                    ClearConsole(command);

                    break;
                case "take":

                    takeItem(command);

                    break;
                case "drop":

                    dropItem(command);

                    break;
                case "bag":

                    displayBag();

                    break;
                case "use":

                    useItem(command);

                    break;
                case "equip":

                    equipItem(command);

                    break;
                case "unequip":

                    unequipItem(command);

                    break;

                case "attack":

                    attackEnemy(command);

                    break;
                
            }

            GeneralDataLibrary.Break();
            GeneralDataLibrary.LongLine();
            GeneralDataLibrary.Break();

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

                    Console.WriteLine(nextRoom.TutorialDescription);

                }
                else if (nextRoom.IsBarred)
                {

                    Console.WriteLine(nextRoom.BarredDescription);

                }
                else if (nextRoom.IsCutable)
                {

                    Console.WriteLine(nextRoom.CutableDescription);

                }
                else if (nextRoom.IsLocked)
                {

                    Console.WriteLine("You will need a '" + nextRoom.KeyToUnlock + "' key!");

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

                            if (currentItem.HasPickupTutorialEvent)
                            {

                                currentItem.progressTutorial();

                            }

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

                        if (item.HasPickupTutorialEvent)
                        {

                            item.progressTutorial();

                        }

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

        private void lookAround(Command command)
        {
            if (command.hasSecondWord())
            {

                bool showDesc = true;

                if (player.CurrentRoom.getExit(command.getSecondWord()) != null)
                {
                    if (player.CurrentRoom.getExit(command.getSecondWord()).IsBarred)
                    {

                        Console.WriteLine(player.CurrentRoom.getExit(command.getSecondWord()).BarredDescription);
                        showDesc = false;

                    }

                    if (player.CurrentRoom.getExit(command.getSecondWord()).IsCutable)
                    {

                        Console.WriteLine(player.CurrentRoom.getExit(command.getSecondWord()).CutableDescription);
                        showDesc = false;

                    }

                    if (player.CurrentRoom.getExit(command.getSecondWord()).IsLocked)
                    {

                        Console.WriteLine(GeneralDataLibrary.I() + "This room is locked!");
                        Console.WriteLine(player.CurrentRoom.getExit(command.getSecondWord()).KeyToUnlock);
                        showDesc = false;

                    }

                    if (player.CurrentRoom.getExit(command.getSecondWord()).IsTutorialLocked)
                    {

                        Console.WriteLine(player.CurrentRoom.getExit(command.getSecondWord()).TutorialDescription);
                        showDesc = false;

                    }

                    if (showDesc)
                    {

                        GeneralDataLibrary.Break();
                        Console.WriteLine(GeneralDataLibrary.I() + "There is nothing blocking the entrance to " + command.getSecondWord());

                    }
                }
                else
                {

                    Console.WriteLine(GeneralDataLibrary.Note() + "Could not find exit!");

                }

            }
            else
            {
                Console.WriteLine(GeneralDataLibrary.I() + player.CurrentRoom.getLongDescription());
                GeneralDataLibrary.Break();
                if (player.CurrentRoom.Inventory.Items.Count > 0)
                {
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
            if (iC > 0) {
                GeneralDataLibrary.ShortLine(GeneralDataLibrary.I());
                GeneralDataLibrary.Break();
                for (int i = 0; i < iC; i++)
                {

                    Console.WriteLine(GeneralDataLibrary.I(2) + (i + 1) + ": " + player.Inventory.Items[i].Name);

                }
            }
            GeneralDataLibrary.Break();
            Console.WriteLine(GeneralDataLibrary.I() + player.Inventory.SpaceLeft + " space left in bag.");
            GeneralDataLibrary.Break();

            if (player.FirstHand != null)
            {

                Console.WriteLine(GeneralDataLibrary.I() + "hand: " + player.FirstHand.Name + " - " + player.FirstHand.Description);

            }
            else
            {

                Console.WriteLine(GeneralDataLibrary.I() + "hand: " + "<empty>");

            }

            if (player.SecondHand != null)
            {

                Console.WriteLine(GeneralDataLibrary.I() + "offhand: " + player.SecondHand.Name + " - " + player.SecondHand.Description);

            }
            else
            {

                Console.WriteLine(GeneralDataLibrary.I() + "offhand: " + "<empty>");

            }

            if (player.Armor != null)
            {

                Console.WriteLine(GeneralDataLibrary.I() + "armor: " + player.Armor.Name + " - " + player.Armor.Description);

            }
            else
            {

                Console.WriteLine(GeneralDataLibrary.I() + "armor: " + "<empty>");

            }

            if (player.Special != null)
            {

                Console.WriteLine(GeneralDataLibrary.I() + "special: " + player.Special.Name + " - " + player.Special.Description);

            }
            else
            {

                Console.WriteLine(GeneralDataLibrary.I() + "special: " + "<empty>");

            }

        }

        public void useItem(Command command)
        {

            Item i = null;

            switch (command.getSecondWord())
            {

                case "hand":
                    i = player.FirstHand;
                    break;
                case "offhand":
                    i = player.SecondHand;
                    break;
                default:
                    int num;
                    if (int.TryParse(command.getSecondWord(), out num))
                    {

                        if (num > 0 && num < player.Inventory.Items.Count - 1)
                        {

                            i = player.Inventory.Items[num];

                        }

                    }
                    else
                    {

                        for (int ii = player.Inventory.Items.Count - 1; ii >= 0; ii--)
                        {

                            if (player.Inventory.Items[ii].Name == command.getSecondWord())
                            {

                                i = player.Inventory.Items[ii];

                            }

                        }

                    }
                    break;
                

            }

            if (i == null)
            {

                Console.WriteLine("No item found on index, name or slot!");
                return;

            }

            else
            {
                if (command.hasThirdWord())
                {
                    if (player.CurrentRoom.getExit(command.getThirdWord()) != null)
                    {

                        if (i.use(player.CurrentRoom.getExit(command.getThirdWord())))
                        {

                            destroyItem(command.getSecondWord());

                        }

                    } else
                    {

                        Console.WriteLine(GeneralDataLibrary.Note() + "That exit doesn't exist!");

                    }
                }
                else
                {

                    if (i.use(player))
                    {

                        destroyItem(command.getSecondWord());

                    }

                }

            }

        }

        public void destroyItem(string item)
        {

            switch (item)
            {

                case "hand":
                    player.FirstHand = null;
                    break;
                case "offhand":
                    player.SecondHand = null;
                    break;
                default:
                    int num;
                    if (int.TryParse(item, out num))
                    {

                        player.Inventory.Items.RemoveAt(num);

                    }
                    else
                    {

                        for (int ii = player.Inventory.Items.Count - 1; ii >= 0; ii--)
                        {

                            if (player.Inventory.Items[ii].Name == item)
                            {

                                player.Inventory.Items.RemoveAt(ii);

                            }

                        }

                    }
                    break;

            }

        }

        public void equipItem(Command command)
        {
            int num;

            if (command.hasSecondWord())
            {
                if (command.hasThirdWord())
                {

                    if (int.TryParse(command.getSecondWord(), out num))
                    {

                        num++;

                        if (num > 0 && num < player.Inventory.Items.Count)
                        {

                            Item i = player.Inventory.Items[num];

                            switch (command.getThirdWord())
                            {

                                case "hand":

                                    if (i is Armor || i is Special)
                                    {

                                        Console.WriteLine("You cannot equip armor or special items to your hand!");

                                    }
                                    else
                                    {

                                        if (player.FirstHand == null)
                                        {

                                            player.Inventory.Items.RemoveAt(num);
                                            player.FirstHand = i;
                                            Console.WriteLine("Equipped: " + i.Name + " to hand.");

                                        }
                                        else
                                        {

                                            Item ii = player.FirstHand;
                                            player.Inventory.Items.RemoveAt(num);
                                            player.Inventory.addItem(ii);
                                            player.FirstHand = i;
                                            Console.WriteLine("Unequiped: " + ii.Name + " from hand.");
                                            Console.WriteLine("Equipped: " + i.Name + " to hand.");

                                        }

                                    }

                                    break;

                                case "offhand":

                                    if (i is Armor || i is Special)
                                    {

                                        Console.WriteLine("You cannot equip armor or special items to your second hand!");

                                    }
                                    else
                                    {

                                        if (player.FirstHand == null)
                                        {

                                            player.Inventory.Items.RemoveAt(num);
                                            player.SecondHand = i;
                                            Console.WriteLine("Equipped: " + i.Name + " to second hand.");

                                        }
                                        else
                                        {

                                            Item ii = player.SecondHand;
                                            player.Inventory.Items.RemoveAt(num);
                                            player.Inventory.addItem(ii);
                                            player.SecondHand = i;
                                            Console.WriteLine("Unequiped: " + ii.Name + " from second hand.");
                                            Console.WriteLine("Equipped: " + i.Name + " to second hand.");

                                        }

                                    }

                                    break;

                                case "armor":

                                    if (i is Armor)
                                    {

                                        if (player.Armor == null)
                                        {

                                            player.Inventory.Items.RemoveAt(num);
                                            player.Special = i;
                                            Console.WriteLine("Equipped: " + i.Name + " to armor slot.");

                                        }
                                        else
                                        {

                                            Item ii = player.Armor;
                                            player.Inventory.Items.RemoveAt(num);
                                            player.Inventory.addItem(ii);
                                            player.Special = i;
                                            Console.WriteLine("Unequiped: " + ii.Name + " from armor slot.");
                                            Console.WriteLine("Equipped: " + i.Name + " to armor slot.");


                                        }

                                    }
                                    else
                                    {

                                        Console.WriteLine("You cannot equip anything but armors to the armor slot.");

                                    }

                                    break;

                                case "special":

                                    if (i is Special)
                                    {

                                        if (player.Special == null)
                                        {

                                            player.Inventory.Items.RemoveAt(num);
                                            player.Armor = i;
                                            Console.WriteLine("Equipped: " + i.Name + " to special slot.");

                                        }
                                        else
                                        {

                                            Item ii = player.Special;
                                            player.Inventory.Items.RemoveAt(num);
                                            player.Inventory.addItem(ii);
                                            player.Armor = i;
                                            Console.WriteLine("Unequiped: " + ii.Name + " from special slot.");
                                            Console.WriteLine("Equipped: " + i.Name + " to special slot.");

                                        }

                                    }
                                    else
                                    {

                                        Console.WriteLine("You can only equip specials to this slot");

                                    }

                                    break;

                                default:
                                    Console.WriteLine("That is not an equip slot.");
                                    break;
                            }



                        }

                    }
                    else
                    {

                        for (int i = player.Inventory.Items.Count-1; i >= 0; i--)
                        {

                            Item ii = player.Inventory.Items[i];

                            if (ii.Name == command.getSecondWord())
                            {

                                switch (command.getThirdWord())
                                {

                                    case "hand":

                                        if (ii is Armor || ii is Special)
                                        {

                                            Console.WriteLine("You cannot equip armor or special items to your hand!");

                                        }
                                        else
                                        {

                                            if (player.FirstHand == null)
                                            {

                                                player.Inventory.Items.RemoveAt(i);
                                                player.FirstHand = ii;
                                                Console.WriteLine("Equipped: " + ii.Name + " to hand.");

                                            }
                                            else
                                            {

                                                Item ir = player.FirstHand;
                                                player.Inventory.Items.RemoveAt(i);
                                                player.Inventory.addItem(ir);
                                                player.FirstHand = ii;
                                                Console.WriteLine("Unequiped: " + ir.Name + " from hand.");
                                                Console.WriteLine("Equipped: " + ii.Name + " to hand.");

                                            }

                                        }

                                        break;

                                    case "offhand":

                                        if (ii is Armor || ii is Special)
                                        {

                                            Console.WriteLine("You cannot equip armor or special items to your second hand!");

                                        }
                                        else
                                        {

                                            if (player.FirstHand == null)
                                            {

                                                player.Inventory.Items.RemoveAt(i);
                                                player.SecondHand = ii;
                                                Console.WriteLine("Equipped: " + ii.Name + " to second hand.");

                                            }
                                            else
                                            {

                                                Item ir = player.SecondHand;
                                                player.Inventory.Items.RemoveAt(i);
                                                player.Inventory.addItem(ir);
                                                player.SecondHand = ii;
                                                Console.WriteLine("Unequiped: " + ir.Name + " from second hand.");
                                                Console.WriteLine("Equipped: " + ir.Name + " to second hand.");

                                            }

                                        }

                                        break;

                                    case "armor":

                                        if (ii is Armor)
                                        {

                                            if (player.Armor == null)
                                            {

                                                player.Inventory.Items.RemoveAt(i);
                                                player.Special = ii;
                                                Console.WriteLine("Equipped: " + ii.Name + " to armor slot.");

                                            }
                                            else
                                            {

                                                Item ir = player.Armor;
                                                player.Inventory.Items.RemoveAt(i);
                                                player.Inventory.addItem(ir);
                                                player.Special = ii;
                                                Console.WriteLine("Unequiped: " + ir.Name + " from armor slot.");
                                                Console.WriteLine("Equipped: " + ir.Name + " to armor slot.");


                                            }

                                        }
                                        else
                                        {

                                            Console.WriteLine("You cannot equip anything but armors to the armor slot.");

                                        }

                                        break;

                                    case "special":

                                        if (ii is Special)
                                        {

                                            if (player.Special == null)
                                            {

                                                player.Inventory.Items.RemoveAt(i);
                                                player.Armor = ii;
                                                Console.WriteLine("Equipped: " + ii.Name + " to special slot.");

                                            }
                                            else
                                            {

                                                Item ir = player.Special;
                                                player.Inventory.Items.RemoveAt(i);
                                                player.Inventory.addItem(ir);
                                                player.Armor = ii;
                                                Console.WriteLine("Unequiped: " + ir.Name + " from special slot.");
                                                Console.WriteLine("Equipped: " + ir.Name + " to special slot.");

                                            }

                                        }
                                        else
                                        {

                                            Console.WriteLine("You can only equip specials to this slot");

                                        }

                                        break;

                                    default:
                                        Console.WriteLine("That is not an equip slot.");
                                        break;

                                }

                            }

                        }

                    }

                }
                else
                {

                    for (int i = player.Inventory.Items.Count - 1; i >= 0; i--)
                    {

                        Item ii = player.Inventory.Items[i];

                        if (ii.Name == command.getSecondWord())
                        {

                            if (ii is Armor || ii is Special)
                            {

                                Console.WriteLine("You cannot equip armor or special items to your hand!");

                            }
                            else
                            {

                                if (player.FirstHand == null)
                                {

                                    player.Inventory.Items.RemoveAt(i);
                                    player.FirstHand = ii;
                                    Console.WriteLine("Equipped: " + ii.Name + " to hand.");

                                }
                                else
                                {

                                    Item ir = player.FirstHand;
                                    player.Inventory.Items.RemoveAt(i);
                                    player.Inventory.addItem(ir);
                                    player.FirstHand = ii;
                                    Console.WriteLine("Unequiped: " + ir.Name + " from hand.");
                                    Console.WriteLine("Equipped: " + ii.Name + " to hand.");

                                }

                            }

                        }
                    }
                }

            }
            else
            {

                Console.WriteLine("No item to equip given.");

            }

        }

        public void unequipItem(Command command)
        {

            if (command.hasSecondWord())
            {

                switch (command.getSecondWord())
                {

                    case "hand":
                        if(player.FirstHand != null)
                        {

                            if (player.Inventory.addItem(player.FirstHand))
                            {

                                Console.WriteLine("Unequiped: " + player.FirstHand.Name);
                                player.FirstHand = null;

                            }

                        }
                        else
                        {

                            Console.WriteLine(GeneralDataLibrary.Note() + "No item to unequip!");

                        }

                        break;
                    case "offhand":
                        if (player.SecondHand != null)
                        {

                            if (player.Inventory.addItem(player.SecondHand))
                            {

                                Console.WriteLine("Unequiped: " + player.SecondHand.Name);
                                player.SecondHand = null;

                            }

                        }
                        else
                        {

                            Console.WriteLine(GeneralDataLibrary.Note() + "No item to unequip!");

                        }
                        break;
                    case "armor":
                        if (player.Armor != null)
                        {

                            if (player.Inventory.addItem(player.Armor))
                            {

                                Console.WriteLine("Unequiped: " + player.Armor.Name);
                                player.Armor = null;

                            }
                            
                        }
                        else
                        {

                            Console.WriteLine(GeneralDataLibrary.Note() + "No item to unequip!");

                        }

                        break;
                    case "special":
                        if (player.Special != null)
                        {

                            if (player.Inventory.addItem(player.Special))
                            {

                                Console.WriteLine("Unequiped: " + player.Special.Name);
                                player.Special = null;

                            }
                            
                        }
                        else
                        {

                            Console.WriteLine(GeneralDataLibrary.Note() + "No item to unequip!");

                        }

                        break;
                    default:
                        Console.WriteLine("Can't find item slot.");
                        break;
                }

            }

        }

        public void attackEnemy(Command command)
        {



        }

    }
}
