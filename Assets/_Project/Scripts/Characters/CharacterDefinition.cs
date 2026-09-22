using UnityEngine;

namespace Duels.Characters
{
    [CreateAssetMenu(
        fileName = "CharacterDefinition",
        menuName = "Duels/Characters/Character Definition")]
    public sealed class CharacterDefinition : ScriptableObject
    {
        [field: SerializeField]
        public string ClassName { get; private set; }

        [field: SerializeField]
        public int MaxHealth { get; private set; }

        [field: SerializeField]
        public int Damage { get; private set; }

        [field: Header("Damage")]
        [field: SerializeField]
        public float NormalDamageMultiplier { get; private set; }

        [field: Header("Poison")]
        [field: SerializeField]
        public int PoisonDamage { get; private set; }

        [field: SerializeField]
        public int PoisonDuration { get; private set; }

        [field: Header("Stun")]
        [field: SerializeField]
        [field: Range(0f, 1f)]
        public float StunChance { get; private set; }

        [field: SerializeField]
        public int StunDuration { get; private set; }

        [field: Header("Debuff")]
        [field: SerializeField]
        public int DebuffDuration { get; private set; }

        [field: SerializeField]
        [field: Range(0f, 1f)]
        public float DebuffDamageMultiplier { get; private set; }
    }
}