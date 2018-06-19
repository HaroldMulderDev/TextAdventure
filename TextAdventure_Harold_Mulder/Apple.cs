using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure_Harold_Mulder
{
    class Apple : Consumable
    {

        public Apple()
        {

            name = "apple";
            description = "a red healthy looking apple.";
            restore = 10;

        }

        public override bool use(Player host)
        {

            host.healByAmount(restore);
            return true;

        }

    }
}
