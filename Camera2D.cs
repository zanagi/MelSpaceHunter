using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MelSpaceHunter
{
    //Camera class for scrolling around the screens
    class Camera2D
    {
        private int x, y, width, height;
        private float zoom, rotation;

        public Camera2D(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.x = width / 2;
            this.y = height / 2;
            zoom = 1.0f;
            rotation = 0;
        }

        public float Zoom
        {
            get { return zoom; }
            set { zoom = value; if (zoom < 0.1f) zoom = 0.1f; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        /// <summary>
        /// Sets the camera center values
        /// </summary>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        public void MoveTo(int newX, int newY)
        {
            x = newX;
            y = newY;
        }

        public Matrix TransformMatrix
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-X, -Y, 0)) *
                                        Matrix.CreateRotationZ(Rotation) *
                                        Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                        Matrix.CreateTranslation(new Vector3(width * 0.5f, height * 0.5f, 0));
            }
        }
    }
}
