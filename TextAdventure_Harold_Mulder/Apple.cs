using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure_Harold_Mulder
{

    /**
     * Apples are a food source that restore health
     */

    class Apple : Consumable
    {

        /**
         *  Initialize the apple
         */

        public Apple()
        {

            name = "apple";
            description = "a red healthy looking apple.";
            restore = 10;

        }

    }
}
