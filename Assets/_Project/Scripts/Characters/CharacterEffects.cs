using UnityEngine;

namespace Duels.Characters
{
    public sealed class CharacterEffects : MonoBehaviour
    {
        public bool IsStunned { get; private set; }

        public int PoisonDuration { get; private set; }

        public int DebuffDuration { get; private set; }

        public float DamageMultiplier { get; private set; }

        private int poisonDamage;
        private int stunTurnsRemaining;
        private float normalDamageMultiplier = 1f;

        public void Initialize(float defaultDamageMultiplier)
        {
            normalDamageMultiplier = defaultDamageMultiplier;
            DamageMultiplier = defaultDamageMultiplier;
        }

        public void ApplyStun(int duration)
        {
            if (duration <= 0)
            {
                return;
            }

            IsStunned = true;
            stunTurnsRemaining = duration;
        }

        public void ApplyPoison(int damage, int duration)
        {
            if (damage <= 0 || duration <= 0)
            {
                return;
            }

            poisonDamage = damage;
            PoisonDuration = duration;
        }

        public void ApplyDebuff(
            int duration,
            float damageMultiplier)
        {
            if (duration <= 0)
            {
                return;
            }

            DebuffDuration = duration;
            DamageMultiplier = damageMultiplier;
        }

        public int ProcessEffects()
        {
            int damage = ProcessPoison();
            ProcessDebuff();

            return damage;
        }

        public void ProcessStun()
        {
            if (!IsStunned)
            {
                return;
            }

            stunTurnsRemaining--;

            if (stunTurnsRemaining <= 0)
            {
                stunTurnsRemaining = 0;
                IsStunned = false;
            }
        }

        private int ProcessPoison()
        {
            if (PoisonDuration <= 0)
            {
                return 0;
            }

            PoisonDuration--;

            int damage = poisonDamage;

            if (PoisonDuration == 0)
            {
                poisonDamage = 0;
            }

            return damage;
        }

        private void ProcessDebuff()
        {
            if (DebuffDuration <= 0)
            {
                DamageMultiplier = normalDamageMultiplier;
                return;
            }

            DebuffDuration--;

            if (DebuffDuration == 0)
            {
                DamageMultiplier = normalDamageMultiplier;
            }
        }
    }
}