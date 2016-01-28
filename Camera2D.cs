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
        private Vector2 center;
        private Vector2 screen;
        private float zoom, rotation;

        public Camera2D(float width, float height)
        {
            screen = new Vector2(width, height);
            center = new Vector2(screen.X / 2, screen.Y / 2);
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


        //Makes sure the camera won't go over the bounds, if defined
        public void Move(Vector2 delta)
        {
            center += delta;
        }

        public Matrix TransformMatrix
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0)) *
                                        Matrix.CreateRotationZ(Rotation) *
                                        Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                        Matrix.CreateTranslation(new Vector3(screen.X * 0.5f, screen.Y * 0.5f, 0));
            }
        }
    }
}
