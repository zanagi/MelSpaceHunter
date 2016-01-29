using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MelSpaceHunter.Gameplay;
using MelSpaceHunter.Gameplay.Elementals;

namespace MelSpaceHunter.Screens
{
    class GameScreen : Screen
    {
        private InputManager inputManager;
        private BackgroundManager backgroundManager;
        private Camera2D camera;

        private Character character;
        private ElementalManager elementalManager;

        public GameScreen(ScreenManager manager)
            : base(manager)
        {
            this.inputManager = new InputManager();
            this.backgroundManager = new BackgroundManager("background", manager.ViewManager.Width, manager.ViewManager.Height);
            this.camera = new Camera2D(manager.ViewManager.Width, manager.ViewManager.Height);

            this.character = new Character(new Vector2(camera.X, camera.Y));
            this.elementalManager = new ElementalManager();
        }

        public override void LoadContent(ContentManager contentRef)
        {
            base.LoadContent(contentRef);

            backgroundManager.LoadContent(content);
            character.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update();

            character.Update(gameTime, inputManager, elementalManager.GetVisibleElementals());
            camera.MoveTo((int)character.Position.X, (int)character.Position.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        camera.TransformMatrix);

            backgroundManager.Draw(spriteBatch, camera.X, camera.Y);
            character.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
