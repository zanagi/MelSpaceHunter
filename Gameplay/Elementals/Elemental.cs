﻿using System;
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

            Move(characterPosition);

            // TODO: Collision detection & handling
        }

        private void Move(Vector2 characterPosition)
        {
            switch (movement)
            {
                // TODO:
                case Movements.Straight:
                    break;
                case Movements.Circling:
                    break;
                case Movements.Shaking:
                    break;
                case Movements.Following:
                    break;
            }
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
        #endregion
    }
}
