using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MelSpaceHunter.Gameplay.Forms
{
    enum Elements
    {
        None = -1,
        Fire = 1,
        Water = 2,
        Earth = 3,
        Wind = 4
    }

    class Element
    {
        public static bool StrongAgainst(Elements current, Elements target)
        {
            return (int)target - (int)current == 1 || ((int)current == 1 && (int)target == 4);
        }
    }
}
