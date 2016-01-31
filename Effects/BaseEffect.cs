using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MelSpaceHunter.Effects
{
    abstract class BaseEffect
    {
        public virtual void LoadContent(ContentManager content){}

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        public virtual bool Complete
        {
            get { return false; }
        }
    }
}
