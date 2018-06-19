using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure_Harold_Mulder
{
    class Enemy : Character
    {

        public Enemy(string name, string description, uint maxHealth, uint rawDamage)
        {
            this.name = name;
            this.description = description;

            this.maxHealth = maxHealth;
            this.rawDamage = rawDamage;
            health = maxHealth;

        }

        public void HandleAi()
        {



        }

    }
}
