using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZuulCS;

namespace TextAdventure_Harold_Mulder
{
    class Weapon : Item
    {

        protected uint damage;

        public Weapon()
        {

            name = "sword";
            description = "A generic sword.";
            durability = 1;
            damage = 0;

            hasPickupEvent = false;
            hasRoomEvent = false;

        }

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

        public virtual uint attack(Character character)
        {

            character.dealDamageByAmount(damage);
            durability--;
            return damage;

        }

    }
}
