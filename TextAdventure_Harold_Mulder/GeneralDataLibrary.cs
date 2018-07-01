using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAdventure_Harold_Mulder
{

    /**
    * A general library used for text markup purposes
    */

    static public class GeneralDataLibrary
    {

        static string longLine = ("____________________________________________________________________________________");
        static string midLine = ("___________________________________________________");
        static string shortLine = ("____________________");

        static string indent = "    ";
        static string notifier = "!!" + indent;

        /**
        * Creates a long line in text
        */

        static public void LongLine() {

            Console.WriteLine(longLine);

        }

        /**
        * Creates a midline in text
        */

        static public void MidLine(string ind)
        {

            Console.WriteLine(ind + midLine);

        }

        /**
        * Creates a shortline in text
        */

        static public void ShortLine(string ind)
        {

            Console.WriteLine(ind + shortLine);

        }

        /**
        * Return a single indent
        */

        static public string I()
        {
           
            return indent;

        }

        /**
        * Return a number of indents
        */

        static public string I(int amount)
        {
            string str = "";
            for(int i = 0; i < amount; i++)
            {

                str += indent;

            }
            return str;

        }

        /**
        * Creates a single break in the text
        */

        static public void Break()
        {

            Console.WriteLine();

        }

        /**
        * Creates a number of breaks in text
        */

        static public void Break(int amount)
        {

            for (int i = 0; i < amount; i++)
            {

                Console.WriteLine();

            }

        }

        /**
        * returns a single notifier
        */

        static public string Note()
        {

            return notifier;

        }

    }
}
