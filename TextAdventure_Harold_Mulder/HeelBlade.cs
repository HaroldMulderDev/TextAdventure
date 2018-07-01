using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure_Harold_Mulder
{

    /**
    * A weapon creates bleeding on attack
    */

    class HeelBlade : Weapon
    {

        /**
        * Initialize the heelblade
        */

        public HeelBlade()
        {

            name = "heelblade";
            description = "A long knife, specially crafted to targets opponents heels in battle.";
            durability = 32;
            damage = 5;

            hasPickupEvent = false;
            hasRoomEvent = false;

        }

        /**
        * All things that trigger upon attack an enemy
        */

        public override uint attack(Character character)
        {
            bool canCreate = true;

            for (int i = character.CurrentStatusEffects.Count-1; i >= 0; i--)
            {

                if(character.CurrentStatusEffects[i].Name == "Heel Cut")
                {

                    canCreate = false;

                }

            }

            if (canCreate)
            {

                Console.WriteLine(GeneralDataLibrary.Note() + "The blade created a heel cut!");
                GeneralDataLibrary.Break();
                character.createStatusEffects(character, 0, 0, 5, 0, 1, 3, "Heel Cut", "A cut your heel that drains you as you go from room to room");

            }

            durability--;
            return damage;

        }

    }
}
