using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MelSpaceHunter.Screens
{
    class MenuScreen : Screen
    {
        private Texture2D background;
        private Rectangle rect;

        public MenuScreen(ScreenManager manager)
            : base(manager)
        {
            this.rect = new Rectangle(0, 0, manager.ViewManager.Width, manager.ViewManager.Height);
        }

        public override void LoadContent(ContentManager contentRef)
        {
            base.LoadContent(contentRef);

            background = content.Load<Texture2D>("menuBg");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.MouseClicked() || InputManager.KeyTapped(Microsoft.Xna.Framework.Input.Keys.Enter))
            {
                manager.PushScreen(new GameScreen(manager));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, rect, Color.White);
            spriteBatch.End();
        }
    }
}
