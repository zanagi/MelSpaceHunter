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
        private Rectangle rect;

        public EndScreen(ScreenManager manager, int score)
            : base(manager)
        {
            this.score = score;
            this.timer = 0;
            this.timeLimit = 3000;
            this.rect = new Rectangle(manager.ViewManager.RelativeX(60), manager.ViewManager.RelativeY(58.0f),
                manager.ViewManager.RelativeX(10.0f / 3), manager.ViewManager.RelativeX(5));
        }

        public override void LoadContent(ContentManager contentRef)
        {
            base.LoadContent(contentRef);

            background = content.Load<Texture2D>("endBg");
        }

        public override void Update(GameTime gameTime)
        {
            if (timer < timeLimit)
                timer += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer >= timeLimit)
            {
                if (InputManager.MouseClicked())
                {
                    manager.PopScreen();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, manager.ViewManager.Width, manager.ViewManager.Height), Color.White);
            NumberDrawer.DrawNumber(spriteBatch, score, rect, Color.Black);
            // TODO: Display score
            spriteBatch.End();
        }
    }
}
