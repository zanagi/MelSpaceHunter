using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MelSpaceHunter.Gameplay.Elementals
{
    class ElementalZone
    {
        private Vector2 center;
        private int radius, maxElementals, generationInterval, elapsedMillis, elementalWidth, elementalHeight;
        private List<Elemental> baseElementals;
        private bool generating;

        public ElementalZone(Vector2 center, int radius, int maxElementals, int generationInterval,
            int elementalWidth, int elementalHeight, List<Elemental> baseElementals)
        {
            this.center = center;
            this.radius = radius;
            this.maxElementals = maxElementals;
            this.elementalWidth = elementalWidth;
            this.elementalHeight = elementalHeight;
            this.baseElementals = baseElementals;
            this.generating = false;
        }

        public void Update(GameTime gameTime)
        {
            elapsedMillis += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedMillis > generationInterval)
            {
                elapsedMillis = 0;
                generating = true;
            }
        }

        public Elemental NewElemental(List<Elemental> elementals)
        {
            generating = false;

            // TODO:
            return null;
        }

        public Vector2 Position
        {
            get { return center; }
        }
    }
}
