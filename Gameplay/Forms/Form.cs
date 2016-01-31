using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MelSpaceHunter.Gameplay.Elementals;

namespace MelSpaceHunter.Gameplay.Forms
{
    abstract class Form
    {
        protected Animation animation;
        protected Elements element;
        protected int experience, maxExperience;
        protected float elementalPoints, maxElementalPoints;

        public Form(string path, Elements element, int width, int height, int maxElementalPoints = 100)
        {
            this.animation = new Animation(path, 4, 1, width, height);
            this.element = element;
            this.experience = 50;
            this.maxExperience = 100;
            this.elementalPoints = 0;
            this.maxElementalPoints = maxElementalPoints;
        }

        public virtual void LoadContent(ContentManager content)
        {
            animation.LoadContent(content);
        }

        public virtual void Update(GameTime gameTime, List<Elemental> elementals, int attack, int defense, int stamina)
        {
            elementalPoints = Math.Max(0, elementalPoints - Math.Max(0.01f, 10.0f / stamina - stamina / 30.0f));

            animation.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 origin)
        {
            animation.Draw(spriteBatch, origin);
        }

        public virtual void DrawStill(SpriteBatch spriteBatch, int originX, int originY, int width, int height, int frame = 0)
        {
            animation.DrawFrame(spriteBatch, originX, originY, width, height, frame);
        }

        public int GetStatIncrease()
        {
            int increase = experience / maxExperience;

            experience -= increase * maxExperience;
            maxExperience = maxExperience * 6 / 5;

            return increase;
        }

        protected void AddElementalPoints(int amount)
        {
            if (amount < 0)
                throw new Exception("Negative increase in elemental points");

            elementalPoints = Math.Max(elementalPoints + amount, maxElementalPoints);
        }

        public bool CanTransform()
        {
            return elementalPoints == maxElementalPoints;
        }

        #region properties
        public Elements Element
        {
            get { return element; }
        }

        public virtual float AttackModifier
        {
            get { return 1.0f; }
        }

        public virtual float DefenseModifier
        {
            get { return 1.0f; }
        }

        public virtual float StaminaModifier
        {
            get { return 1.0f; }
        }

        public virtual float SpeedModifier
        {
            get { return 1.0f; }
        }

        public float ExperienceRatio
        {
            get { return Math.Min(1.0f, (1.0f * experience) / maxExperience); }
        }

        
        public float ElementalRatio
        {
            get { return Math.Min(1.0f, elementalPoints / maxElementalPoints); }
        }
        #endregion
    }
}
