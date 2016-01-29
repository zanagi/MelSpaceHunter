using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MelSpaceHunter.Gameplay.Forms;

namespace MelSpaceHunter.Gameplay
{
    class Character
    {
        private Vector2 pos;

        // Form-related
        private Form form;
        private int firePoints, waterPoints, windPoints, earthPoints, maxElementPoints;

        // Character Parameters
        private int attack, defence, energyConsumption, speed, maxStatusValue; 

        public Character(Vector2 pos)
        {
            this.pos = pos;
        }

        public bool CanTransform(Elements element)
        {
            switch (element) {
                case Elements.Fire:
                    return firePoints == maxElementPoints;
                case Elements.Water:
                    return waterPoints == maxElementPoints;
                case Elements.Wind:
                    return windPoints == maxElementPoints;
                case Elements.Earth:
                    return earthPoints == maxElementPoints;
            }
            return false;
        }
    }
}
