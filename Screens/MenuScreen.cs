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
        public MenuScreen(ScreenManager manager)
            : base(manager)
        {

        }

        public override void LoadContent(ContentManager contentRef)
        {
            base.LoadContent(contentRef);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (inputManager.MouseClicked())
            {
                manager.PushScreen(new GameScreen(manager));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
