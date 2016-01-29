using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MelSpaceHunter.Gameplay.Forms
{
    class WindForm : Form
    {
        public WindForm(string path, int width, int height)
            : base(path, Elements.Wind, width, height)
        {

        }

        public override float SpeedModifier
        {
            get
            {
                return 1.5f;
            }
        }
    }
}
