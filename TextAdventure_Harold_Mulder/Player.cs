using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZuulCS;

namespace TextAdventure_Harold_Mulder
{
    class Player : Character
    {

        private Room currentRoom;
        private Inventory inventory;

        internal Room CurrentRoom { get => currentRoom; set => currentRoom = value; }
        internal Inventory Inventory { get => inventory; }

        public Player()
        {

            name = "player";
            description = "The player character.";

            inventory = new Inventory(8);

        }

    }
}
