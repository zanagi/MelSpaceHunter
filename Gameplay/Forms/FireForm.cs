using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MelSpaceHunter.Gameplay.Forms
{
    class FireForm : Form
    {
        public FireForm(string path, int width, int height)
            : base(path, Elements.Fire, width, height)
        {

        }

        public override float AttackModifier
        {
            get
            {
                return 1.2f;
            }
        }
    }
}
