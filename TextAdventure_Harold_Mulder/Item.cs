using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure_Harold_Mulder
{
    class Item
    {

        protected string name;
        protected string description;


        protected bool hasPickupEvent;
        protected bool hasRoomEvent;


        internal string Name { get => name;}
        internal string Description { get => description;}

        internal bool HasPickupEvent { get => hasPickupEvent;}
        internal bool HasRoomEvent { get => hasRoomEvent;}

        public Item()
        {

            name = "item";
            description = "A generic game item.";

            hasPickupEvent = false;
            hasRoomEvent = false;
                
        }

        public virtual void handleRoomEvent(Character character)
        {



        }

        public virtual void handlePickupEvent(Character character)
        {



        }

    }
}
