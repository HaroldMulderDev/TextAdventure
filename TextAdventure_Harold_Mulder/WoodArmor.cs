using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure_Harold_Mulder
{

    /**
    * A wooden set of armor the protect the carrier
    */
    class WoodArmor : Armor
    {

        /**
        * Initialize the wooden armor
        */
        public WoodArmor()
        {

            name = "woodarmor";
            description = "a weak armor made out of wood.";
            resistance = 19;
            durability = 13;

        }

    }
}
