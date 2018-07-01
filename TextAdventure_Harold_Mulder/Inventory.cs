using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure_Harold_Mulder
{

    /**
    * The general inventory class
    */

    public class Inventory
    {

        private List<Item> items;
        private int maxItems;
        private int spaceLeft;

        internal List<Item> Items { get => items; }
        internal int SpaceLeft { get => maxItems - items.Count; }

        /**
        * Initialize the inventory
        */

        public Inventory(int amount)
        {

            items = new List<Item>();
            maxItems = amount;



        }

        /**
        * Add an item to the invetory and return wether or not it can fit
        */

        public bool addItem(Item item) {

            
            uint currentItems = 0;

            for (int i = items.Count - 1; i >= 0; i--)
            {
                currentItems++;
            }

            if (currentItems < maxItems)
            {

                items.Add(item);
                return (true);

            }

            Console.WriteLine(GeneralDataLibrary.I() + "There are too many items here!");
            return (false);

        }

        /**
        * Send an item from one invetory to another inventory the item send is determined by the key
        */

        public Item sendItem(Inventory other, string key){

            if (other == null)
            {

                Console.WriteLine("This inventory does not exist!");
                return null;
            }

            for (int i = items.Count-1; i >= 0; i--)
            {

                if (items[i].Name == key){

                    if (other.addItem(items[i]))
                    {
                        Item item = items[i];
                        items.Remove(items[i]);
                        return item;

                    }
                }

            }

            Console.WriteLine(GeneralDataLibrary.I() + "Cannot find that item!");
            return null;

        }

        /**
        * Send an item from one inventory to another inventory the item send is determined by index
        */

        public Item sendItem(Inventory other, int i)
        {

            if (other.addItem(items[i]))
            {
                Item item = items[i];
                items.Remove(items[i]);
                return item;

            }

            return null;

        }

    }
}
