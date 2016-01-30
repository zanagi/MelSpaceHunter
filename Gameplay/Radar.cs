using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MelSpaceHunter.Gameplay
{
    class Radar
    {
        private Texture2D texture, dotTexture;
        private Rectangle rect;

        private string path, dotPath;
        private int textureRadius, detectionRadius;


        public Radar(string path, string dotPath, int x, int y, int textureRadius, int detectionRadius)
        {
            this.path = path;
            this.dotPath = dotPath;
            this.rect = new Rectangle(x - textureRadius, y - textureRadius, textureRadius * 2, textureRadius * 2);
            this.textureRadius = textureRadius;
            this.detectionRadius = detectionRadius;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(path);
            dotTexture = content.Load<Texture2D>(dotPath);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw radar
            spriteBatch.Draw(texture, rect, Color.White);
        }

        public void DrawDot(SpriteBatch spriteBatch, Vector2 locationDelta, Color color)
        {
            if (locationDelta.LengthSquared() < detectionRadius * detectionRadius)
            {
                spriteBatch.Draw(dotTexture,
                        new Rectangle(
                            rect.Center.X + (int)(locationDelta.X * 0.88f * textureRadius / detectionRadius),
                            rect.Center.Y + (int)(locationDelta.Y * 0.88f * textureRadius / detectionRadius),
                            (int)(rect.Width * 0.05f), (int)(rect.Height * 0.05f)), color);
            }
        }
    }
}
