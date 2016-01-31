using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MelSpaceHunter.Gameplay.Elementals;

namespace MelSpaceHunter.Gameplay.Forms
{
    class FireForm : Form
    {
        // Q: Half damage AoE, E: ?
        private int qRadius;

        public FireForm(string path, int width, int height)
            : base(path, Elements.Fire, width, height)
        {
            this.qRadius = 3 * width;
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            // TODO: animation
        }

        public override void Update(GameTime gameTime, List<Elemental> elementals, Character character)
        {
            base.Update(gameTime, elementals, character);

            if (usingQ)
            {
                for (int i = 0; i < elementals.Count; i++)
                {
                    if (Vector2.DistanceSquared(character.Position, elementals[i].Position) <= qRadius * qRadius)
                    {
                        elementals[i].TakeDamage(DamageToElemental(character, elementals[i]) / 2);
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 origin)
        {
            base.Draw(spriteBatch, origin);
        }

        public override float AttackModifier
        {
            get
            {
                return (usingQ || usingE) ? 2.0f : 1.2f;
            }
        }
    }
}
