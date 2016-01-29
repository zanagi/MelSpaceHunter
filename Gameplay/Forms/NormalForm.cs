using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MelSpaceHunter.Gameplay.Elementals;

namespace MelSpaceHunter.Gameplay.Forms
{
    class NormalForm : Form
    {
        public NormalForm(string path, int width, int height)
            : base(path, Elements.None, width, height)
        {

        }
    }
}
