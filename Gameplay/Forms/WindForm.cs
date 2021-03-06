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

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            // TODO: animation
        }

        public override void Update(GameTime gameTime, List<Elemental> elementals, Character character)
        {
            base.Update(gameTime, elementals, character);
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
