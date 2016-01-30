using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MelSpaceHunter.Gameplay.Elementals
{
    class Elemental
    {
        private Elements element;
        private Movements movement;
        private Vector2 pos, velocity;
        private Animation animation, creationAnimation;
        private bool created, visible;
        private int width, height;

        // Stats
        private int attack, defense, speed, health, maxHealth;

        public Elemental(Elements element, Movements movement, Vector2 pos, Vector2 velocity, Animation animation, Animation creationAnimation,
            int width, int height, int attack, int defense, int speed, int health)
        {
            this.created = false;
            this.visible = false;
            this.element = element;
            this.movement = movement;
            this.pos = pos;
            this.velocity = velocity;

            this.width = width;
            this.height = height;
            this.animation = animation;
            this.animation.TargetWidth = width;
            this.animation.TargetHeight = height;
            this.creationAnimation = creationAnimation;
            this.creationAnimation.TargetWidth = width;
            this.creationAnimation.TargetHeight = height;

            this.attack = attack;
            this.defense = defense;
            this.speed = speed;
            this.health = this.maxHealth = health;
        }

        public Elemental Clone(Movements newMovement, Vector2 newPosition, Vector2 newVelocity)
        {
            return new Elemental(element, newMovement, newPosition, newVelocity, animation.Clone(), creationAnimation.Clone(),
                width, height, attack, defense, speed, maxHealth);
        }

        public void Update(GameTime gameTime, List<Elemental> elementals, Vector2 characterPosition)
        {
            if (!created)
            {
                // TODO: creation animation update
                creationAnimation.Update(gameTime);
                created = creationAnimation.LoopedOnce;
                return;
            }

            animation.Update(gameTime);

            Move(characterPosition, elementals);

            // TODO: Collision detection & handling
        }

        private void Move(Vector2 characterPosition, List<Elemental> elementals)
        {
            switch (movement)
            {
                // TODO:
                case Movements.Straight:
                    // pos += velocity * MoveSpeed;
                    break;
                case Movements.Circling:
                    break;
                case Movements.Shaking:
                    break;
                case Movements.Following:
                    Vector2 a = (characterPosition - pos);
                    a.Normalize();
                    velocity += a * 0.5f;
                    break;
            }

            velocity = CalculateVelocity(elementals);
            if (velocity.X == 0 && velocity.Y == 0) return;

            velocity.Normalize();
            pos += velocity * MoveSpeed;
        }

        private Vector2 CalculateVelocity(List<Elemental> elementals)
        {
            Vector2 delta = velocity * MoveSpeed;
            Vector2 tempVelocity = Vector2.Zero;
            bool intersectionHappens = false;

            // Collision detection & handling
            for (int i = 0; i < elementals.Count; i++)
            {
                if (!this.Equals(elementals[i]))
                {
                    if (PositionIntersects(delta, elementals[i]))
                    {
                        intersectionHappens = true;

                        // Expecting that width >= height && width == radius always
                        double circleDistance = Math.Sqrt(Math.Pow((elementals[i].pos.X - pos.X), 2) + Math.Pow((elementals[i].pos.Y - pos.Y), 2)
                            - (elementals[i].width + width) / 2);
                        tempVelocity = Vector2.Reflect(velocity, elementals[i].pos - pos);
                        tempVelocity.Normalize();
                        tempVelocity *= (float)circleDistance;

                        bool noIntersection = true;
                        for (int j = 0; j < elementals.Count; j++)
                        {
                            if (!this.Equals(elementals[j]))
                            {
                                if (PositionIntersects(tempVelocity, elementals[j]))
                                {
                                    noIntersection = false;
                                    break;
                                }
                            }
                        }
                        if (noIntersection) break;
                    }
                }
            }
            if (!intersectionHappens) return velocity;

            return tempVelocity;
        }

        private bool PositionIntersects(Vector2 delta, Elemental e)
        {
            return Vector2.DistanceSquared(pos + delta, e.pos) <= Math.Pow((width + e.width) / 2, 2);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (!visible)
                return;

            if(!created) {
                creationAnimation.Draw(spriteBatch, pos, Color.White);
            } else {
                animation.Draw(spriteBatch, pos, Element.GetColor(this.element));
            }
        }

        #region Properties
        public Elements CurrentElement
        {
            get { return element; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public Vector2 Position
        {
            get { return pos; }
        }

        public bool Created
        {
            get { return created; }
        }

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public float MoveSpeed
        {
            get { return 5.0f / speed + speed / 3.0f; }
        }
        #endregion
    }
}
