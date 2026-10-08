using UnityEngine;

namespace Duels.Characters
{
    [RequireComponent(typeof(Character))]
    public abstract class CharacterAttack : MonoBehaviour
    {
        protected Character Character { get; private set; }

        protected CharacterDefinition Definition =>
            Character.Definition;

        protected virtual void Awake()
        {
            Character = GetComponent<Character>();
        }

        protected void DealDamage(Character target)
        {
            target.TakeDamage(Character.CurrentDamage);
        }
    }
}