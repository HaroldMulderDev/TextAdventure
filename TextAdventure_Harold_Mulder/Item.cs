using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZuulCS;

namespace TextAdventure_Harold_Mulder
{

    /**
    * A generic item class used to inherit from
    */

    public class Item
    {

        protected string name;
        protected string description;
        protected uint durability;

        protected uint damage;
        protected uint resistance;

        protected Room pickupTutorialUnlock;

        protected bool hasPickupEvent;
        protected bool hasRoomEvent;
        protected bool hasPickupTutorialEvent;

        internal string Name { get => name;}
        internal string Description { get => description;}

        internal uint Damage { get => damage; }
        internal uint Resistance { get => resistance; }

        internal Room PickupTutorialUnlock { get => pickupTutorialUnlock; }

        internal bool HasPickupEvent { get => hasPickupEvent;}
        internal bool HasRoomEvent { get => hasRoomEvent;}
        internal bool HasPickupTutorialEvent { get => hasPickupTutorialEvent; }

        /**
        * Constructor - Initialize item
        */

        public Item()
        {

            name = "item";
            description = "A generic game item.";
            durability = 1;
            damage = 0;


            hasPickupEvent = false;
            hasRoomEvent = false;
            hasPickupTutorialEvent = false;

            
                
        }

        /**
        * This is used if the item has a room event
        */

        public virtual bool handleRoomEvent(Character character)
        {

            return false;

        }

        /**
        * This is used if the item has a pickup event
        */

        public virtual bool handlePickupEvent(Character character)
        {

            return false;

        }

        /**
        * Does whatever an item needs to do and return true if the item ought to be destroyed
        */

        public virtual bool use()
        {

            Console.WriteLine("Cant use this item that way!");

            return false;

        }

        /**
        * Does whatever an item needs to do to any character and return true if the item ought to be destroyed
        */

        public virtual bool use(Character character)
        {

            Console.WriteLine("Can't use this item that way!");

            return false;

        }

        /**
        * Does whatever an item needs to do to the player and return true if the item ought to be destroyed
        */

        public virtual bool use(Player host)
        {

            Console.WriteLine("Can't use this item that way!");

            return false;

        }

        /**
        * Does whatever an item needs to do to any room and return true if the item ought to be destroyed
        */

        public virtual bool use(Room room)
        {

            Console.WriteLine("Can't use this item that way!");

            return false;

        }

        /**
        * This is used if the item needs to progress the tutorial
        */

        public virtual void progressTutorial()
        {

            // Nothin

        }

        /**
        * This is used to initialize any item to progess the tutorial
        */

        public void setPickupTutorialUnlock(Room room)
        {

            hasPickupTutorialEvent = true;
            pickupTutorialUnlock = room;

        }

    }
}
