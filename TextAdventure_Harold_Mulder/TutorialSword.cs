using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZuulCS;

namespace TextAdventure_Harold_Mulder
{
    class TutorialSword : Weapon
    {

        public TutorialSword()
        {

            durability = 12;
            damage = 3;
            name = "iron sword";
            description = "A sturdy iron blade";

        }

        public override void progressTutorial()
        {

            pickupTutorialUnlock.removeTutorialLock();

        }

    }
}
