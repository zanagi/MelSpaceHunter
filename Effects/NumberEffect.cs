using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelSpaceHunter.Effects
{
    class NumberEffect : BaseEffect
    {
        private int elapsedTime, number, duration, width, height;
        private Vector2 pos, velocity;
        private Color color;
        private bool fade;
        
        public NumberEffect(int number, int duration, int digitWidth, int digitHeight, Vector2 pos, Vector2 velocity, Color color, bool fade = true)
        {
            this.elapsedTime = 0;
            this.number = number;
            this.duration = duration;
            this.width = digitWidth;
            this.height = digitHeight;
            this.pos = pos;
            this.velocity = velocity;
            this.color = color;
            this.fade = fade;
        }

        public override void Update(GameTime gameTime)
        {
            if(elapsedTime >= duration)
                return;
            
            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            pos += velocity;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            NumberDrawer.DrawNumber(spriteBatch, number, new Rectangle((int)pos.X, (int)pos.Y, width, height), 
                (fade) ? color * Math.Max(1.0f - 1.0f * elapsedTime / duration, 0.0f) : color);
        }

        public override bool Complete
        {
            get { return elapsedTime >= duration; }
        }
    }
}
