using System.Collections;
using Duels.Characters;
using UnityEngine;

namespace Duels.Battle
{
    public sealed class BattleSystem : MonoBehaviour
    {
        [SerializeField] private float turnDelay = 1f;

        private BattleEvents battleEvents;
        private WaitForSeconds turnWait;

        public void Initialize(BattleEvents events)
        {
            battleEvents = events;
            turnWait = new WaitForSeconds(turnDelay);
        }

        public IEnumerator Fight(Character player1, Character player2)
        {
            if (!TryGetAttack(player1, out ICharacterAttack player1Attack) ||
                !TryGetAttack(player2, out ICharacterAttack player2Attack))
            {
                yield break;
            }

            PublishState(player1, player2);

            while (IsAlive(player1) && IsAlive(player2))
            {
                yield return Turn(player1, player2, player1Attack);

                PublishState(player1, player2);

                if (!IsAlive(player2))
                    break;

                yield return turnWait;

                yield return Turn(player2, player1, player2Attack);

                PublishState(player1, player2);

                if (!IsAlive(player1))
                    break;

                yield return turnWait;
            }

            string winner = IsAlive(player1)
                ? player1.Definition.ClassName
                : player2.Definition.ClassName;

            battleEvents.RaiseBattleFinished(winner);
        }

        private IEnumerator Turn(
            Character attacker,
            Character target,
            ICharacterAttack attack)
        {
            attacker.Effects.ProcessEffects(attacker.Health);

            if (attacker.Effects.IsStunned)
            {
                attacker.Effects.ProcessStun();
            }
            else
            {
                attack.Attack(target);
            }

            yield return null;
        }

        private void PublishState(
            Character player1,
            Character player2)
        {
            BattleState state = new BattleState(
                player1.Definition.ClassName,
                player1.Health.CurrentHealth,
                player1.Health.MaxHealth,
                player1.CurrentDamage,
                player1.Effects.IsStunned,
                player1.Effects.PoisonDuration,
                player1.Effects.DebuffDuration,

                player2.Definition.ClassName,
                player2.Health.CurrentHealth,
                player2.Health.MaxHealth,
                player2.CurrentDamage,
                player2.Effects.IsStunned,
                player2.Effects.PoisonDuration,
                player2.Effects.DebuffDuration
            );

            battleEvents.RaiseStateChanged(state);
        }

        private bool IsAlive(Character character)
        {
            return character.Health.IsAlive();
        }

        private bool TryGetAttack(
            Character character,
            out ICharacterAttack attack)
        {
            return character.TryGetComponent(out attack);
        }
    }
}