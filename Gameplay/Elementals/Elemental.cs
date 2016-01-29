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
        private Vector2 pos;
        private Texture2D texture;
        private bool born;
        private int width, height;

        // Stats
        private int attack, defense, speed, health, maxHealth;

        public Elemental(Elements element, Vector2 pos, Texture2D texture, int width, int height, int attack, int defense, int speed, int health)
        {
            this.born = false;
            this.element = element;
            this.pos = pos;
            this.texture = texture;
            this.width = width;
            this.height = height;
            this.attack = attack;
            this.defense = defense;
            this.speed = speed;
            this.health = this.maxHealth = health;
        }

        public Elemental Clone(Vector2 newPosition)
        {
            return new Elemental(element, newPosition, texture, width, height, attack, defense, speed, maxHealth);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
