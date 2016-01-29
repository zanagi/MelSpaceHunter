using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MelSpaceHunter.Gameplay.Elementals
{
    class ElementalManager
    {
        private List<Elemental> elementals;
        private List<ElementalZone> elementalZones;

        private List<Animation> animations;

        private const int relativeRemovalRadius = 250;

        public ElementalManager()
        {
            this.elementals = new List<Elemental>();
            this.elementalZones = new List<ElementalZone>();
            this.animations = new List<Animation>()
            {

            };
        }

        public List<Elemental> GetVisibleElementals()
        {
            return elementals.Where(el => el.Visible).ToList();
        }

        public void LoadContent(ContentManager content)
        {
            for (int i = 0; i < animations.Count; i++)
            {
                animations[i].LoadContent(content);
            }
        }

        public void Update(GameTime gameTime, Character character, ViewManager viewManager)
        {
            for (int i = 0; i < elementals.Count; i++)
            {
                if (Vector2.Distance(character.Position, elementals[i].Position) > viewManager.RelativeX(relativeRemovalRadius))
                {
                    // Elemental too far to care anymore, remove it
                    elementals.RemoveAt(i);
                }
                else
                {
                    elementals[i].Update(gameTime, elementals);
                    elementals[i].Visible = Math.Abs(character.Position.X - elementals[i].Position.X) < viewManager.RelativeX(50) + elementals[i].Width
                        || Math.Abs(character.Position.Y - elementals[i].Position.Y) < viewManager.RelativeY(50) + elementals[i].Height;
                }
            }

            for (int i = 0; i < elementalZones.Count; i++)
            {
                if (Vector2.Distance(character.Position, elementalZones[i].Position) > viewManager.RelativeX(relativeRemovalRadius))
                {
                    elementalZones.RemoveAt(i);
                }
                elementalZones[i].Update(gameTime);
            }

            // TODO: Create new elemental zones if necessary

            // TODO: Create new elementals if necessary
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < elementals.Count; i++)
            {
                elementals[i].Draw(spriteBatch);
            }
        }
    }
}
