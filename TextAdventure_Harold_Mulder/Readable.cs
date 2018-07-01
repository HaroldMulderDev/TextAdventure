using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure_Harold_Mulder
{

    /**
    * A general readable class used for scrolls, books etc.
    */

    class Readable : Item
    {

        protected string keyword;
        protected string author;
        protected string date;
        protected string recipient;
        protected string mainText;

        /**
        * Initialize readable
        */

        public Readable()
        {

            name = "readable";
            description = "A general readable";

            this.keyword = "";
            this.author = "";
            this.date = "";
            this.recipient = "";
            this.mainText = "";

        }

        /**
        * Output text when used in given manner
        */

        public override bool use(Player player)
        {

            Console.WriteLine(GeneralDataLibrary.I() + "The " + name + " is written with " + keyword + " text:");
            GeneralDataLibrary.Break();
            Console.WriteLine(GeneralDataLibrary.I() + "Granted by: " + author);
            Console.WriteLine(GeneralDataLibrary.I() + date);
            GeneralDataLibrary.Break();
            GeneralDataLibrary.Break();
            Console.WriteLine(GeneralDataLibrary.I() + "Destined to: " + recipient);
            GeneralDataLibrary.Break();
            Console.Write(mainText);
            return false;

        }

    }
}
