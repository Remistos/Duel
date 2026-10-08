using Duels.Battle;
using UnityEngine;

namespace Duels.Characters
{
    [RequireComponent(typeof(CharacterHealth))]
    [RequireComponent(typeof(CharacterEffects))]
    public sealed class Character : MonoBehaviour
    {
        [SerializeField] private CharacterDefinition definition;

        private CharacterHealth health;
        private CharacterEffects effects;

        public CharacterDefinition Definition { get; private set; }

        public int CurrentDamage =>
            Mathf.RoundToInt(
                definition.Damage * effects.DamageMultiplier);

        public bool IsAlive =>
            health.IsAlive;

        public bool IsStunned =>
            effects.IsStunned;

        private void Awake()
        {
            health = GetComponent<CharacterHealth>();
            effects = GetComponent<CharacterEffects>();

            if (definition == null)
            {
                Debug.LogError(
                    $"CharacterDefinition не назначен в {name}.",
                    this);

                return;
            }

            Definition = definition;

            health.Initialize(definition.MaxHealth);
            effects.Initialize(
                definition.NormalDamageMultiplier);
        }

        public void TakeDamage(int damage)
        {
            health.TakeDamage(damage);
        }

        public void ProcessEffects()
        {
            int poisonDamage = effects.ProcessEffects();

            if (poisonDamage > 0)
            {
                TakeDamage(poisonDamage);
            }
        }

        public void ProcessStun()
        {
            effects.ProcessStun();
        }

        public void ApplyStun(int duration)
        {
            effects.ApplyStun(duration);
        }

        public void ApplyPoison(
            int damage,
            int duration)
        {
            effects.ApplyPoison(
                damage,
                duration);
        }

        public void ApplyDebuff(
            int duration,
            float damageMultiplier)
        {
            effects.ApplyDebuff(
                duration,
                damageMultiplier);
        }

        public PlayerState GetState()
        {
            return new PlayerState(
                definition.ClassName,
                health.CurrentHealth,
                health.MaxHealth,
                CurrentDamage,
                effects.IsStunned,
                effects.PoisonDuration,
                effects.DebuffDuration);
        }
    }
}