using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure_Harold_Mulder
{
    class Inventory
    {

        private List<Item> items;
        private int maxItems;
        private int spaceLeft;

        private string indent;

        internal List<Item> Items { get => items; }
        internal int SpaceLeft { get => maxItems - items.Count; }

        public Inventory(int amount)
        {

            items = new List<Item>();
            maxItems = amount;

            indent = "    ";


        }

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

            Console.WriteLine(indent + "There are too many items here!");
            return (false);

        }

        public Item sendItem(Inventory other, string key){

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

            Console.WriteLine(indent + "Cannot find that item!");
            return null;

        }

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
