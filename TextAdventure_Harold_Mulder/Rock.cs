using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZuulCS;

namespace TextAdventure_Harold_Mulder
{
    class Rock : Item
    {

        public Rock()
        {

            durability = 1;

        }

        public override bool use(Room room)
        {

            if (room.IsBarred)
            {

                room.breach();

            }
            if (base.use(room))
            {

                return true;

            }

            return false;

        }

    }
}
