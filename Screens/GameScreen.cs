using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MelSpaceHunter.Gameplay;

namespace MelSpaceHunter.Screens
{
    class GameScreen : Screen
    {
        private InputManager inputManager;
        private BackgroundManager backgroundManager;
        private Camera2D camera;

        // Test
        private Vector2 test;

        public GameScreen(ScreenManager manager)
            : base(manager)
        {
            this.inputManager = new InputManager();
            this.backgroundManager = new BackgroundManager("background", manager.ViewManager.Width, manager.ViewManager.Height);
            this.camera = new Camera2D(manager.ViewManager.Width, manager.ViewManager.Height);

            test = new Vector2(camera.X, camera.Y);
        }

        public override void LoadContent(ContentManager contentRef)
        {
            base.LoadContent(contentRef);

            backgroundManager.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update();

            // TEST:
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                test.Y -= 3;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                test.Y += 3;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                test.X += 3;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                test.X -= 3;
            }
            camera.MoveTo((int)test.X, (int)test.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        camera.TransformMatrix);

            backgroundManager.Draw(spriteBatch, camera.X, camera.Y);

            spriteBatch.End();
        }
    }
}
