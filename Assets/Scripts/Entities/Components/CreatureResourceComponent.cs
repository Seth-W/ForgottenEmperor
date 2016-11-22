namespace CFE
{
    using System;
    using UnityEngine;

    class CreatureResourceComponent : MonoBehaviour, EResource
    {
        [SerializeField]
        float MaxHealth, MaxMana;
        float Health, Mana;

        EntityModel model;

        Bar healthBar, manaBar;

        void Start()
        {
            Health = MaxHealth;
            Mana = MaxMana;
            model = GetComponent<EntityModel>();
            if (model == null)
                Debug.Log("Could not find an EntityModel attached to this EResource");
        }


        public float GetHealth()
        {
            return Health;
        }

        public float GetMana()
        {
            return Mana;
        }

        public void IncrementHealth(float incrementVal, DamageType type)
        {
            if (incrementVal == 0)
                return;
            else if(incrementVal > 0)
            {
                Health += incrementVal;
                Health = Mathf.Clamp(Health, 0, MaxHealth);
                if (Health <= incrementVal)
                    model.setAlive(true);
            }
            else
            {
                if (!model.alive)
                    return;
                Health += incrementVal;
                if (Health <= 0)
                    model.setAlive(false);
            }
            healthBar.SetPercentageFull(Health / MaxHealth);
        }

        public void IncrementMana(float incrementVal)
        {
            Mana += incrementVal;
            Mana = Mathf.Clamp(Mana, 0, MaxMana);
            manaBar.SetPercentageFull(Mana / MaxMana);
        }

        public void Initiate()
        {
            throw new NotImplementedException();
        }

        public void RegisterHealthBar(Bar bar)
        {
            healthBar = bar;
        }

        public void RegisterManaBar(Bar bar)
        {
            manaBar = bar;
        }
    }
}
