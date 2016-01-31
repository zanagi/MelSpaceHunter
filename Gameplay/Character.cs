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

        // Character Stats
        private int attack, defense, stamina, speed, maxStatValue;
        private int currentHealth, maxHealth, maxHealthLimit;

        private bool animating;
        private Animation specialAnimation;

        private const float velocityDivider = 1.5f;

        public Character(Vector2 pos, int width, int height, int startingStatValue = 5, int maxStatValue = 30, int startingMaxHealth = 100, int maxHealthLimit = 500,
            int startingElementPoints = 0, int maxElementPoints = 100)
        {
            this.pos = pos;
            this.attack = this.defense = this.stamina = this.speed = startingStatValue;
            this.maxStatValue = maxStatValue;
            this.currentHealth = maxHealth = startingMaxHealth;
            this.maxHealthLimit = maxHealthLimit;

            this.normalForm = new NormalForm("Forms/normalForm", width, height);
            this.fireForm = new FireForm("Forms/fireForm", width, height);
            this.earthForm = new EarthForm("Forms/earthForm", width, height);
            this.waterForm = new WaterForm("Forms/waterForm", width, height);
            this.windForm = new WindForm("Forms/windForm", width, height);
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
            form.Update(gameTime, elementals, TotalAttack, TotalDefence, TotalStamina);
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
                HandleTransformationInput(inputManager);
            }
            else
            {
                HandleFormInput(inputManager);
            }
        }

        private void HandleTransformationInput(InputManager inputManager)
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

        private void HandleFormInput(InputManager inputManager)
        {
            // TODO: Transformation Special Moves
            if (inputManager.KeyTapped(Keys.Q))
            {

            }
            else if (inputManager.KeyTapped(Keys.E))
            {

            }
            else if (inputManager.KeyTapped(Keys.W) && form.Equals(fireForm))
            {
                ChangeForm(normalForm);
            }
            else if (inputManager.KeyTapped(Keys.D) && form.Equals(waterForm))
            {
                ChangeForm(normalForm);
            }
            else if (inputManager.KeyTapped(Keys.S) && form.Equals(earthForm))
            {
                ChangeForm(normalForm);
            }
            else if (inputManager.KeyTapped(Keys.A) && form.Equals(windForm))
            {
                ChangeForm(normalForm);
            }
        }

        private void ChangeForm(Form newForm)
        {
            form = newForm;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            form.Draw(spriteBatch, pos);
        }

        public void DrawStill(SpriteBatch spriteBatch, int originX, int originY, int width, int height)
        {
            form.DrawStill(spriteBatch, originX, originY, width, height);
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
                    stamina = Math.Max(stamina + amount, maxStatValue);
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

        public int TotalStamina
        {
            get { return (int)(stamina * form.StaminaModifier); }
        }

        public int TotalSpeed
        {
            get { return (int)(speed * form.SpeedModifier); }
        }

        public int StatAverage
        {
            get { return (attack + defense + stamina + speed) / 4;}
        }

        public float FireRatio
        {
            get { return fireForm.ElementalRatio; }
        }

        public float WaterRatio
        {
            get { return waterForm.ElementalRatio; }
        }

        public float EarthRatio
        {
            get { return earthForm.ElementalRatio; }
        }

        public float WindRatio
        {
            get { return windForm.ElementalRatio; }
        }

        public bool Transformed
        {
            get { return !form.Equals(normalForm); }
        }

        public float HealthRatio
        {
            get { return 1.0f * currentHealth / maxHealth; }
        }

        public float ExperienceRatio
        {
            get { return form.ExperienceRatio; }
        }

        public Vector2 Position
        {
            get { return pos; }
        }

        public float Velocity
        {
            get { return 20.0f / TotalSpeed + TotalSpeed / 3.0f; }
        }

        public bool Animating
        {
            get { return animating; }
        }
        #endregion
    }
}
