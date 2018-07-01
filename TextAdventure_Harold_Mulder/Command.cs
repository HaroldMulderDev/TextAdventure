using System.Collections.Generic;

namespace ZuulCS
{

    /**
    * Used to hold the command used
    */

    public class Command
	{
		private string commandWord;
		private string secondWord;
        private string thirdWord;

		/**
	     * Create a command object. First and second word must be supplied, but
	     * either one (or both) can be null. The command word should be null to
	     * indicate that this was a command that is not recognised by this game.
	     */
        
		public Command(string firstWord, string secondWord, string thirdWord)
		{
			this.commandWord = firstWord;
			this.secondWord = secondWord;
            this.thirdWord = thirdWord;
		}

		/**
	     * Return the command word (the first word) of this command. If the
	     * command was not understood, the result is null.
	     */
		public string getCommandWord()
		{
			return commandWord;
		}

		/**
	     * Return the second word of this command. Returns null if there was no
	     * second word.
	     */
		public string getSecondWord()
		{
			return secondWord;
		}

        /**
	     * Return the third word of this command. Returns null if there was no
	     * third word.
	     */
        public string getThirdWord()
        {
            return thirdWord;
        }

        /**
	     * Return true if this command was not understood.
	     */
        public bool isUnknown()
		{
			return (commandWord == null);
		}

		/**
	     * Return true if the command has a second word.
	     */
		public bool hasSecondWord()
		{
			return (secondWord != null);
		}

        /**
         * Return true if the command has a third word.
         */
        public bool hasThirdWord()
        {

            return (thirdWord != null); 

        }
	}
}
