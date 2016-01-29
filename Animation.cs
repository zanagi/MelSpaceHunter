﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MelSpaceHunter
{
    class Animation
    {
        private string path;
        private int frameWidth, frameHeight, currentFrame, frameColumns, frameRows, frameCount;
        private double frameTime, currentTime;
        private Texture2D texture;

        public Animation(string path, int frameColumns, int frameRows, double frameTime = 100)
        {
            this.path = path;
            this.frameColumns = frameColumns;
            this.frameRows = frameRows;
            this.frameCount = frameRows * frameColumns;
            this.currentFrame = 0;
            this.frameTime = frameTime;
            this.currentTime = 0;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(path);
            frameWidth = texture.Width / frameColumns;
            frameHeight = texture.Height / frameRows;
        }

        public virtual void Update(GameTime gameTime)
        {
            this.currentTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (currentTime > frameTime)
            {
                int frameChange = (int)(currentTime / frameTime);
                currentFrame = (currentFrame + frameChange) % frameCount;
                currentTime -= frameChange * frameTime;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 origin)
        {
            Rectangle destinationRect = new Rectangle((int)origin.X - frameWidth / 2, (int)origin.Y - frameHeight / 2, frameWidth, frameHeight);
            Rectangle sourceRect = new Rectangle(currentFrame % frameColumns * frameWidth, currentFrame / frameColumns * frameHeight, frameWidth, frameHeight);
            spriteBatch.Draw(texture, destinationRect, sourceRect, Color.White);
        }
    }
}