using System;
using TextAdventure_Harold_Mulder;
using System.Collections.Generic;

namespace ZuulCS
{
	public class CommandLibrary
	{
		// an array that holds all valid command words
		private List<string> validCommands;
        
        private Dictionary<string, string> commandData;

        internal Dictionary<string, string> CommandData { get => commandData; }

		/**
	     * Constructor - initialise the command words.
	     */
		public CommandLibrary()
		{

            

            initializeCommands();

            validCommands = new List<string>();

            foreach(string key in commandData.Keys)
            {

                validCommands.Add(key);

            }
		}

		/**
	     * Check whether a given string is a valid command word.
	     * Return true if it is, false if it isn't.
	     */
		public bool isCommand(string instring)
		{
			for(int i = 0; i < validCommands.Count; i++) {
				if (validCommands[i] == instring) {
					return true;
				}
			}
			// if we get here, the string was not found in the commands
			return false;
		}

		/**
	     * Print all valid commands to Console.WriteLine.
	     */
		public void showAll()
		{
			for(int i = 0; i < validCommands.Count; i++) {
				Console.Write(validCommands[i]);
				if (i != validCommands.Count - 1) {
					Console.Write(", ");
				}
			}
			GeneralDataLibrary.Break();
		}

        public void initializeCommands()
        {

            commandData = new Dictionary<string, string>();

            commandData.Add("go", GeneralDataLibrary.I() + "Description: \n \n" + GeneralDataLibrary.I(2) + "Moves the player to another room. \n \n \n" + GeneralDataLibrary.I() + "Usages: \n \n" + GeneralDataLibrary.I(2) + "'go <direction>': Moves the player to the room in given direction.");
            commandData.Add("quit", GeneralDataLibrary.I() + "Description: \n \n" + GeneralDataLibrary.I(2) + "Quits the current game. \n \n \n" + GeneralDataLibrary.I() + "Usages: \n \n" + GeneralDataLibrary.I(2) + "'quit': Closes the current game.");
            commandData.Add("help", GeneralDataLibrary.I() + "Description: \n \n" + GeneralDataLibrary.I(2) + "Shows global helpful information. \n \n \n" + GeneralDataLibrary.I() + "Usages: \n \n" + GeneralDataLibrary.I(2) + "'help': Shows a list of possible commands. \n" + GeneralDataLibrary.I(2) + "'help <command>': Shows details on a specific command.");
            commandData.Add("look", GeneralDataLibrary.I() + "Description: \n \n" + GeneralDataLibrary.I(2) + "Checks the current room and player condition. \n \n \n" + GeneralDataLibrary.I() + "Usages: \n \n" + GeneralDataLibrary.I(2) + "'look': Show the current room's items, exits, description and also shows player health and bag space.");
            commandData.Add("clear", GeneralDataLibrary.I() + "Description: \n \n" + GeneralDataLibrary.I(2) + "Clears the console of text. \n \n" + GeneralDataLibrary.I() + "Usages: \n \n" + GeneralDataLibrary.I(2) + "'clear': Removes all the messages from the console and display the current room's description.");
            commandData.Add("take", GeneralDataLibrary.I() + "Description: \n \n" + GeneralDataLibrary.I(2) + "Picks up one or more items from the current room. \n \n \n" + GeneralDataLibrary.I() + "Usages: \n \n" + GeneralDataLibrary.I(2) + "'take': Attempt to take all items from the current room. \n" + GeneralDataLibrary.I(2) + "'take <item>': picks up first item with given name from current room.\n" + GeneralDataLibrary.I(2) + "'take <number>': Picks up item with given index.");
            commandData.Add("drop", GeneralDataLibrary.I() + "Description: \n \n" + GeneralDataLibrary.I(2) + "Drops item into the current room. \n \n \n" + GeneralDataLibrary.I() + "Usages: \n \n" + GeneralDataLibrary.I(2) + "'drop <item>': Drops specific item from the players bag into the current room. \n" + GeneralDataLibrary.I(2) + "'drop <number>': drops the item at corresponing index in the bag.");
            commandData.Add("bag", GeneralDataLibrary.I() + "Description: \n \n" + GeneralDataLibrary.I(2) + "Shows the items in your bag \n \n \n" + GeneralDataLibrary.I() + "Usages: \n \n" + GeneralDataLibrary.I(2) + "'bag': Shows all the bag content and remaining bag space");

        }
	}
}
