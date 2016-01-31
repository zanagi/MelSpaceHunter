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
using MelSpaceHunter.Effects;

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

        // Damage
        private int damageTimeElapsed, damageStacked, damageDigitWidth, damageDigitHeight;
        private const int damageInterval = 300;

        private const float velocityDivider = 1.5f;

        private int score;

        public Character(Vector2 pos, int width, int height, int startingStatValue = 5, int maxStatValue = 25, int startingMaxHealth = 100, int maxHealthLimit = 500,
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

            this.damageTimeElapsed = damageInterval;
            this.damageStacked = 0;
            this.damageDigitWidth = width / 4;
            this.damageDigitHeight = this.damageDigitWidth * 3 / 2;
            this.score = 0;
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

            if (!form.Equals(normalForm) && form.ElementalRatio <= 0)
                ChangeForm(normalForm);

            if (!CanBeDamaged)
                damageTimeElapsed += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            form.Update(gameTime, elementals, this);
        }

        private void UpdateStats()
        {
            IncreaseStat(form.CurrentElement, form.GetStatIncrease() * 2); // Faster stat increase
        }

        private void HandleInput(InputManager inputManager)
        {
            // Movement
            if (inputManager.KeyPressed(Keys.Up) || inputManager.KeyPressed(Keys.W))
            {
                pos.Y -= Velocity;
            }
            if (inputManager.KeyPressed(Keys.Down) || inputManager.KeyPressed(Keys.S))
            {
                pos.Y += Velocity;
            }
            if (inputManager.KeyPressed(Keys.Left) || inputManager.KeyPressed(Keys.A))
            {
                pos.X -= Velocity;
            }
            if (inputManager.KeyPressed(Keys.Right) || inputManager.KeyPressed(Keys.D))
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
            if (inputManager.KeyTapped(Keys.D1) && fireForm.CanTransform())
            {
                ChangeForm(fireForm);
            }
            else if (inputManager.KeyTapped(Keys.D2) && waterForm.CanTransform())
            {
                ChangeForm(waterForm);
            }
            else if (inputManager.KeyTapped(Keys.D3) && earthForm.CanTransform())
            {
                ChangeForm(earthForm);
            }
            else if (inputManager.KeyTapped(Keys.D4) && windForm.CanTransform())
            {
                ChangeForm(windForm);
            }
        }

        private void HandleFormInput(InputManager inputManager)
        {
            // TODO: Transformation Special Moves
            if (inputManager.KeyTapped(Keys.Q) && CanUseQ())
            {
                form.UseQ();
            }
            else if (inputManager.KeyTapped(Keys.E) && CanUseE())
            {
                form.UseE();
            }
            else if (inputManager.KeyTapped(Keys.D1) && form.Equals(fireForm))
            {
                ChangeForm(normalForm);
            }
            else if (inputManager.KeyTapped(Keys.D2) && form.Equals(waterForm))
            {
                ChangeForm(normalForm);
            }
            else if (inputManager.KeyTapped(Keys.D3) && form.Equals(earthForm))
            {
                ChangeForm(normalForm);
            }
            else if (inputManager.KeyTapped(Keys.D4) && form.Equals(windForm))
            {
                ChangeForm(normalForm);
            }
        }

        private void ChangeForm(Form newForm)
        {
            if (newForm.Equals(normalForm))
            {
                form.DropElementalPoints();
            }
            else
            {
                // Restore health on elemental form change
                RecoverHealth(maxHealth / 4, currentHealth / 2);
            }
            form = newForm;
        }

        public void TakeDamage()
        {
            if (CanBeDamaged)
            {
                currentHealth = Math.Max(0, currentHealth - damageStacked);

                EffectManager.AddEffect(new NumberEffect(damageStacked, 700, damageDigitWidth, damageDigitHeight, 
                    new Vector2(pos.X - damageDigitWidth / 2, pos.Y - damageDigitHeight), -Vector2.UnitY, Color.Red));

                damageStacked = damageTimeElapsed = 0;
            }
        }

        public void StackDamage(int amount)
        {
            if (amount <= 0)
                return;

            if (CanBeDamaged)
                damageStacked += amount;
        }

        public void AddExperience(int amount)
        {
            if (amount <= 0)
                return;

            form.AddExperience(amount);

            // Also add score
            score += amount;
        }

        public void AddElementalPoints(Elements element, int amount)
        {
            if (form.Equals(normalForm))
            {
                //Console.WriteLine("elem: " + amount);

                switch (element)
                {
                    case Elements.Fire:
                        fireForm.AddElementalPoints(amount);
                        break;
                    case Elements.Water:
                        waterForm.AddElementalPoints(amount);
                        break;
                    case Elements.Earth:
                        earthForm.AddElementalPoints(amount);
                        break;
                    case Elements.Wind:
                        windForm.AddElementalPoints(amount);
                        break;
                }
            }
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

            if (element != Elements.None && amount > 0)
                RecoverHealth(maxHealth / 10, currentHealth / 5);

            switch (element)
            {
                case Elements.Fire:
                    attack = Math.Min(attack + amount, maxStatValue);
                    break;
                case Elements.Earth:
                    defense = Math.Min(defense + amount, maxStatValue);
                    break;
                case Elements.Water:
                    stamina = Math.Min(stamina + amount, maxStatValue);
                    break;
                case Elements.Wind:
                    speed = Math.Min(speed + amount, maxStatValue);
                    break;
                case Elements.None:
                    int healthChange = Math.Min(amount * 5, maxHealthLimit - maxHealth);
                    currentHealth += healthChange;
                    maxHealth += healthChange;
                    break;
            }
        }

        private void RecoverHealth(int a, int b)
        {
            currentHealth = Math.Min(maxHealth, currentHealth + Math.Max(a, b));
        }

        public bool IsActive(Elements element)
        {
            switch (element)
            {
                case Elements.Fire:
                    return (form.Equals(fireForm));
                case Elements.Earth:
                    return (form.Equals(earthForm));
                case Elements.Water:
                    return (form.Equals(waterForm));
                case Elements.Wind:
                    return (form.Equals(windForm));
            }
            return false;
        }

        public bool IsAvailable(Elements element)
        {
            switch (element)
            {
                case Elements.Fire:
                    return ((form.Equals(normalForm) && fireForm.CanTransform()));
                case Elements.Earth:
                    return ((form.Equals(normalForm) && earthForm.CanTransform()));
                case Elements.Water:
                    return ((form.Equals(normalForm) && waterForm.CanTransform()));
                case Elements.Wind:
                    return ((form.Equals(normalForm) && windForm.CanTransform()));
            }
            return false;
        }

        public bool CanTransform()
        {
            return fireForm.CanTransform() || waterForm.CanTransform() || earthForm.CanTransform() || windForm.CanTransform();
        }

        public bool CanUseQ()
        {
            return !form.UsingAbility && StatAverage >= 10 && form.ElementalRatio >= 0.25f;
        }

        public bool CanUseE()
        {
            return !form.UsingAbility && StatAverage >= 15 && form.ElementalRatio >= 0.5f;
        }

        #region properties
        public Elements CurrentElement
        {
            get { return form.CurrentElement; }
        }

        public bool CanBeDamaged
        {
            get { return damageTimeElapsed >= damageInterval; }
        }

        public int TotalAttack
        {
            get { return Math.Min(maxStatValue, (int)(attack * form.AttackModifier)); }
        }

        public int TotalDefense
        {
            get { return Math.Min(maxStatValue, (int)(defense * form.DefenseModifier)); }
        }

        public int TotalStamina
        {
            get { return Math.Min(maxStatValue, (int)(stamina * form.StaminaModifier)); }
        }

        public int TotalSpeed
        {
            get { return Math.Min(maxStatValue, (int)(speed * form.SpeedModifier)); }
        }

        public int CurrentHealth
        {
            get { return currentHealth; }
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

        public int Score
        {
            get { return score; }
        }
        #endregion
    }
}
