using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MelSpaceHunter.Gameplay.Forms
{
    class EarthForm : Form
    {
        public EarthForm(string path)
            : base(path, Elements.Earth)
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
