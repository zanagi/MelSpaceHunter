using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MelSpaceHunter.Gameplay.Forms
{
    class WaterForm : Form
    {
        public WaterForm(string path, int width, int height)
            : base(path, Elements.Water, width, height)
        {

        }

        public override float EnergyConsumptionModifier
        {
            get
            {
                return 1.5f;
            }
        }
    }
}
