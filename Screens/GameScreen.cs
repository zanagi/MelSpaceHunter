﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MelSpaceHunter.Gameplay;
using MelSpaceHunter.Gameplay.Elementals;
using MelSpaceHunter.Effects;

namespace MelSpaceHunter.Screens
{
    class GameScreen : Screen
    {
        private BackgroundManager backgroundManager;

        private Character character;
        private ElementalManager elementalManager;
        private Radar radar;
        private InfoArea infoArea;

        // TODO: Help screem
        private bool inHelpScreen, startInfoDone, transformInfoDone;

        private Texture2D help;

        public GameScreen(ScreenManager manager)
            : base(manager)
        {
            this.backgroundManager = new BackgroundManager("grassBg", manager.ViewManager.Width, manager.ViewManager.Height);

            int characterBaseWh = manager.ViewManager.RelativeY(10);
            this.character = new Character(new Vector2(camera.X, camera.Y), characterBaseWh, characterBaseWh);

            int elementalBaseWH = manager.ViewManager.RelativeY(8);
            this.elementalManager = new ElementalManager(elementalBaseWH, elementalBaseWH);

            this.radar = new Radar("radar", "radar_dot",
                manager.ViewManager.RelativeX(90), manager.ViewManager.Height - manager.ViewManager.RelativeX(10),
                manager.ViewManager.RelativeX(8), manager.ViewManager.Width);
            this.infoArea = new InfoArea(manager.ViewManager);

            // TODO: Help screen
            this.inHelpScreen = this.startInfoDone = this.transformInfoDone = false;
        }

        public override void LoadContent(ContentManager contentRef)
        {
            base.LoadContent(contentRef);

            backgroundManager.LoadContent(content);
            elementalManager.LoadContent(content);
            radar.LoadContent(content);
            infoArea.LoadContent(content);
            character.LoadContent(content);

            help = content.Load<Texture2D>("help");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!startInfoDone)
            {
                InfoBar.ActivateInfo("start"); startInfoDone = true;
            }
            if (!transformInfoDone && character.CanTransform())
            {
                InfoBar.ActivateInfo("transform"); transformInfoDone = true;
            }

            if (InputManager.KeyTapped(Keys.H))
            {
                inHelpScreen = !inHelpScreen;
            }
            if (inHelpScreen)
                return;

            character.Update(gameTime, inputManager, elementalManager.GetElementals());
            elementalManager.Update(gameTime, character, manager.ViewManager);
            camera.MoveTo((int)character.Position.X, (int)character.Position.Y);

            EffectManager.Update(gameTime);

            if (character.CurrentHealth <= 0)
            {
                manager.PopScreen();
                manager.PushScreen(new EndScreen(manager, character.Score));
            }
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
            elementalManager.Draw(spriteBatch);

            // Draw effects
            EffectManager.Draw(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            // Draw radar & info zone
            DrawRadar(spriteBatch);
            infoArea.Draw(spriteBatch, character);

            // Infobar
            InfoBar.Draw(spriteBatch);

            if (inHelpScreen)
            {
                spriteBatch.Draw(help, new Rectangle(manager.ViewManager.RelativeX(30), 0,
                    manager.ViewManager.RelativeX(40), manager.ViewManager.Height), Color.White);
            }

            spriteBatch.End();
        }

        private void DrawRadar(SpriteBatch spriteBatch)
        {
            radar.Draw(spriteBatch);

            List<Elemental> elementals = elementalManager.GetElementals();
            for (int i = 0; i < elementals.Count; i++)
            {
                radar.DrawDot(spriteBatch, elementals[i].Position - character.Position, Element.GetColor(elementals[i].CurrentElement));
            }
        }
    }
}
