using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure_Harold_Mulder
{
    class Consumable : Item
    {

        protected uint restore;

        public Consumable()
        {

            restore = 0;

        }

        public override bool use(Player player)
        {

            player.healByAmount(restore);
            return true;

        }

    }
}
