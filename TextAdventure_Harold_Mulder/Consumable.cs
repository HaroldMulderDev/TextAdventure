using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure_Harold_Mulder
{

    /**
    * A general consumable used to heal the player
    */

    class Consumable : Item
    {

        protected uint restore;

        /**
        * Initialize the consumable
        */

        public Consumable()
        {

            restore = 0;

        }

        /**
        * Restore health to the player on basis of items restore value
        */

        public override bool use(Character host)
        {

            host.healByAmount(restore);
            return true;

        }

    }
}
