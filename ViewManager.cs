using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MelSpaceHunter
{
    /// <summary>
    /// A class that handles the metrics of the screen and objects
    /// </summary>
    class ViewManager
    {
        private int width, height;

        public ViewManager(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public float RelativeX(float x)
        {
            return this.width * x / 100.0f;
        }

        public float RelativeY(float y)
        {
            return this.height * y / 100.0f;
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }
    }
}
