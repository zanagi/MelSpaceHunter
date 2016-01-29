using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MelSpaceHunter.Gameplay.Forms
{
    abstract class Form
    {
        protected Animation animation;

        public Form(string path)
        {
            this.animation = new Animation(path, 4, 1);
        }

        public virtual void LoadContent(ContentManager content)
        {
            animation.LoadContent(content);
        }

        public virtual void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 origin)
        {
            animation.Draw(spriteBatch, origin);
        }
    }
}
