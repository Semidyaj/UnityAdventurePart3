using UnityEngine;

namespace DZ_11
{
    public class Health
    {
        public Health (float maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }

        public void Heal(float additiveHealth)
        {
            if (additiveHealth < 0)
                return;

            CurrentHealth = Mathf.Min(CurrentHealth + additiveHealth, MaxHealth);
        }

        public void TakeDamage(float damage)
        {
            if (damage < 0)
                return;

            CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        }
    }
}