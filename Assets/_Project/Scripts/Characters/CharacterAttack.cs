using UnityEngine;

namespace Duels.Characters
{
    [RequireComponent(typeof(Character))]
    public abstract class CharacterAttack : MonoBehaviour
    {
        protected Character Character { get; private set; }

        protected CharacterDefinition Definition => Character.Definition;

        protected CharacterHealth Health => Character.Health;

        protected CharacterEffects Effects => Character.Effects;

        protected virtual void Awake()
        {
            Character = GetComponent<Character>();
        }

        protected int CalculateDamage()
        {
            return Mathf.RoundToInt(
                Definition.Damage * Effects.DamageMultiplier);
        }

        protected void DealDamage(Character target)
        {
            target.Health.TakeDamage(CalculateDamage());
        }
    }
}