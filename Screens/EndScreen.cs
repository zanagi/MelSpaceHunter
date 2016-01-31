using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MelSpaceHunter.Screens
{
    class EndScreen : Screen
    {
        private Texture2D background;
        private int score, timer, timeLimit;

        public EndScreen(ScreenManager manager, int score)
            : base(manager)
        {
            this.score = score;
            this.timer = 0;
            this.timeLimit = 3000;
        }

        public override void LoadContent(ContentManager contentRef)
        {
            base.LoadContent(contentRef);

            background = content.Load<Texture2D>("endBg");
        }

        public void Update(GameTime gameTime)
        {
            if (timer < timeLimit)
                timer += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer >= timeLimit)
            {
                if (inputManager.MouseClicked())
                {
                    manager.PopScreen();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, manager.ViewManager.Width, manager.ViewManager.Height), Color.White);

            // TODO: Display score
        }
    }
}
