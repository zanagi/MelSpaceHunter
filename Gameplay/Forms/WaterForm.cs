using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MelSpaceHunter.Gameplay.Elementals;

namespace MelSpaceHunter.Gameplay.Forms
{
    class WaterForm : Form
    {
        public WaterForm(string path, int width, int height)
            : base(path, Elements.Water, width, height)
        {

        }

        public override void Update(GameTime gameTime, List<Elemental> elementals)
        {

        }

        public override float StaminaModifier
        {
            get
            {
                return 1.5f;
            }
        }
    }
}
