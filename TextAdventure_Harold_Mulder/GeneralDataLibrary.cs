using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAdventure_Harold_Mulder
{
    static public class GeneralDataLibrary
    {

        static string longLine = ("____________________________________________________________________________________");
        static string midLine = ("___________________________________________________");
        static string shortLine = ("____________________");

        static string indent = "    ";

        static public void LongLine() {

            Console.WriteLine(longLine);

        }

        static public void MidLine(string ind)
        {

            Console.WriteLine(ind + midLine);

        }

        static public void ShortLine(string ind)
        {

            Console.WriteLine(ind + shortLine);

        }

        static public string I()
        {
           
            return indent;

        }

        static public string I(int amount)
        {
            string str = "";
            for(int i = 0; i < amount; i++)
            {

                str += indent;

            }
            return str;

        }

        static public void Break()
        {

            Console.WriteLine();

        }

        static public void Break(int amount)
        {

            for (int i = 0; i < amount; i++)
            {

                Console.WriteLine();

            }

        }

    }
}
