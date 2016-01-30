using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MelSpaceHunter.Gameplay
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
        public static bool WeakAgainst(Elements current, Elements target)
        {
            return (int)target - (int)current == 1 || ((int)current == 4 && (int)target == 1);
        }

        public static Color GetColor(Elements element)
        {
            switch (element)
            {
                case Elements.Fire:
                    return Color.Red;
                case Elements.Earth:
                    return Color.Gold;
                case Elements.Water:
                    return Color.Blue;
                case Elements.Wind:
                    return Color.Green;
            }

            return Color.White;
        }

        public static Elements RandomElement()
        {
            return (Elements)Helper.RandomInt(1, 5);
        }
    }
}
