using System;
using TextAdventure_Harold_Mulder;
using System.Collections.Generic;

namespace ZuulCS
{
	public class CommandLibrary
	{
		// an array that holds all valid command words
		private List<string> validCommands;
        private GeneralDataLibrary GDL;
        private List<List<List<string>>> commandData;

        internal List<List<string>> CommandDescriptions { get => commandData[1]; }
        internal List<List<string>> CommandUsages { get => commandData[2]; } 

		/**
	     * Constructor - initialise the command words.
	     */
		public CommandLibrary()
		{

            GDL = new GeneralDataLibrary();

            initializeCommands();

            validCommands = new List<string>();

            for(int i = 0; i < commandData[0].Count; i++)
            {

                validCommands.Add(commandData[0][i][0]);

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
			GDL.Break();
		}

        public void initializeCommands()
        {

            commandData = new List<List<List<string>>>();

            commandData.Add(new List<List<string>>()
            {

                new List<string>{ "go" },
                new List<string>{ "quit" },
                new List<string>{ "help" },
                new List<string>{ "look" },
                new List<string>{ "clear" },
                new List<string>{ "take" },
                new List<string>{ "drop" },
                new List<string>{ "bag" }

            });

            commandData.Add(new List<List<string>>
            {

                new List<string>{ "Shows global helpful information." },
                new List<string>{ "Moves the player to another room." },
                new List<string>{ "Quits the current game." },
                new List<string>{ "Checks the current room and player condition." },
                new List<string>{ "Clears the console of text." },
                new List<string>{ "Picks up one or more items from the current room." },
                new List<string>{ "Drops item into the current room." },

            });

            commandData.Add(new List<List<string>>()
            {

                new List<string>
                {
                    "'help': Shows a list of possible commands.",
                    "'help <command>': Shows details on a specific command."
                },

                new List<string>
                {

                    "'go <direction>': Moves the player to the room in given direction."

                },

                new List<string>
                {

                    "'quit': Closes the current game."

                },

                new List<string>
                {

                    "'look': Show the current room's items, exits, description and also shows player health and bag space."

                },

                new List<string>
                {

                    "'clear': Removes all the messages from the console and display the current room's description."

                },

                new List<string>
                {

                    "'take': Attempt to take all items from the current room.",
                    "'take <item>': picks up first item with given name from current room.",
                    "'take <number>': Picks up item with given index."

                },

                new List<string>
                {

                    "'drop <item>': Drops specific item from the players bag into the current room.",
                    "'drop <number>': drops the item at corresponing index in the bag."

                }

            });

        }
	}
}
