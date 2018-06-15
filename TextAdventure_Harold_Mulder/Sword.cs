using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure_Harold_Mulder
{
    class Sword : Item
    {

        protected uint damage;

        public Sword()
        {

            name = "sword";
            description = "A generic sword.";
            durability = 1;
            damage = 0;

            hasPickupEvent = false;
            hasRoomEvent = false;

        }

        public virtual uint attack(Character character)
        {

            character.dealDamageByAmount(damage);
            durability--;
            return damage;

        }

    }
}
