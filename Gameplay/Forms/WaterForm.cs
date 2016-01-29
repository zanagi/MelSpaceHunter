using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MelSpaceHunter.Gameplay.Forms
{
    class WaterForm : Form
    {
        public WaterForm(string path)
            : base(path, Elements.Water)
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
