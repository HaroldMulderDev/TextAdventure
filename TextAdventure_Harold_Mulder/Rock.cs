using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZuulCS;

namespace TextAdventure_Harold_Mulder
{
    public class Rock : Item
    {

        public Rock()
        {

            durability = 1;
            name = "rock";
            description = "A rock the perfect size for throwing";

        }

        public override bool use(Room room)
        {

            if (room.IsBarred)
            {

                Console.WriteLine(GeneralDataLibrary.I() + "You throw the rock... \n");
                if (room.breach())
                {

                    durability--;
                    if (durability <= 0) {

                        return true;

                    }

                } else
                {

                    Console.WriteLine(GeneralDataLibrary.I() + "but really there was no use in it.");

                }
                

            }

            return false;

        }

    }
}
