using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelSpaceHunter.Effects
{
    class EffectManager
    {
        private static List<BaseEffect> effects;

        public static void Initialize()
        {
            effects = new List<BaseEffect>();
        }

        public static void AddEffect(BaseEffect effect)
        {
            effects.Add(effect);
        }

        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < effects.Count; i++)
            {
                effects[i].Update(gameTime);

                if (effects[i].Complete)
                {
                    effects.RemoveAt(i);
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < effects.Count; i++)
            {
                effects[i].Draw(spriteBatch);
            }
        }
    }
}
