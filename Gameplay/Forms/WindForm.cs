﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MelSpaceHunter.Gameplay.Elementals;

namespace MelSpaceHunter.Gameplay.Forms
{
    class WindForm : Form
    {
        public WindForm(string path, int width, int height)
            : base(path, Elements.Wind, width, height)
        {

        }

        public override void Update(GameTime gameTime, List<Elemental> elementals, int attack, int defense, int stamina)
        {
            base.Update(gameTime, elementals, attack, defense, stamina);
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
