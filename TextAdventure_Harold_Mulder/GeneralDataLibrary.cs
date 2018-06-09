using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAdventure_Harold_Mulder
{
    public class GeneralDataLibrary
    {

        string longLine;
        string midLine;
        string shortLine;

        string indent;

        public GeneralDataLibrary()
        {

            longLine = ("______________________________________________________________________________");
            midLine = ("________________________________________");
            shortLine = ("__________________");

            indent = "    ";
        }

        public void LongLine() {

            Console.WriteLine(longLine);

        }

        public void MidLine()
        {

            Console.WriteLine(midLine);

        }

        public void ShortLine()
        {

            Console.WriteLine(shortLine);

        }

        public string I()
        {
           
            return indent;

        }

        public string I(int amount)
        {
            string str = "";
            for(int i = 0; i < amount; i++)
            {

                str += indent;

            }
            return str;

        }

        public void Break()
        {

            Console.WriteLine();

        }

        public void Break(int amount)
        {

            for (int i = 0; i < amount; i++)
            {

                Console.WriteLine();

            }

        }

    }
}
