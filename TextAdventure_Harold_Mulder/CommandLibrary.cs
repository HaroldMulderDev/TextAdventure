using System;
using TextAdventure_Harold_Mulder;

namespace ZuulCS
{
	public class CommandLibrary
	{
		// an array that holds all valid command words
		private string[] validCommands;
        private GeneralDataLibrary GDL;

		/**
	     * Constructor - initialise the command words.
	     */
		public CommandLibrary()
		{

            GDL = new GeneralDataLibrary();

			validCommands = new string[] {
				"go",
				"quit",
				"help",
                "look",
                "clear",
                "take",
                "drop",
                "bag"
			};
		}

		/**
	     * Check whether a given string is a valid command word.
	     * Return true if it is, false if it isn't.
	     */
		public bool isCommand(string instring)
		{
			for(int i = 0; i < validCommands.Length; i++) {
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
			for(int i = 0; i < validCommands.Length; i++) {
				Console.Write(validCommands[i]);
				if (i != validCommands.Length - 1) {
					Console.Write(", ");
				}
			}
			GDL.Break();
		}
	}
}
