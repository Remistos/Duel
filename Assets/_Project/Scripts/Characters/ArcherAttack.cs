using Duels.Battle;
using UnityEngine;

namespace Duels.Characters
{
    public sealed class ArcherAttack : CharacterAttack, ICharacterAttack
    {
        public void Attack(Character target)
        {
            DealDamage(target);

            target.Effects.ApplyPoison(
                Definition.PoisonDamage,
                Definition.PoisonDuration);
        }
    }
}