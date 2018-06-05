using System;

namespace ZuulCS
{
	public class Parser
	{
		private CommandLibrary commands;  // holds all valid command words

		public Parser()
		{
			commands = new CommandLibrary();
		}

		/**
	     * Ask and interpret the user input. Return a Command object.
	     */
		public Command getCommand()
		{
			Console.Write("> ");     // print prompt

			string word1 = null;
			string word2 = null;

			string[] words = Console.ReadLine().Split(' ');
			if (words.Length > 0) { word1 = words[0]; }
			if (words.Length > 1) { word2 = words[1]; }

			// Now check whether this word is known. If so, create a command with it.
			if (commands.isCommand(word1)) {
				return new Command(word1, word2);
			}

			// If not, create a "null" command (for unknown command).
			return new Command(null, null);
		}

		/**
	     * Print out a list of valid command words.
	     */

		public void showCommands()
		{
			commands.showAll();
		}
    }
}
