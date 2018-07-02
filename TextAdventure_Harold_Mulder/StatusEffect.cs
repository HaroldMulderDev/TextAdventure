using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* This list shows the things an effect can have.
    Triggers:
        0 = onRoomEnter
    Effects:
        0 = damage
        1 = heal
        2 = immunity
    Conditions:
        0 = Always active
    EndConditions:
        0 = timerEnd
        1 = onUseBandage
*/
namespace TextAdventure_Harold_Mulder
{

    /**
    * A status effect class to effect a character
    */
    public class StatusEffect
    {

        private Character host;

        private string name;
        private string description;

        internal string Name { get => name; }

        private uint triggerType;
        internal uint TriggerType { get => triggerType; }

        List<uint> effectType;
        List<uint> conditions;
        private List<uint> endConditions;
        internal List<uint> EndConditions { get => endConditions; }

        private uint effectTimer;
        private uint maxEffectTimer;

        private uint bandageNeed;

        private uint damageAmount;
        private uint healAmount;

        /**
        * Initialize the status effect
        */
        public StatusEffect(Character player)
        {

            host = player;
            effectType = new List<uint>();
            conditions = new List<uint>();
            endConditions = new List<uint>();

            name = "Effect";
            description = "A general status effect.";

        }

        // Enable end conditions
        // =======================================================

        /**
        * Enable an end timer to this status effect that triggers when a player goes from room to room
        */
        public void enableEndTimer(uint timer)
        {

            endConditions.Add(0);
            maxEffectTimer = timer;
            effectTimer = maxEffectTimer;

        }

        /**
        * Enable an end when a certain amount of (unused) "bandages"are used
        */
        public void enableEndBandage(uint amount)
        {

            bandageNeed = amount;
            endConditions.Add(1);

        }



        // Handle conditions
        // =======================================================

        /**
        * Allows the effect to be always active
        */
        public void enableAlwaysActive()
        {

            conditions.Add(0);

        }



        // Handle effects
        // =======================================================

        /**
        * Enable a damage effect when triggered
        */
        public void enableDamage(uint amount)
        {

            effectType.Add(0);
            damageAmount = amount;

        }

        /**
        * Enable a heal effect when triggered
        */
        public void enableHeal(uint amount)
        {

            effectType.Add(1);
            healAmount = amount;

        }

        /**
        * give immunity to host
        */
        public void enableImmunity()
        {

            effectType.Add(2);

        }

        // Handle effects
        // =======================================================

        /**
        * Enable a trigger when the host goes to another room
        */
        public void enableRoomEnter()
        {

            triggerType = 0;

        }

        /**
        * Handle the full effect
        */
        public void HandleFullEffect()
        {

                if (HandleConditions())
                {

                    HandleEffects();

                }

        }

        /**
        * Handle the effect conditions to see if its allowed to trigger (unused)
        */
        public bool HandleConditions()
        {


            return true;
        }

        /**
        * trigger the effects
        */
        public void HandleEffects()
        {

            for (int i = effectType.Count-1; i >= 0; i--)
            {

                switch (effectType[i])
                {

                    case 0:
                        if(host.Health >= damageAmount)
                        {

                            host.dealDamageByAmount(damageAmount);

                        } else
                        {
                            host.dealDamageByAmount(host.Health);
                        }
                        break;
                    case 1:
                        if(host.Health+healAmount <= host.MaxHealth)
                        {
                            host.healByAmount(healAmount);
                        } else
                        {
                            host.healByAmount(host.MaxHealth - host.Health);
                        }
                        break;

                }

            }

        }

        /**
        * Set the effect name
        */
        public void setName(string name)
        {

            this.name = name;

        }

        /**
        * Set the effect description
        */
        public void setDescription(string description)
        {

            this.description = name;

        }

        /**
        * Reduce the timer from the effect
        */
        public bool reduceTimer(uint amount)
        {
            if(effectTimer - amount <= 0)
            {

                return true;

            } else
            {

                effectTimer--;
                return false;

            }
            


        }

    }
}
