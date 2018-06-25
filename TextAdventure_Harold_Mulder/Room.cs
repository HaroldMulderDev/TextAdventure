using System.Collections.Generic;
using TextAdventure_Harold_Mulder;
using System;

namespace ZuulCS
{
    public class Room
    {

        private string description;
        private Dictionary<string, Room> exits; // stores exits of this room.
        private Dictionary<string, string> exitEvents;
        private Inventory inventory;
        private List<Enemy> enemies;

        private bool isLocked;
        private bool isBarred;
        private bool isCutable;
        private bool isTutorialLocked;

        private string keyToUnlock;
        private string cutableDescription;
        private string cutableUnlockDescription;
        private string barredDescription;
        private string barredUnlockDescription;
        private string tutorialDescription;

        internal Inventory Inventory { get => inventory; }

        internal bool IsLocked { get => isLocked; }
        internal bool IsBarred { get => isBarred; }
        internal bool IsCutable { get => isCutable; }
        internal bool IsTutorialLocked { get => isTutorialLocked; set => isTutorialLocked = value; }

        internal string KeyToUnlock { get => keyToUnlock; }
        internal string CutableDescription { get => cutableDescription; }
        internal string CutableUnlockDescription { get => cutableUnlockDescription; }
        internal string BarredDescription { get => barredDescription; }
        internal string BarredUnlockDescription { get => barredUnlockDescription; }
        internal string TutorialDescription { get => tutorialDescription; }

        internal Dictionary<string, string> ExitEvents { get => exitEvents; }
        internal Dictionary<string, Room> Exits { get => exits; }
        internal List<Enemy> Enemies { get => enemies; }
        /**
	     * Create a room described "description". Initially, it has no exits.
	     * "description" is something like "in a kitchen" or "in an open court
	     * yard".
	     */
        public Room(string description)
        {

            inventory = new Inventory(6);
            this.description = description;
            exits = new Dictionary<string, Room>();
            exitEvents = new Dictionary<string, string>();

            isLocked = false;
            isBarred = false;
            isCutable = false;
            isTutorialLocked = false;

            keyToUnlock = "";

            enemies = new List<Enemy>();

        }

        /**
	     * Define an exit from this room.
	     */
        public void setExit(string direction, Room neighbor)
        {
            exits[direction] = neighbor;
        }

        public void setExit(string direction, Room neighbor, string exitText)
        {

            exits[direction] = neighbor;
            exitEvents[direction] = exitText;

        }

        /**
	     * Return the description of the room (the one that was defined in the
	     * constructor).
	     */
        public string getShortDescription()
        {
            return description;
        }

        /**
	     * Return a long description of this room, in the form:
	     *     You are in the kitchen.
	     *     Exits: north west
	     */
        public string getLongDescription()
        {
            string returnstring = "You are ";
            returnstring += description;
            returnstring += ".\n";
            returnstring += getExitstring();
            return returnstring;
        }

        /**
	     * Return a string describing the room's exits, for example
	     * "Exits: north, west".
	     */
        private string getExitstring()
        {
            string returnstring = "Exits:";

            // because `exits` is a Dictionary, we can't use a `for` loop
            int commas = 0;
            foreach (string key in exits.Keys) {
                if (commas != 0 && commas != exits.Count) {
                    returnstring += ",";
                }
                commas++;
                returnstring += " " + key;
            }
            return returnstring;
        }

        /**
	     * Return the room that is reached if we go from this room in direction
	     * "direction". If there is no room in that direction, return null.
	     */
        public Room getExit(string direction)
        {
            if (exits.ContainsKey(direction)) {
                return (Room)exits[direction];
            } else {
                return null;
            }

        }

        public void setLocked(string keyName)
        {

            keyToUnlock = keyName;
            isLocked = true;

        }

        public bool unlock(string keyName)
        {

            if (keyName == keyToUnlock)
            {

                isLocked = false;
                return true;

            }

            return false;

        }

        public void setCutable(string description, string unlockDescription)
        {

            isCutable = true;
            cutableDescription = description;
            cutableUnlockDescription = unlockDescription;

        }

        public bool cut()
        {
            if (isCutable)
            {

                isCutable = false;
                Console.WriteLine(GeneralDataLibrary.I() + barredUnlockDescription);
                return true;

            }

            Console.WriteLine("This exit can not be cut!");
            return false;

        }

        public void setBarred(string description, string unlockDescription)
        {

            isBarred = true;
            barredDescription = description;
            barredUnlockDescription = unlockDescription;

        }

        public bool breach()
        {

            if (isBarred)
            {

                isBarred = false;
                Console.WriteLine(GeneralDataLibrary.I() + barredUnlockDescription);
                return true;

            }

            Console.WriteLine("This exit can not be breached!");
            return false;

        }

        public void setTutorialLock(string description)
        {

            isTutorialLocked = true;
            tutorialDescription = GeneralDataLibrary.Note() + "'" + description + "'";

        }

        public void removeTutorialLock()
        {

            isTutorialLocked = false;

        }

        public void addEnemy(Enemy enemy)
        {

            enemies.Add(enemy);

        }

        void removeEnemy(Enemy enemy)
        {

            enemies.Remove(enemy);

        }

	}
}
