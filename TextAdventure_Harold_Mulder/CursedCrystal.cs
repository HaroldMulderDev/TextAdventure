using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure_Harold_Mulder
{
    class CursedCrystal : Item
    {

        public CursedCrystal()
        {

            name = "Crystal";
            description = "A crystal that radiates with an eerie glow.";

            hasPickupEvent = true;
            hasRoomEvent = false;

        }

        public override void handlePickupEvent(Character character)
        {

            character.createStatusEffects(character, 0, 0, 5, 0, 0, 5, "GhostBite", "A ancient curse that eats its host from the inside.");

        }

    }
}
