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

        public CharacterDefinition Definition => definition;

        public CharacterHealth Health => health;

        public CharacterEffects Effects => effects;

        public int CurrentDamage =>
            Mathf.RoundToInt(
                definition.Damage * effects.DamageMultiplier);

        private void Awake()
        {
            health = GetComponent<CharacterHealth>();
            effects = GetComponent<CharacterEffects>();

            if (definition == null)
            {
                Debug.LogError(
                    $"CharacterDefinition не назначен у {name}.",
                    this);

                return;
            }

            health.Initialize(definition.MaxHealth);
            effects.Initialize(definition.NormalDamageMultiplier);
        }
    }
}