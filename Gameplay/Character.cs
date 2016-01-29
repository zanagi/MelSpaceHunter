using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MelSpaceHunter.Gameplay.Forms;
using MelSpaceHunter.Gameplay.Elementals;

namespace MelSpaceHunter.Gameplay
{
    class Character
    {
        private Vector2 pos;

        // Elements/Form-related
        private Form form;
        private NormalForm normalForm;
        private FireForm fireForm;
        private EarthForm earthForm;
        private WaterForm waterForm;
        private WindForm windForm;
        private double formPoints, maxFormPoints;

        // Character Stats
        private int attack, defense, energyConsumption, speed, maxStatValue;
        private int currentHealth, maxHealth, maxHealthLimit;

        private const float velocityDivider = 1.5f;

        public Character(Vector2 pos, int width, int height, int startingStatValue = 5, int maxStatValue = 30, int startingMaxHealth = 100, int maxHealthLimit = 500,
            int startingElementPoints = 0, int maxElementPoints = 100)
        {
            this.pos = pos;
            this.attack = this.defense = this.energyConsumption = this.speed = startingStatValue;
            this.maxStatValue = maxStatValue;
            this.currentHealth = maxHealth = startingMaxHealth;
            this.maxHealthLimit = maxHealthLimit;

            this.normalForm = new NormalForm("Forms/normalForm", width, height);
            this.fireForm = new FireForm("Forms/fireForm", width, height);
            this.earthForm = new EarthForm("Forms/earthForm", width, height);
            this.waterForm = new WaterForm("Forms/waterForm", width, height);
            this.windForm = new WindForm("Forms/windForm", width, height);
            this.formPoints = 0.0;
            this.maxFormPoints = 100.0;
            this.form = normalForm;
        }

        public void LoadContent(ContentManager content)
        {
            normalForm.LoadContent(content);
            fireForm.LoadContent(content);
            earthForm.LoadContent(content);
            waterForm.LoadContent(content);
            windForm.LoadContent(content);
        }

        public void Update(GameTime gameTime, InputManager inputManager, List<Elemental> elementals)
        {
            UpdateStats();

            HandleInput(inputManager);
            form.Update(gameTime, elementals);
        }

        private void UpdateStats()
        {
            IncreaseStat(form.Element, form.GetStatIncrease());
        }

        private void HandleInput(InputManager inputManager)
        {
            // Movement
            if (inputManager.KeyPressed(Keys.Up))
            {
                pos.Y -= Velocity;
            }
            if (inputManager.KeyPressed(Keys.Down))
            {
                pos.Y += Velocity;
            }
            if (inputManager.KeyPressed(Keys.Left))
            {
                pos.X -= Velocity;
            }
            if (inputManager.KeyPressed(Keys.Right))
            {
                pos.X += Velocity;
            }

            // Transformation
            if (!Transformed)
            {
                if (inputManager.KeyTapped(Keys.W) && fireForm.CanTransform())
                {
                    ChangeForm(fireForm);
                }
                else if (inputManager.KeyTapped(Keys.D) && waterForm.CanTransform())
                {
                    ChangeForm(waterForm);
                }
                else if (inputManager.KeyTapped(Keys.S) && earthForm.CanTransform())
                {
                    ChangeForm(earthForm);
                }
                else if (inputManager.KeyTapped(Keys.A) && windForm.CanTransform())
                {
                    ChangeForm(windForm);
                }
            }
            else
            {
                // TODO: Transformation Special Moves
                if (inputManager.KeyTapped(Keys.Q))
                {

                }
                else if (inputManager.KeyTapped(Keys.E))
                {

                }
            }
        }

        private void ChangeForm(Form newForm)
        {
            form = newForm;
            formPoints = maxFormPoints;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            form.Draw(spriteBatch, pos);
        }

        /// <summary>
        /// Draws the character icon, stats and elemental form icons on the bottom
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void DrawIcon(SpriteBatch spriteBatch)
        {

        }

        public void IncreaseStat(Elements element, int amount)
        {
            if(amount < 0)
                throw new Exception("Negative stat increase.");

            switch (element)
            {
                case Elements.Fire:
                    attack = Math.Max(attack + amount, maxStatValue);
                    break;
                case Elements.Earth:
                    defense = Math.Max(defense + amount, maxStatValue);
                    break;
                case Elements.Water:
                    energyConsumption = Math.Max(energyConsumption + amount, maxStatValue);
                    break;
                case Elements.Wind:
                    speed = Math.Max(speed + amount, maxStatValue);
                    break;
                case Elements.None:
                    int healthChange = Math.Min(amount, maxHealthLimit - maxHealth);
                    currentHealth += healthChange;
                    maxHealth += healthChange;
                    break;
            }
        }

        #region properties
        public int TotalAttack
        {
            get { return (int)(attack * form.AttackModifier); }
        }

        public int TotalDefence
        {
            get { return (int)(defense * form.DefenseModifier); }
        }

        public int TotalEnergyConsumption
        {
            get { return (int)(energyConsumption * form.EnergyConsumptionModifier); }
        }

        public int TotalSpeed
        {
            get { return (int)(speed * form.SpeedModifier); }
        }

        public int StatAverage
        {
            get { return (attack + defense + energyConsumption + speed) / 4;}
        }

        public bool Transformed
        {
            get { return !form.Equals(normalForm); }
        }

        public Vector2 Position
        {
            get { return pos; }
        }

        public float Velocity
        {
            get { return 20.0f / TotalSpeed + TotalSpeed / 3.0f; }
        }
        #endregion
    }
}
