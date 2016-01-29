using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        private double formPoints;

        // Character Parameters
        private int attack, defense, energyConsumption, speed, maxStatValue;
        private int currentHealth, maxHealth, maxHealthLimit;


        public Character(Vector2 pos, int startingStatValue = 5, int maxStatValue = 30, int startingMaxHealth = 100, int maxHealthLimit = 500,
            int startingElementPoints = 0, int maxElementPoints = 100)
        {
            this.pos = pos;
            this.attack = this.defense = this.energyConsumption = this.speed = startingStatValue;
            this.maxStatValue = maxStatValue;
            this.currentHealth = maxHealth = startingMaxHealth;
            this.maxHealthLimit = maxHealthLimit;
            this.normalForm = new NormalForm("Forms/normal");
            this.fireForm = new FireForm("Forms/fire");
            this.earthForm = new EarthForm("Forms/earth");
            this.waterForm = new WaterForm("Forms/water");
            this.windForm = new WindForm("Forms/wind");
            this.formPoints = 0;
            this.form = normalForm;
        }

        public void Update(GameTime gameTime, InputManager inputManager, List<Elemental> elementals)
        {
            UpdateStats();

            form.Update(gameTime, elementals);
        }

        private void UpdateStats()
        {
            if (attack < maxStatValue)
                attack += fireForm.GetStatIncrease();
        }

        public bool CanTransform(Elements element)
        {
            switch (element) {
                case Elements.Fire:
                    return fireForm.CanTransform();
                case Elements.Earth:
                    return earthForm.CanTransform();
                case Elements.Water:
                    return waterForm.CanTransform();
                case Elements.Wind:
                    return windForm.CanTransform();
            }
            return false;
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
    }
}
