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
        private int radius, generatedElementals, maxElementals, generationInterval, elapsedMillis, baseElementalCount;
        private List<Elemental> baseElementals;
        private bool generating;

        private const int velocity = 15;

        public ElementalZone(Vector2 center, int radius, int maxElementals, int generationInterval, List<Elemental> baseElementals)
        {
            this.center = center;
            this.radius = radius;
            this.generatedElementals = 0;
            this.maxElementals = maxElementals;
            this.generationInterval = generationInterval;
            this.baseElementals = baseElementals;
            this.baseElementalCount = baseElementals.Count;
            this.generating = true;
        }

        public void Update(GameTime gameTime, Vector2 characterPosition)
        {
            elapsedMillis += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedMillis > generationInterval)
            {
                elapsedMillis = 0;
                generating = true;
            }
            if (generating)
            {
                // Move center away from player
                Vector2 delta = center - characterPosition;
                delta.Normalize();
                center += delta * velocity;
            }
        }

        public Elemental NewElemental(List<Elemental> elementals)
        {
            generatedElementals += 1;

            int counter = 0;
            int counterMax = 10;

            Elemental baseElemental = baseElementals[Helper.RandomInt(0, baseElementalCount)];

            while (counter < counterMax)
            {
                var angle = Math.Sqrt(Helper.RandomNextDouble()) * Math.PI * 2;
                var gRadius = Math.Sqrt(Helper.RandomNextDouble()) * radius;
                Vector2 pos = new Vector2((int)(center.X + gRadius * Math.Cos(angle)), (int)(center.Y + gRadius * Math.Sin(angle)));

                bool intersects = false;
                for (int i = 0; i < elementals.Count; i++)
                {
                    if (Vector2.DistanceSquared(pos, elementals[i].Position) < Math.Pow(elementals[i].Width + baseElemental.Width, 2))
                    {
                        intersects = true;
                        break;
                    }
                }

                // No intersection, good place to create new elemental
                if(!intersects) {
                    float velX = 2 * (float)Helper.RandomNextDouble() - 1;
                    float velY = 2 * (float)Helper.RandomNextDouble() - 1;
                    Vector2 velocity = (velX != 0.0f || velY != 0.0f) ? new Vector2(velX, velY) : Vector2.One;
                    velocity.Normalize();
                    return baseElemental.Clone(Movement.RandomMovement(), pos, velocity);
                }
            }
            return null;
        }

        public bool GeneratedAll
        {
            get { return generatedElementals >= maxElementals; }
        }

        public Vector2 Position
        {
            get { return center; }
        }

        public bool GeneratingElement
        {
            get { return generating; }
            set { generating = value; }
        }
    }
}
