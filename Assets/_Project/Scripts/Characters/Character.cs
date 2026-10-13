using Duels.Battle;
using UnityEngine;

namespace Duels.Characters
{
    public sealed class Character : MonoBehaviour
    {
        [field: SerializeField]
        public CharacterDefinition Definition { get; private set; }

        private CharacterHealth health;
        private CharacterEffects effects;

        public int CurrentDamage =>
    Definition == null || effects == null
        ? 0
        : Mathf.RoundToInt(
            Definition.Damage * effects.DamageMultiplier);

        public bool IsAlive => health != null && health.IsAlive;

        public bool IsStunned => effects != null && effects.IsStunned;

        private void Awake()
        {
            if (Definition == null)
            {
                Debug.LogError(
                    $"CharacterDefinition не назначен у персонажа {name}.",
                    this);

                enabled = false;
                return;
            }

            health = new CharacterHealth(Definition.MaxHealth);

            effects = new CharacterEffects(
                Definition.NormalDamageMultiplier);
        }

        public void TakeDamage(int damage)
        {
            health?.TakeDamage(damage);
        }

        public void ProcessEffects()
        {
            if (effects == null)
                return;

            int poisonDamage = effects.ProcessEffects();

            if (poisonDamage > 0)
                TakeDamage(poisonDamage);
        }

        public void ProcessStun()
        {
            effects?.ProcessStun();
        }

        public void ApplyStun(int duration)
        {
            effects?.ApplyStun(duration);
        }

        public void ApplyPoison(int damage, int duration)
        {
            effects?.ApplyPoison(damage, duration);
        }

        public void ApplyDebuff(int duration, float damageMultiplier)
        {
            effects?.ApplyDebuff(duration, damageMultiplier);
        }

        public PlayerState GetState()
        {
            return new PlayerState(
                Definition.ClassName,
                health.CurrentHealth,
                health.MaxHealth,
                CurrentDamage,
                effects.IsStunned,
                effects.PoisonDuration,
                effects.DebuffDuration);
        }
    }
}