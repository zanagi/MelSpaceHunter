using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MelSpaceHunter.Gameplay.Forms
{
    class FireForm : Form
    {
        public FireForm(string path)
            : base(path, Elements.Fire)
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
