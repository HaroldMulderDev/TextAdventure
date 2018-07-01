using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure_Harold_Mulder
{

    /**
    * A cursed crystal can be picked up and curses the player
    */

    class CursedCrystal : Item
    {

        /**
        * Initialize the cursed crystal
        */

        public CursedCrystal()
        {

            name = "crystal";
            description = "A crystal that radiates with an eerie glow.";

            hasPickupEvent = true;
            hasRoomEvent = false;

        }

        /**
        * Setup everything that triggers when you pick up this item
        */

        public override bool handlePickupEvent(Character character)
        {
            Console.WriteLine(GeneralDataLibrary.I(3) + "As you pick up the crystal you feel an ancient curse enter your body!");
            character.createStatusEffects(character, 0, 0, 5, 0, 0, 5, "GhostBite", "A ancient curse that eats its host from the inside.");
            return true;

        }

    }
}
