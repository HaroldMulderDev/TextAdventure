using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZuulCS;

namespace TextAdventure_Harold_Mulder
{
    class Item
    {

        protected string name;
        protected string description;
        protected uint durability;

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
            durability = 0;

            hasPickupEvent = false;
            hasRoomEvent = false;

            
                
        }

        public virtual bool handleRoomEvent(Character character)
        {

            return false;

        }

        public virtual bool handlePickupEvent(Character character)
        {

            return false;

        }

        public virtual void use()
        {



        }

        public virtual void use(Character host)
        {



        }

        public virtual void use(Room room)
        {



        }

    }
}
