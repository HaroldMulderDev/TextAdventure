using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure_Harold_Mulder
{
    class Armor : Item
    {

        protected uint resistance;

        internal uint Resistance { get => resistance; }

        public Armor()
        {

            name = "armor";
            description = "A generic set of armor";
            resistance = 0;
            durability = 1;


        }

    }
}
