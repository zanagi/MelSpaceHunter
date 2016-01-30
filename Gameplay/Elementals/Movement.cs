using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MelSpaceHunter.Gameplay.Elementals
{
    enum Movements
    {
        Straight = 1,
        Circling = 2,
        Shaking = 3,
        Following = 4
    }

    class Movement
    {
        public static Movements RandomMovement()
        {
            return (Movements)Helper.RandomInt(1, 5);
        }
    }
}
