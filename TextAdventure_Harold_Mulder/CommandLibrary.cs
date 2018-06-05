using System;

namespace ZuulCS
{
	public class CommandLibrary
	{
		// an array that holds all valid command words
		private string[] validCommands;

		/**
	     * Constructor - initialise the command words.
	     */
		public CommandLibrary()
		{
			validCommands = new string[] {
				"go",
				"quit",
				"help",
                "look",
                "yomom",
                "clear",
                "take",
                "drop"
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
			Console.WriteLine();
		}
	}
}
