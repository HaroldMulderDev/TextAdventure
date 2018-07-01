using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZuulCS;

namespace TextAdventure_Harold_Mulder
{

    /**
    * A tutorial sword to move the tutorial further
    */
    public class TutorialSword : Weapon
    {

        /**
        * Initialize the tutorial sword
        */
        public TutorialSword()
        {

            durability = 15;
            damage = 12;
            name = "iron-sword";
            description = "A sturdy iron blade";

        }

        /**
        * Progress the actual tutorial
        */
        public override void progressTutorial()
        {

            pickupTutorialUnlock.removeTutorialLock();

        }

    }
}
