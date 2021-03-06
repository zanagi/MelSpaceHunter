﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using MelSpaceHunter.Screens;
using MelSpaceHunter.Effects;
#endregion

namespace MelSpaceHunter
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ScreenManager screenManager;
        private float targetAspectRatio;

        private bool viewportUpdated;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            // graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            // Resulution calc
            targetAspectRatio = 16.0f / 9;

            base.Initialize();

            // Game-related initializations
            Helper.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            screenManager = new ScreenManager(Content, new ViewManager(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));

            // Initialize static classes
            Texture2D numberSheet = Content.Load<Texture2D>("numbers");
            NumberDrawer.Initialize(numberSheet);

            EffectManager.Initialize();

            Texture2D infoBarBackground = Content.Load<Texture2D>("InfoBar/infoBar");
            Texture2D startText = Content.Load<Texture2D>("InfoBar/startText");
            Texture2D transformText = Content.Load<Texture2D>("InfoBar/transformText");
            InfoBar.Initialize(infoBarBackground, 0, screenManager.ViewManager.RelativeY(4),
                screenManager.ViewManager.Width, screenManager.ViewManager.RelativeY(4));
            InfoBar.AddPair("start", startText);
            InfoBar.AddPair("transform", transformText);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            InputManager.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || !screenManager.HasScreen)
                Exit();

            if (InputManager.KeyTapped(Keys.F5))
            {
                graphics.IsFullScreen = !graphics.IsFullScreen;
                graphics.ApplyChanges();
            }

            if (!viewportUpdated || !this.IsActive)
                return;

            screenManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Viewport = new Viewport
            {
                X = 0,
                Y = 0,
                Width = GraphicsDevice.PresentationParameters.BackBufferWidth,
                Height = GraphicsDevice.PresentationParameters.BackBufferHeight,
                MinDepth = 0,
                MaxDepth = 1
            };

            GraphicsDevice.Clear(Color.Black);

            int width = GraphicsDevice.PresentationParameters.BackBufferWidth;
            int height = (int)(width / targetAspectRatio + .5f);
            if (height > GraphicsDevice.PresentationParameters.BackBufferHeight)
            {
                height = GraphicsDevice.PresentationParameters.BackBufferHeight;
                width = (int)(height * targetAspectRatio + .5f);
            }

            GraphicsDevice.Viewport = new Viewport
            {
                X = GraphicsDevice.PresentationParameters.BackBufferWidth / 2 - width / 2,
                Y = GraphicsDevice.PresentationParameters.BackBufferHeight / 2 - height / 2,
                Width = width,
                Height = height,
                MinDepth = 0,
                MaxDepth = 1
            };

            if (!viewportUpdated)
            {
                screenManager.UpdateView(width, height);
                viewportUpdated = true;
            }
            screenManager.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
