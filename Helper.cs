using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MelSpaceHunter
{
    class Helper
    {
        private static Random random;

        public static void Initialize()
        {
            random = new Random();
        }

        public static void Initialize(int randomSeed)
        {
            random = new Random(randomSeed);
        }

        public static int RandomInt(int min, int max)
        {
            return random.Next(min, max);
        }

        public static int RandomInt(float min, float max)
        {
            return random.Next((int)min, (int)max);
        }

        public static int RandomIntAroundCenter(int center, float relativeMargin) {
            int margin = (int)(center * relativeMargin);

            if (margin < 0)
                return center;

            return center + RandomInt(0, 2 * margin) - margin;
        }

        public static double RandomNextDouble()
        {
            return random.NextDouble();
        }
    }
}
