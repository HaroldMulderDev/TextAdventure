using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure_Harold_Mulder
{

    /**
    * A generic key item
    */

    public class Key : Item
    {

        /**
        * Initialize the key
        */

        public Key()
        {

            name = "key";
            description = "A generic key.";
            durability = 1;

            hasPickupEvent = false;
            hasRoomEvent = false;

        }

    }
}
