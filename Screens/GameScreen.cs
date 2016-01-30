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

        // TODO: Help screem
        private bool inHelpScreen;

        public GameScreen(ScreenManager manager)
            : base(manager)
        {
            this.inputManager = new InputManager();
            this.backgroundManager = new BackgroundManager("background", manager.ViewManager.Width, manager.ViewManager.Height);
            this.camera = new Camera2D(manager.ViewManager.Width, manager.ViewManager.Height);

            int characterBaseWh = manager.ViewManager.RelativeY(10);
            this.character = new Character(new Vector2(camera.X, camera.Y), characterBaseWh, characterBaseWh);

            int elementalBaseWH = manager.ViewManager.RelativeY(8);
            this.elementalManager = new ElementalManager(elementalBaseWH, elementalBaseWH);

            // TODO: Help screen
            this.inHelpScreen = false;
        }

        public override void LoadContent(ContentManager contentRef)
        {
            base.LoadContent(contentRef);

            backgroundManager.LoadContent(content);
            elementalManager.LoadContent(content);
            character.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update();

            if (inputManager.KeyTapped(Keys.Escape))
            {
                inHelpScreen = !inHelpScreen;
            }

            if (inHelpScreen)
                return;

            
            character.Update(gameTime, inputManager, elementalManager.GetElementals());
            elementalManager.Update(gameTime, character, manager.ViewManager);
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
            elementalManager.Draw(spriteBatch);
            character.Draw(spriteBatch);

            // TODO: Draw helpscreen

            spriteBatch.End();
        }
    }
}
