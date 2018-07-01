using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure_Harold_Mulder
{

    /**
    * A scroll to read from
    */
    class Scroll : Readable
    {

        /**
        * Initialize the scroll
        */
        public Scroll(string keyword, string author, string date, string recipient, string mainText)
        {

            this.name = "scroll";
            this.description = "A scroll written with text";

            this.keyword = keyword;
            this.author = author;
            this.date = date;
            this.recipient = recipient;
            this.mainText = mainText;

        }

    }
}
