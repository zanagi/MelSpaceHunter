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
        private List<Elemental> visibleElementals, hiddenElementals;

        public ElementalManager()
        {
            this.visibleElementals = new List<Elemental>();
            this.hiddenElementals = new List<Elemental>();
        }

        public List<Elemental> GetVisibleElementals()
        {
            return visibleElementals;
        }

        public void Update(GameTime gameTime, Vector2 characterPosition, ViewManager viewManager)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < visibleElementals.Count; i++)
            {
                visibleElementals[i].Draw(spriteBatch);
            }
        }
    }
}
