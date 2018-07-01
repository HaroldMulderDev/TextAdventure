using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZuulCS;

namespace TextAdventure_Harold_Mulder
{

    /**
    * general enemy class can be used for basically any enemy
    */

    public class Enemy : Character
    {

        private string attackShout;

        /**
        * Initialize the enemy
        */

        public Enemy(string name, string description, uint maxHealth, uint rawDamage, string attackShout)
        {
            this.name = name;
            this.description = description;
            this.attackShout = attackShout;

            this.maxHealth = maxHealth;
            this.rawDamage = rawDamage;
            health = maxHealth;

        }

        /**
        * Handle the enemies AI needed for this game
        */

        public void HandleAi(Character player)
        {

            
            Console.WriteLine(GeneralDataLibrary.I() + GeneralDataLibrary.Note() + attackShout);
            GeneralDataLibrary.Break();
            if(attack(this, player))
            {

                Console.WriteLine(GeneralDataLibrary.I() + "You have died!");

            } else
            {

                Console.WriteLine(GeneralDataLibrary.I() + "You have: " + player.Health + " health left!");

            }

            GeneralDataLibrary.Break();


        }

    }
}
