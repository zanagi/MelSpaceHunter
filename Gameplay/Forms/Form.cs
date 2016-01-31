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
        protected Animation animation, qAnimation, eAnimation;
        protected Elements element;
        protected int experience, maxExperience, targetExperience;
        protected float elementalPoints, maxElementalPoints;

        protected bool usingQ, usingE;
        protected int qTimer, eTimer;
        protected const int abilityTime = 5000;

        public Form(string path, Elements element, int width, int height, int maxElementalPoints = 100)
        {
            this.animation = new Animation(path, 4, 1, width, height);
            this.element = element;
            this.experience = this.targetExperience = 0;
            this.maxExperience = 100;
            this.elementalPoints = 0;
            this.maxElementalPoints = maxElementalPoints;
            this.usingQ = this.usingE = false;
            this.qTimer = this.eTimer = 0;
        }

        public virtual void LoadContent(ContentManager content)
        {
            animation.LoadContent(content);
        }

        public virtual void Update(GameTime gameTime, List<Elemental> elementals, Character character)
        {
            if (usingQ && qAnimation != null)
            {
                qAnimation.Update(gameTime);
            }
            else if (usingE && eAnimation != null)
            {
                eAnimation.Update(gameTime);
            }

            if (experience < targetExperience)
            {
                experience += Math.Min(3, targetExperience - experience);
            }

            elementalPoints = Math.Max(0, elementalPoints - Math.Max(0.05f, 1.0f / character.TotalStamina));

            animation.Update(gameTime);

            HandleCombat(elementals, character);
        }

        private void HandleCombat(List<Elemental> elementals, Character character)
        {
            for (int i = 0; i < elementals.Count; i++)
            {
                if (Vector2.DistanceSquared(elementals[i].Position, character.Position) 
                    <= Math.Pow((elementals[i].Width + animation.TargetWidth) / 2, 2))
                {
                    elementals[i].TakeDamage(DamageToElemental(character, elementals[i]));
                    character.StackDamage((int)(Math.Max(1, (elementals[i].Attack + 4) * (elementals[i].Attack + 4) / (5 * character.TotalDefense))
                        * Element.GetMultiplier(elementals[i].CurrentElement, character.CurrentElement)));
                }
            }
            character.TakeDamage();
        }

        protected int DamageToElemental(Character character, Elemental elemental)
        {
            return (int)(Math.Max(1, (character.TotalAttack + 5) * (character.TotalAttack + 5) / (5 * elemental.Defense))
            * Element.GetMultiplier(character.CurrentElement, elemental.CurrentElement));
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 origin)
        {
            if (usingQ && qAnimation != null)
            {
                qAnimation.Draw(spriteBatch, origin);
            }
            else if (usingE && eAnimation != null)
            {
                eAnimation.Draw(spriteBatch, origin);
            }
            animation.Draw(spriteBatch, origin);
        }

        public virtual void DrawStill(SpriteBatch spriteBatch, int originX, int originY, int width, int height, int frame = 0)
        {
            animation.DrawFrame(spriteBatch, originX, originY, width, height, frame);
        }

        public int GetStatIncrease()
        {
            int increase = experience / maxExperience;

            if (increase > 0)
            {
                targetExperience = experience % maxExperience;
                experience = 0;

                // Increase max experience cap
                maxExperience = maxExperience * 115 / 100;
            }
            return increase;
        }

        public void DropElementalPoints()
        {
            elementalPoints = 0;
        }

        public void AddElementalPoints(int amount)
        {
            if (amount < 0)
                throw new Exception("Negative increase in elemental points");

            //Console.WriteLine("Elemental points yeahs");

            elementalPoints = Math.Min(elementalPoints + amount, maxElementalPoints);
        }

        public void AddExperience(int targetExperienceAmount)
        {
            targetExperience += targetExperienceAmount;
        }

        public bool CanTransform()
        {
            return elementalPoints >= maxElementalPoints;
        }

        public void UseQ()
        {
            usingQ = true;
            elementalPoints = Math.Max(0, elementalPoints - 0.25f * maxElementalPoints);
        }

        public void UseE() 
        {
            usingE = true;
            elementalPoints = Math.Max(0, elementalPoints - 0.5f * maxElementalPoints);
        }

        #region properties
        public bool UsingAbility
        {
            get { return usingQ || usingE; }
        }

        public Elements CurrentElement
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
