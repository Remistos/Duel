using Duels.Battle;

namespace Duels.Characters
{
    public sealed class MageAttack : CharacterAttack, ICharacterAttack
    {
        public void Attack(Character target)
        {
            DealDamage(target);

            target.ApplyDebuff(
                Definition.DebuffDuration,
                Definition.DebuffDamageMultiplier);
        }
    }
}