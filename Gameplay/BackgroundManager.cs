using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MelSpaceHunter.Gameplay
{
    class BackgroundManager
    {
        private string path;
        private int width, height;
        private Texture2D background;

        public BackgroundManager(string path, int width, int height)
        {
            this.path = path;
            this.width = width;
            this.height = height;
        }

        public void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(path);
        }

        public void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            int tempX = (x >= 0) ? x : (x - width); // Cheat to in order to handle calculations correctly
            int tempY = (y >= 0) ? y : (y - height); // Cheat to in order to handle calculations correctly

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    Rectangle rect = new Rectangle((tempX / width + i) * width, (tempY / height + j) * height, width, height);
                    spriteBatch.Draw(background, rect, Color.White);
                }
            }
        }
    }
}
