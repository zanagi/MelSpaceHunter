using System;
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
        private int frameWidth, frameHeight, sourceFrameWidth, sourceFrameHeight, currentFrame, frameColumns, frameRows, frameCount;
        private double frameTime, currentTime;
        private Texture2D texture;
        private bool loopedOnce;

        public Animation(string path, int frameColumns, int frameRows, int frameWidth, int frameHeight, double frameTime = 100)
        {
            this.path = path;
            this.frameColumns = frameColumns;
            this.frameRows = frameRows;
            this.frameCount = frameRows * frameColumns;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.currentFrame = 0;
            this.frameTime = frameTime;
            this.currentTime = 0;
            this.loopedOnce = false;
        }

        public Animation Clone()
        {
            Texture2D temp = texture;

            return new Animation(path, frameColumns, frameRows, frameWidth, frameHeight, frameTime)
            {
                texture = temp,
                sourceFrameWidth = texture.Width / frameColumns,
                sourceFrameHeight = texture.Height / frameRows
            };
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(path);
            sourceFrameWidth = texture.Width / frameColumns;
            sourceFrameHeight = texture.Height / frameRows;
        }

        public virtual void Update(GameTime gameTime)
        {
            this.currentTime += gameTime.ElapsedGameTime.Milliseconds;

            if (currentTime > frameTime)
            {
                int frameChange = (int)(currentTime / frameTime);
                currentFrame = (currentFrame + frameChange) % frameCount;
                currentTime -= frameChange * frameTime;

                if (currentFrame == 0)
                    loopedOnce = true;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 origin)
        {
            spriteBatch.Draw(texture, GetDestinationRectangle(origin), GetSourceRectangle(), Color.White);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 origin, Color color)
        {
            spriteBatch.Draw(texture, GetDestinationRectangle(origin), GetSourceRectangle(), color);
        }

        public virtual void DrawFrame(SpriteBatch spriteBatch, int originX, int originY, int width, int height, int frame)
        {
            Rectangle dest = new Rectangle(originX - width / 2, originY - height / 2, width, height);
            spriteBatch.Draw(texture, dest, GetSourceRectangle(frame), Color.White);
        }

        private Rectangle GetDestinationRectangle(Vector2 origin)
        {
            return new Rectangle((int)origin.X - frameWidth / 2, (int)origin.Y - frameHeight / 2, frameWidth, frameHeight);
        }

        private Rectangle GetSourceRectangle()
        {
            return new Rectangle(currentFrame % frameColumns * sourceFrameWidth, currentFrame / frameColumns * sourceFrameHeight,
                sourceFrameWidth, sourceFrameHeight);
        }

        private Rectangle GetSourceRectangle(int frame)
        {
            return new Rectangle(frame % frameColumns * sourceFrameWidth, frame / frameColumns * sourceFrameHeight,
                sourceFrameWidth, sourceFrameHeight);
        }

        public int TargetWidth
        {
            get { return frameWidth; }
            set { frameWidth = value; }
        }

        public int TargetHeight
        {
            get { return frameHeight; }
            set { frameHeight = value; }
        }

        public bool LoopedOnce
        {
            get { return loopedOnce; }
        }
    }
}
