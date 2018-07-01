using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZuulCS;

namespace TextAdventure_Harold_Mulder
{

    /**
    * A generic weapon class used to inherit from
    */
    public class Weapon : Item
    {

        /**
        * Initialize the weapon
        */
        public Weapon()
        {

            name = "weapon";
            description = "A generic weapon.";
            durability = 1;
            damage = 0;

            hasPickupEvent = false;
            hasRoomEvent = false;

        }

        /**
        * Use the weapon
        */
        public override bool use(Room room)
        {

            if (room.cut())
            {
                durability--;

                if(durability <= 0)
                {

                    return true;

                }

            }

            return false;
        }

        /**
        * Attack using the weapon
        */
        public virtual uint attack(Character character)
        {

            durability--;
            return damage;

        }

    }
}
