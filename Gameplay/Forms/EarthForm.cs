using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MelSpaceHunter.Gameplay.Forms
{
    class EarthForm : Form
    {
        public EarthForm(string path, int width, int height)
            : base(path, Elements.Earth, width, height)
        {

        }

        public override float DefenseModifier
        {
            get
            {
                return 1.5f;
            }
        }
    }
}
