using UnityEngine;

namespace Duels.Characters
{
    public sealed class CharacterHealth : MonoBehaviour
    {
        public int CurrentHealth { get; private set; }

        public int MaxHealth { get; private set; }

        public bool IsAlive => CurrentHealth > 0;

        public void Initialize(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (damage <= 0)
            {
                return;
            }

            CurrentHealth = Mathf.Max(
                CurrentHealth - damage,
                0);
        }
    }
}