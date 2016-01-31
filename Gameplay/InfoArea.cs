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

        // Stat rects
        private Rectangle attackStatRect, staminaStatRect, defenseStatRect, speedStatRect, healthStatRect;

        public InfoArea(ViewManager viewManager)
        {
            infoZoneRect = new Rectangle(0, viewManager.Height - viewManager.RelativeX(17.5f), viewManager.RelativeX(25), viewManager.RelativeX(17.5f));
            characterCircleRect = new Rectangle(viewManager.RelativeX(3.5f), viewManager.Height - viewManager.RelativeX(11f),
                viewManager.RelativeX(10), viewManager.RelativeX(10));
            expBarRect = new Rectangle(characterCircleRect.X + characterCircleRect.Width / 2,
                characterCircleRect.Y + characterCircleRect.Height - viewManager.RelativeX(1), viewManager.RelativeX(25), viewManager.RelativeX(1));
            hpBarRect = new Rectangle(characterCircleRect.X + (int)(0.7f * characterCircleRect.Width), expBarRect.Y - viewManager.RelativeX(2),
                expBarRect.Width - (int)(0.20f * characterCircleRect.Width), viewManager.RelativeX(2));

            // Element icons
            int iconWidth = viewManager.RelativeX(3.5f);
            int iconMargin = viewManager.RelativeX(0.1f);
            int radius = (characterCircleRect.Width + iconWidth) / 2 + iconMargin;
            this.fireIconRect =
                new Rectangle(-iconWidth / 2 + characterCircleRect.Center.X + (int)(radius * Math.Cos(Math.PI * 13 / 12)),
                    -iconWidth / 2 + characterCircleRect.Center.Y + (int)(radius * Math.Sin(Math.PI * 13 / 12)),
                    iconWidth, iconWidth);
            this.waterIconRect =
                new Rectangle(-iconWidth / 2 + characterCircleRect.Center.X + (int)(radius * Math.Cos(Math.PI * 49 / 36)),
                    -iconWidth / 2 + characterCircleRect.Center.Y + (int)(radius * Math.Sin(Math.PI * 49 / 36)),
                    iconWidth, iconWidth);
            this.earthIconRect =
                new Rectangle(-iconWidth / 2 + characterCircleRect.Center.X + (int)(radius * Math.Cos(Math.PI * 59 / 36)),
                    -iconWidth / 2 + characterCircleRect.Center.Y + (int)(radius * Math.Sin(Math.PI * 59 / 36)),
                    iconWidth, iconWidth);
            this.windIconRect =
                new Rectangle(-iconWidth / 2 + characterCircleRect.Center.X + (int)(radius * Math.Cos(Math.PI * 23 / 12)),
                    -iconWidth / 2 + characterCircleRect.Center.Y + (int)(radius * Math.Sin(Math.PI * 23 / 12)),
                    iconWidth, iconWidth);

            // Stat icons
            int statIconWidth = viewManager.RelativeX(2);
            int statIconMargin = viewManager.RelativeX(3);
            int baseY = infoZoneRect.Y + statIconMargin / 2;
            this.attackIconRect = new Rectangle(infoZoneRect.Right - statIconWidth - 4 * statIconMargin / 3, baseY,
                statIconWidth, statIconWidth);
            this.staminaIconRect = new Rectangle(infoZoneRect.Right - statIconWidth - 4 * statIconMargin / 3, baseY + statIconMargin,
                statIconWidth, statIconWidth);
            this.defenseIconRect = new Rectangle(infoZoneRect.Right - statIconWidth - 4 * statIconMargin / 3, baseY + 2 * statIconMargin,
                statIconWidth, statIconWidth);
            this.speedIconRect = new Rectangle(infoZoneRect.Right - statIconWidth - 4 * statIconMargin / 3, baseY + 3 * statIconMargin,
                statIconWidth, statIconWidth);

            // Stat number rects
            this.attackStatRect = new Rectangle(attackIconRect.Right + viewManager.RelativeX(0.5f), attackIconRect.Top,
                statIconWidth * 2 / 3, statIconWidth);
            this.staminaStatRect = new Rectangle(staminaIconRect.Right + viewManager.RelativeX(0.5f), staminaIconRect.Top,
                statIconWidth * 2 / 3, statIconWidth);
            this.defenseStatRect = new Rectangle(defenseIconRect.Right + viewManager.RelativeX(0.5f), defenseIconRect.Top,
                statIconWidth * 2 / 3, statIconWidth);
            this.speedStatRect = new Rectangle(speedIconRect.Right + viewManager.RelativeX(0.5f), speedIconRect.Top,
                statIconWidth * 2 / 3, statIconWidth);
            this.healthStatRect = new Rectangle(hpBarRect.Right - statIconWidth * 3, hpBarRect.Top - statIconWidth * 5 / 3, statIconWidth,
                statIconWidth * 3 / 2);
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
            attackIcon = content.Load<Texture2D>("CharacterInfo/attackIcon");
            staminaIcon = content.Load<Texture2D>("CharacterInfo/staminaIcon");
            defenseIcon = content.Load<Texture2D>("CharacterInfo/defenseIcon");
            speedIcon = content.Load<Texture2D>("CharacterInfo/speedIcon");
        }

        public void Draw(SpriteBatch spriteBatch, Character character)
        {
            spriteBatch.Draw(infoZoneTexture, infoZoneRect, Color.White);

            // Character circle && HP / Exp bar
            DrawBars(spriteBatch, character);
            spriteBatch.Draw(characterCircle, characterCircleRect, Color.White);
            character.DrawStill(spriteBatch, characterCircleRect.Center.X, characterCircleRect.Center.Y,
                characterCircleRect.Width * 8 / 10, characterCircleRect.Height * 8 / 10);

            // Icons
            // TODO: Grey base icons
            DrawElementIcons(spriteBatch, character);

            // StatIcons
            DrawStatIcons(spriteBatch, character);
        }

        private void DrawBars(SpriteBatch spriteBatch, Character character)
        {
            spriteBatch.Draw(barTexture, expBarRect, Color.White);
            spriteBatch.Draw(barTexture,
                new Rectangle(expBarRect.X, expBarRect.Y, (int)(expBarRect.Width * character.ExperienceRatio), expBarRect.Height), Color.Gold);
            spriteBatch.Draw(barTexture, hpBarRect, Color.Red);
            spriteBatch.Draw(barTexture, new Rectangle(hpBarRect.X, hpBarRect.Y, (int)(hpBarRect.Width * character.HealthRatio), hpBarRect.Height),
                Color.Green);
            NumberDrawer.DrawNumber(spriteBatch, character.CurrentHealth, healthStatRect, Color.Orange);
        }

        private void DrawElementIcons(SpriteBatch spriteBatch, Character character)
        {
            float fireRatio = character.FireRatio; float waterRatio = character.WaterRatio;
            float earthRatio = character.EarthRatio; float windRatio = character.WindRatio;
            spriteBatch.Draw(fireIcon, fireIconRect, Color.Black);
            spriteBatch.Draw(fireIcon,
                    new Rectangle(fireIconRect.X, fireIconRect.Y + (int)((1.0f - fireRatio) * fireIconRect.Height),
                    fireIconRect.Width, (int)(fireIconRect.Height * fireRatio)),
                    new Rectangle(0, (int)((1.0f - fireRatio) * fireIcon.Height), fireIcon.Width, (int)(fireRatio * fireIcon.Height)), 
                    (character.IsActive(Elements.Fire)) ? Color.White : Color.SlateGray);
            spriteBatch.Draw(waterIcon, waterIconRect, Color.Black);
            spriteBatch.Draw(waterIcon,
                    new Rectangle(waterIconRect.X, waterIconRect.Y + (int)((1.0f - waterRatio) * waterIconRect.Height),
                    waterIconRect.Width, (int)(waterIconRect.Height * waterRatio)),
                    new Rectangle(0, (int)((1.0f - waterRatio) * waterIcon.Height), waterIcon.Width, (int)(waterRatio * waterIcon.Height)),
                    (character.IsActive(Elements.Water)) ? Color.White : Color.SlateGray);
            spriteBatch.Draw(earthIcon, earthIconRect, Color.Black);
            spriteBatch.Draw(earthIcon,
                    new Rectangle(earthIconRect.X, earthIconRect.Y + (int)((1.0f - earthRatio) * earthIconRect.Height),
                    earthIconRect.Width, (int)(earthIconRect.Height * earthRatio)),
                    new Rectangle(0, (int)((1.0f - earthRatio) * earthIcon.Height), earthIcon.Width, (int)(earthRatio * earthIcon.Height)),
                    (character.IsActive(Elements.Earth)) ? Color.White : Color.SlateGray);
            spriteBatch.Draw(windIcon, windIconRect, Color.Black);
            spriteBatch.Draw(windIcon,
                    new Rectangle(windIconRect.X, windIconRect.Y + (int)((1.0f - windRatio) * windIconRect.Height),
                    windIconRect.Width, (int)(windIconRect.Height * windRatio)),
                    new Rectangle(0, (int)((1.0f - windRatio) * windIcon.Height), windIcon.Width, (int)(windRatio * windIcon.Height)),
                    (character.IsActive(Elements.Wind)) ? Color.White : Color.SlateGray);
        }

        private void DrawStatIcons(SpriteBatch spriteBatch, Character character)
        {
            spriteBatch.Draw(attackIcon, attackIconRect, Color.White);
            NumberDrawer.DrawNumber(spriteBatch, character.TotalAttack, attackStatRect, Color.Black);
            spriteBatch.Draw(staminaIcon, staminaIconRect, Color.White);
            NumberDrawer.DrawNumber(spriteBatch, character.TotalStamina, staminaStatRect, Color.Black);
            spriteBatch.Draw(defenseIcon, defenseIconRect, Color.White);
            NumberDrawer.DrawNumber(spriteBatch, character.TotalDefense, defenseStatRect, Color.Black);
            spriteBatch.Draw(speedIcon, speedIconRect, Color.White);
            NumberDrawer.DrawNumber(spriteBatch, character.TotalSpeed, speedStatRect, Color.Black);
        }
    }
}
