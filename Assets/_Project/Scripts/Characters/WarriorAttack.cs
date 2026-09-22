using Duels.Battle;
using UnityEngine;

namespace Duels.Characters
{
    public sealed class WarriorAttack : CharacterAttack, ICharacterAttack
    {
        public void Attack(Character target)
        {
            DealDamage(target);

            if (Random.value <= Definition.StunChance)
            {
                target.Effects.ApplyStun(
                    Definition.StunDuration);
            }
        }
    }
}