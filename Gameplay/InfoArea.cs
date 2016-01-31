using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MelSpaceHunter.Gameplay
{
    class InfoArea
    {
        private Texture2D infoZoneTexture, characterCircle, fireIcon, earthIcon, waterIcon, windIcon, barTexture,
            attackIcon, staminaIcon, defenseIcon, speedIcon;
        private Rectangle infoZoneRect, characterCircleRect, hpBarRect, expBarRect, fireIconRect, waterIconRect, earthIconRect, windIconRect,
            attackIconRect, staminaIconRect, defenseIconRect, speedIconRect;

        public InfoArea(ViewManager viewManager)
        {
            infoZoneRect = new Rectangle(0, viewManager.Height - viewManager.RelativeX(17.5f), viewManager.RelativeX(25), viewManager.RelativeX(17.5f));
            characterCircleRect = new Rectangle(viewManager.RelativeX(3.5f), viewManager.Height - viewManager.RelativeX(13.5f),
                viewManager.RelativeX(10), viewManager.RelativeX(10));
            expBarRect = new Rectangle(characterCircleRect.X + characterCircleRect.Width / 2,
                characterCircleRect.Y + characterCircleRect.Height - viewManager.RelativeX(1), viewManager.RelativeX(25), viewManager.RelativeX(1));
            hpBarRect = new Rectangle(characterCircleRect.X + (int)(0.7f * characterCircleRect.Width), expBarRect.Y - viewManager.RelativeX(2),
                expBarRect.Width - (int)(0.20f * characterCircleRect.Width), viewManager.RelativeX(2));


            int iconWidth = viewManager.RelativeX(3.5f);
            int iconMargin = viewManager.RelativeX(0.1f);
            this.fireIconRect = new Rectangle(characterCircleRect.X + (characterCircleRect.Width - iconWidth) / 2,
                characterCircleRect.Y - iconWidth - iconMargin, iconWidth, iconWidth);
            this.waterIconRect = new Rectangle(characterCircleRect.X + characterCircleRect.Width + iconMargin,
                characterCircleRect.Y + (characterCircleRect.Height - iconWidth) / 2, iconWidth, iconWidth);
            this.earthIconRect = new Rectangle(characterCircleRect.X + (characterCircleRect.Width - iconWidth) / 2,
                characterCircleRect.Y + characterCircleRect.Height + iconMargin, iconWidth, iconWidth);
            this.windIconRect = new Rectangle(characterCircleRect.X - iconWidth - iconMargin,
                characterCircleRect.Y + (characterCircleRect.Height - iconWidth) / 2, iconWidth, iconWidth);
        }

        public void LoadContent(ContentManager content)
        {
            infoZoneTexture = content.Load<Texture2D>("CharacterInfo/infoZoneBackground");
            characterCircle = content.Load<Texture2D>("CharacterInfo/baseCircle");
            fireIcon = content.Load<Texture2D>("CharacterInfo/fireIcon");
            waterIcon = content.Load<Texture2D>("CharacterInfo/waterIcon");
            earthIcon = content.Load<Texture2D>("CharacterInfo/earthIcon");
            windIcon = content.Load<Texture2D>("CharacterInfo/windIcon");
            barTexture = content.Load<Texture2D>("CharacterInfo/emptyBar");
        }

        public void Draw(SpriteBatch spriteBatch, Character character)
        {
            spriteBatch.Draw(infoZoneTexture, infoZoneRect, Color.White);

            // Character circle && HP / Exp bar
            spriteBatch.Draw(barTexture, expBarRect, Color.White);
            spriteBatch.Draw(barTexture,
                new Rectangle(expBarRect.X, expBarRect.Y, (int)(expBarRect.Width * character.ExperienceRatio), expBarRect.Height), Color.Gold);
            spriteBatch.Draw(barTexture, hpBarRect, Color.Red);
            spriteBatch.Draw(barTexture, new Rectangle(hpBarRect.X, hpBarRect.Y, (int)(hpBarRect.Width * character.HealthRatio), hpBarRect.Height),
                Color.Green);
            spriteBatch.Draw(characterCircle, characterCircleRect, Color.White);

            // Icons
            // TODO: Grey base icons
            float fireRatio = character.FireRatio; float waterRatio = character.WaterRatio;
            float earthRatio = character.EarthRatio; float windRatio = character.WindRatio;
            spriteBatch.Draw(fireIcon, fireIconRect, Color.SlateGray);
            spriteBatch.Draw(fireIcon,
                    new Rectangle(fireIconRect.X, fireIconRect.Y + (int)((1.0f - fireRatio) * fireIconRect.Height),
                    fireIconRect.Width, (int)(fireIconRect.Height * fireRatio)),
                    new Rectangle(0, (int)((1.0f - fireRatio) * fireIcon.Height), fireIcon.Width, (int)(fireRatio * fireIcon.Height)), Color.White);
            spriteBatch.Draw(waterIcon, waterIconRect, Color.SlateGray);
            spriteBatch.Draw(waterIcon,
                    new Rectangle(waterIconRect.X, waterIconRect.Y + (int)((1.0f - waterRatio) * waterIconRect.Height),
                    waterIconRect.Width, (int)(waterIconRect.Height * waterRatio)),
                    new Rectangle(0, (int)((1.0f - waterRatio) * waterIcon.Height), waterIcon.Width, (int)(waterRatio * waterIcon.Height)), Color.White);
            spriteBatch.Draw(earthIcon, earthIconRect, Color.SlateGray);
            spriteBatch.Draw(earthIcon,
                    new Rectangle(earthIconRect.X, earthIconRect.Y + (int)((1.0f - earthRatio) * earthIconRect.Height),
                    earthIconRect.Width, (int)(earthIconRect.Height * earthRatio)),
                    new Rectangle(0, (int)((1.0f - earthRatio) * earthIcon.Height), earthIcon.Width, (int)(earthRatio * earthIcon.Height)), Color.White);
            spriteBatch.Draw(windIcon, windIconRect, Color.SlateGray);
            spriteBatch.Draw(windIcon,
                    new Rectangle(windIconRect.X, windIconRect.Y + (int)((1.0f - windRatio) * windIconRect.Height),
                    windIconRect.Width, (int)(windIconRect.Height * windRatio)),
                    new Rectangle(0, (int)((1.0f - windRatio) * windIcon.Height), windIcon.Width, (int)(windRatio * windIcon.Height)), Color.White);

            // Character Avatar
            character.DrawStill(spriteBatch, characterCircleRect.Center.X, characterCircleRect.Center.Y, 
                characterCircleRect.Width * 8 / 10, characterCircleRect.Height * 8 / 10);
        }
    }
}
