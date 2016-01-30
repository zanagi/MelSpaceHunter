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
        private int maxZoneCount, zoneInterval, elapsedZoneTime, baseElementalWidth, baseElementalHeight;
        private bool newZoneNeeded;

        private List<Animation> animations;
        private Animation creationAnimation;

        private const int relativeRemovalRadius = 200;
        private const int baseElementalHealth = 20;
        private const int baseMaxZoneElementals = 5;

        public ElementalManager(int baseElementalWidth, int baseElementalHeight, int maxZoneCount = 10, int zoneInterval = 2500)
        {
            this.elementals = new List<Elemental>();
            this.elementalZones = new List<ElementalZone>();
            this.maxZoneCount = maxZoneCount;
            this.zoneInterval = zoneInterval;
            this.elapsedZoneTime = 0;
            this.baseElementalWidth = baseElementalWidth;
            this.baseElementalHeight = baseElementalHeight;
            this.newZoneNeeded = true;

            this.animations = new List<Animation>()
            {
                new Animation("Forms/fireForm", 4, 1, 60, 60) //placeholder width height
            };
            this.creationAnimation = new Animation("Elementals/elementals_creation", 6, 1, baseElementalWidth, baseElementalHeight);
        }

        public List<Elemental> GetElementals()
        {
            return elementals;
        }

        public void LoadContent(ContentManager content)
        {
            creationAnimation.LoadContent(content);

            for (int i = 0; i < animations.Count; i++)
            {
                animations[i].LoadContent(content);
            }
        }

        public void Update(GameTime gameTime, Character character, ViewManager viewManager)
        {
            UpdateZones(gameTime, character, viewManager);
            UpdateElementals(gameTime, character, viewManager);
        }

        private void UpdateElementals(GameTime gameTime, Character character, ViewManager viewManager)
        {
            for (int i = 0; i < elementals.Count; i++)
            {
                // Using relativeX for both check for a circle image zone
                if (Vector2.DistanceSquared(character.Position, elementals[i].Position) > Math.Pow(viewManager.RelativeX(relativeRemovalRadius), 2))
                {
                    // Elemental too far to care anymore, remove it
                    Console.WriteLine("Elemental removed");
                    elementals.RemoveAt(i);
                }
                else
                {
                    elementals[i].Update(gameTime, elementals, character.Position);
                    elementals[i].Visible = Math.Abs(character.Position.X - elementals[i].Position.X) < viewManager.RelativeX(50) + elementals[i].Width
                        || Math.Abs(character.Position.Y - elementals[i].Position.Y) < viewManager.RelativeY(50) + elementals[i].Height;
                }
            }
        }

        private void UpdateZones(GameTime gameTime, Character character, ViewManager viewManager)
        {
             // Using relativeX for both check for a circle image zone
            int zoneRadius = viewManager.RelativeX(50);

            if (newZoneNeeded)
            {
                Console.WriteLine("New zone needed");
                if (elementalZones.Count == 0)
                {
                    // Create a zone with the character at the radius
                    AddNewZone(character.Position, character.StatAverage, zoneRadius);
                } else if(elementalZones.Count < maxZoneCount) {
                    CreateNewZone(character, viewManager, zoneRadius);
                }
            }
            else
            {
                elapsedZoneTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (elapsedZoneTime >= zoneInterval)
                {
                    newZoneNeeded = true;
                    elapsedZoneTime = 0;
                }
            }

            for (int i = 0; i < elementalZones.Count; i++)
            {
                // Smaller removal radius for zones
                if (elementalZones[i].GeneratedAll || Vector2.DistanceSquared(character.Position, elementalZones[i].Position)
                    > viewManager.RelativeX(relativeRemovalRadius) * viewManager.RelativeX(relativeRemovalRadius))
                {
                    Console.WriteLine("Zone removed");
                    elementalZones.RemoveAt(i);
                }
                else
                {
                    elementalZones[i].Update(gameTime, character.Position);

                    if (elementalZones[i].GeneratingElement)
                    {
                        elementals.Add(elementalZones[i].NewElemental(elementals));

                        Console.WriteLine("Elemental created at: " + elementals[elementals.Count - 1].Position);
                    }
                }
            }
        }

        /// <summary>
        /// Generates a position for zone, created the zone and adds it to the list of zones
        /// </summary>
        /// <param name="character"></param>
        /// <param name="viewManager"></param>
        /// <param name="zoneRadius"></param>
        private void CreateNewZone(Character character, ViewManager viewManager, int zoneRadius)
        {
            bool zoneCreated = false;
            int counter = 0;
            int counterMax = 5;

            while (!zoneCreated && counter < counterMax)
            {
                counter++;

                Vector2 pos = RandomZonePos(character.Position, viewManager);
                zoneCreated = true;

                for (int i = 0; i < elementalZones.Count; i++)
                {
                    // More lenient zone overlapping allowed
                    if (Vector2.DistanceSquared(elementalZones[i].Position, pos) < zoneRadius * zoneRadius / 4)
                    {
                        zoneCreated = false;
                        break;
                    }
                }

                if (zoneCreated)
                    AddNewZone(pos, character.StatAverage, zoneRadius);
            }
        }

        private void AddNewZone(Vector2 center, int characterStatAverage, int zoneRadius)
        {
            if (animations.Count <= 0)
                throw new Exception("No animations for elementals");

            newZoneNeeded = false;

            Elements mainElement = Element.RandomElement();
            List<Elements> zoneElements = new List<Elements>() { 
                mainElement, mainElement, Element.RandomElement(), Element.RandomElement(), Element.RandomElement()
            };

            List<Elemental> baseElementals = new List<Elemental>();

            for(int i = 0; i < 4; i++)
            {
                int wh = Helper.RandomIntAroundCenter((baseElementalWidth + baseElementalHeight) / 2, 0.2f);
                int attack = 1 + Helper.RandomIntAroundCenter(characterStatAverage * 2 / 3, 0.5f);
                int defense = 1 + Helper.RandomIntAroundCenter(characterStatAverage * 2 / 3, 0.5f);
                int speed = 1 + Helper.RandomIntAroundCenter(characterStatAverage * 2 / 3, 0.5f);
                int health = baseElementalHealth + Helper.RandomInt(0, characterStatAverage);

                baseElementals.Add(
                    new Elemental(zoneElements[Helper.RandomInt(0, zoneElements.Count)], Movement.RandomMovement(),
                        Vector2.Zero, Vector2.One, animations[Helper.RandomInt(0, animations.Count)], creationAnimation,
                        wh, wh, attack, defense, speed, health)
                );
            };

            // Stat calculation
            Console.WriteLine("Zone added");

            elementalZones.Add(new ElementalZone(center, zoneRadius, baseMaxZoneElementals + characterStatAverage / 6,
                1000 - 100 * characterStatAverage / 6, baseElementals));
        }

        private Vector2 RandomZonePos(Vector2 characterPosition, ViewManager viewManager)
        {
            var angle = Math.Sqrt(Helper.RandomNextDouble()) * Math.PI * 2;
            var gRadius = Helper.RandomInt(viewManager.RelativeY(10), viewManager.RelativeX(125));
            return new Vector2((int)(characterPosition.X + gRadius * Math.Cos(angle)),
                (int)(characterPosition.Y + gRadius * Math.Sin(angle)));
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
