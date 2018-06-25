using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure_Harold_Mulder
{
    public class Key : Item
    {

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
