using UnityEngine;

namespace Duels.Characters
{
    public sealed class CharacterHealth : MonoBehaviour
    {
        public int CurrentHealth { get; private set; }

        public int MaxHealth { get; private set; }

        public void Initialize(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        }

        public bool IsAlive()
        {
            return CurrentHealth > 0;
        }
    }
}