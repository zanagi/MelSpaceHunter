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
        protected int experience, maxExperience, elementalPoints, maxElementalPoints; 

        public Form(string path, Elements element, int maxElementalPoints = 100)
        {
            this.animation = new Animation(path, 4, 1);
            this.element = element;
            this.experience = 0;
            this.maxExperience = 100;
            this.elementalPoints = 0;
            this.maxElementalPoints = maxElementalPoints;
        }

        public virtual void LoadContent(ContentManager content)
        {
            animation.LoadContent(content);
        }

        public virtual void Update(GameTime gameTime, List<Elemental> elementals)
        {
            animation.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 origin)
        {
            animation.Draw(spriteBatch, origin);
        }

        public int GetStatIncrease()
        {
            int increase = experience / maxExperience;

            experience -= increase * maxExperience;

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

        public virtual float EnergyConsumptionModifier
        {
            get { return 1.0f; }
        }

        public virtual float SpeedModifier
        {
            get { return 1.0f; }
        }

    }
}
