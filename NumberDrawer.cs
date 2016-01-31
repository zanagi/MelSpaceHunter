using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MelSpaceHunter
{
    class NumberDrawer
    {
        private static bool messageSent = false;
        private static Texture2D numberSheet;
        private static int digitWidth, digitHeight;

        public static void Initialize(Texture2D texture)
        {
            numberSheet = texture;
            digitWidth = texture.Width / 10;
            digitHeight = texture.Height;
        }

        public static void DrawNumber(SpriteBatch spriteBatch, int number, Rectangle digitRect, Color color)
        {
            if (numberSheet == null)
            {
                if (!messageSent)
                {
                    Console.WriteLine("No number sheet defined");
                    messageSent = true;
                }
                return;
            }
            var digits = GetIntArray(number);

            for (int i = 0; i < digits.Length; i++)
            {
                spriteBatch.Draw(numberSheet, new Rectangle(digitRect.X + i * digitRect.Width, digitRect.Y, digitRect.Width, digitRect.Height),
                    new Rectangle(digits[i] * digitWidth, 0, digitWidth, digitHeight), color);
            }
        }

        private static int[] GetIntArray(int num)
        {
            List<int> listOfInts = new List<int>();
            while (num > 0)
            {
                listOfInts.Add(num % 10);
                num = num / 10;
            }
            listOfInts.Reverse();
            return listOfInts.ToArray();
        }
    }
}
