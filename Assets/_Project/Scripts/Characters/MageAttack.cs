using Duels.Battle;
using UnityEngine;

namespace Duels.Characters
{
    public sealed class MageAttack : CharacterAttack, ICharacterAttack
    {
        public void Attack(Character target)
        {
            DealDamage(target);

            target.Effects.ApplyDebuff(
                Definition.DebuffDuration,
                Definition.DebuffDamageMultiplier);
        }
    }
}