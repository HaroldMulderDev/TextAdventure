using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure_Harold_Mulder
{
    class Item
    {

        private string name;
        private string description;

        internal string Name { get => name; }
        internal string Description { get => description; }

        public Item()
        {

            name = "item";
            description = "A generic game item.";

    }

    }
}
