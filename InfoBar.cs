using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MelSpaceHunter
{
    class InfoBar
    {
        private static Dictionary<string, Texture2D> infoTextPairs;
        private static Queue<string> keyQueue;
        private static int textSpeed;
        private static float opacity;

        private static Texture2D background, activeText;
        private static Rectangle barRect, textRect;
        private static bool active;

        public static void Initialize(Texture2D bg, int nx, int ny, int w, int h, int ts = 4)
        {
            infoTextPairs = new Dictionary<string, Texture2D>();
            keyQueue = new Queue<string>();
            background = bg;
            barRect = new Rectangle(nx, ny, w, h);
            textSpeed = ts;
            opacity = 0.0f;
            active = false;
        }

        public static void AddPair(string key, Texture2D value)
        {
            if(!infoTextPairs.ContainsKey(key))
                infoTextPairs.Add(key, value);
        }

        public static void ActivateInfo(string key)
        {
            Console.WriteLine("KEY: " + key);
            if (active)
            {
                keyQueue.Enqueue(key);
                return;
            }
            if (infoTextPairs.ContainsKey(key))
            {
                infoTextPairs.TryGetValue(key, out activeText);
                active = true;

                if (activeText != null)
                {
                    textRect = new Rectangle(barRect.X + barRect.Width,
                        barRect.Y + barRect.Height / 5, activeText.Width * barRect.Height / activeText.Height,
                        barRect.Height * 3 / 5);
                    opacity = 1.0f;
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {

            if (opacity >= 0)
            {
                if (!active)
                    opacity -= 0.05f;
                spriteBatch.Draw(background, barRect, Color.White * opacity);
            }
            if (active && activeText != null)
            {
                spriteBatch.Draw(activeText, textRect, Color.White);
                textRect.X -= textSpeed;

                if (textRect.X < barRect.X - textRect.Width)
                {
                    active = false;

                    if (keyQueue.Count > 0)
                        ActivateInfo(keyQueue.Dequeue());
                }
            }
        }
    }
}
