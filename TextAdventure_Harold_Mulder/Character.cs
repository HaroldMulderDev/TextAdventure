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
        private uint maxHealth;

        private Item firstHand; // Main hand only weapons can be held here
        private Item secondHand; // Second hand any item can be held here and quickly used even during battle
        private Item armor; // This is the armor your wearing it will resist hits you take.
        private Item special; // A special is a piece of equipment giving you a strong boost.

        internal uint Health { get => health; }
        internal uint MaxHealth { get => maxHealth; } 

        private List<StatusEffect> currentStatusEffects;

        internal Item FirstHand { get => firstHand; set => firstHand = value; }
        internal Item SecondHand { get => secondHand; set => secondHand = value; }
        internal Item Armor { get => armor; set => armor = value; }
        internal Item Special { get => special; set => special = value; }

        public Character()
        {

            maxHealth = 100;
            health = maxHealth;
            currentStatusEffects = new List<StatusEffect>();

            firstHand = null;
            secondHand = null;
            armor = null;
            special = null;
            

        }

        public void createStatusEffects(Character host, uint trigger , uint effect, uint effectMultiplier, uint condition, uint endCondition, uint endConditionMultiplier, string name, string description)
        {

            StatusEffect s = new StatusEffect(host);

            switch (trigger)
            {

                case 0:
                    s.enableRoomEnter();
                    break;

            }

            // Add all effects
            

            switch (effect)
            {

                case 0:
                    s.enableDamage(effectMultiplier);
                    break;

                case 1:
                    s.enableHeal(effectMultiplier);
                    break;

                case 2:
                    s.enableImmunity();
                    break;
            }

            


            // Add all Conditions
            
            switch (condition)
            {

                case 0:
                    s.enableAlwaysActive();
                    break;

            }

            switch (endCondition)
            {

                case 0:
                    s.enableEndTimer(endConditionMultiplier);
                    break;

                case 1:
                    s.enableEndBandage(endConditionMultiplier);
                    break;

            }

            s.setName(name);
            s.setDescription(description);
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

                for(int ii = currentStatusEffects[i].EndConditions.Count-1; ii >= 0; ii--)
                {

                    if(currentStatusEffects[i].EndConditions[ii] == amount)
                    {

                        if (currentStatusEffects[i].reduceTimer(1))
                        {

                            removeEffect(i);

                        }

                    }

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

        public void removeEffect(int i)
        {

            currentStatusEffects[i] = null;
            currentStatusEffects.RemoveAt(i);

        }

        public bool checkBadItem(Item item, int e)
        {

            switch (e)
            {

                case 0:
                    if (item.HasPickupEvent)
                    {

                        if (item.handlePickupEvent(this))
                        {

                            return true;

                        }

                    }
                    break;
                case 1:
                    if (item.HasRoomEvent)
                    {

                        if (item.handleRoomEvent(this))
                        {

                            return true;

                        }

                    }
                    break;

            }

            return false;

        }

    }
}
