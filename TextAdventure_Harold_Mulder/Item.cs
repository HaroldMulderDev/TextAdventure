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
            durability = 1;

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

        public virtual bool use()
        {

            Console.WriteLine("Cant use this item that way!");

            return false;

        }

        public virtual bool use(Character host)
        {

            Console.WriteLine("Can't use this item that way!");

            return false;

        }

        public virtual bool use(Room room)
        {

            Console.WriteLine("Can't use this item that way!");

            return false;

        }

    }
}
