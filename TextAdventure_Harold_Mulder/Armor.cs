using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure_Harold_Mulder
{

    /**
     * A general armor class used to inherit from
     */

    class Armor : Item
    {
        
        /**
         * Initialize armor
         */

        public Armor()
        {

            name = "armor";
            description = "A generic set of armor";
            resistance = 0;
            durability = 1;


        }

    }
}
