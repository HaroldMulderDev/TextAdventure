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

        public void enableEndTimer(uint timer)
        {

            endConditions.Add(0);
            maxEffectTimer = timer;
            effectTimer = maxEffectTimer;

        }

        public void enableEndBandage(uint amount)
        {

            bandageNeed = amount;
            endConditions.Add(1);

        }



        // Handle conditions
        // =======================================================
        
        public void enableAlwaysActive()
        {

            conditions.Add(0);

        }



        // Handle effects
        // =======================================================

        public void enableDamage(uint amount)
        {

            effectType.Add(0);
            damageAmount = amount;

        }

        public void enableHeal(uint amount)
        {

            effectType.Add(1);
            healAmount = amount;

        }

        public void enableImmunity()
        {

            effectType.Add(2);

        }

        // Handle effects
        // =======================================================

        public void enableRoomEnter()
        {

            triggerType = 0;

        }



        public void HandleFullEffect()
        {

                bool isNotBlocked = HandleConditions();

                if (isNotBlocked)
                {

                    HandleEffects();
                    
                    HandleEndConditions();

                }

        }

        

        public bool HandleConditions()
        {


            return true;
        }

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

        public void HandleEndConditions()
        {



        }

        public void setName(string name)
        {

            this.name = name;

        }

        public void setDescription(string description)
        {

            this.description = name;

        }

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
