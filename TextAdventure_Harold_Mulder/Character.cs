using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZuulCS;

namespace TextAdventure_Harold_Mulder
{
    class Character
    {

        private uint health;
        internal uint Health { get => health; }

        private uint maxHealth;
        internal uint MaxHealth { get => maxHealth; } 

        private string[] statusEffects;
        private Game host;

        private List<StatusEffect> currentStatusEffects;
        public Character()
        {

            maxHealth = 100;
            health = maxHealth;
            currentStatusEffects = new List<StatusEffect>();
            

        }

        public void createStatusEffects(Character host, uint trigger , List<uint> effects, List<uint> effectMultiplier, List<uint> conditions, List<uint> endConditions, List<uint> endConditionMultipliers)
        {

            StatusEffect s = new StatusEffect(host);

            switch (trigger)
            {

                case 0:
                    s.enableRoomEnter();
                    break;

            }

            // Add all effects
            for (int i = effects.Count-1; i >= 0; i--)
            {

                switch (effects[i])
                {

                    case 0:
                        s.enableDamage(effectMultiplier[i]);
                        break;

                    case 1:
                        s.enableHeal(effectMultiplier[i]);
                        break;

                    case 2:
                        s.enableImmunity();
                        break;
                }

            }


            // Add all Conditions
            for (int i = conditions.Count-1; i >= 0; i--)
            {

                switch (conditions[i])
                {

                    case 0:
                        s.enableAlwaysActive();
                        break;

                }

            }

            for (int i = endConditions.Count-1; i >= 0; i--)
            {

                switch (endConditions[i])
                {

                    case 0:
                        s.enableEndTimer(endConditionMultipliers[i]);
                        break;

                    case 1:
                        s.enableEndBandage(endConditionMultipliers[i]);
                        break;

                }

            }

            currentStatusEffects.Add(s);

            

        }

        public void CheckTriggers(uint amount)
        {

            for (int i = currentStatusEffects.Count - 1; i >= 0; i--)
            {

                if (currentStatusEffects[i].TriggerType == amount)
                {

                    currentStatusEffects[i].HandleFullEffect();

                }

            }
        }

        public void dealDamageByAmount(uint amount)
        {
            if (health - amount < 0)
            {
                health = 0;
            }
            else
            {
                health -= amount;
            }


        }

        public void healByAmount(uint amount)
        {
            if (health + amount > maxHealth)
            {
                health = maxHealth;
            }
            else
            {
                health += amount;
            }


        }

        private bool isAlive()
        {
            if (health > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
