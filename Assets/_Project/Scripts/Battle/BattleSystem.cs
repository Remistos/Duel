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

        public IEnumerator Fight(
            Character player1,
            Character player2)
        {
            if (!TryGetAttack(
                    player1,
                    out ICharacterAttack player1Attack) ||
                !TryGetAttack(
                    player2,
                    out ICharacterAttack player2Attack))
            {
                yield break;
            }

            PublishState(player1, player2);

            while (player1.IsAlive && player2.IsAlive)
            {
                yield return Turn(
                    player1,
                    player2,
                    player1Attack);

                PublishState(player1, player2);

                if (!player2.IsAlive)
                {
                    break;
                }

                yield return turnWait;

                yield return Turn(
                    player2,
                    player1,
                    player2Attack);

                PublishState(player1, player2);

                if (!player1.IsAlive)
                {
                    break;
                }

                yield return turnWait;
            }

            string winner = player1.IsAlive
                ? player1.Definition.ClassName
                : player2.Definition.ClassName;

            battleEvents.RaiseBattleFinished(winner);
        }

        private IEnumerator Turn(
            Character attacker,
            Character target,
            ICharacterAttack attack)
        {
            attacker.ProcessEffects();

            if (attacker.IsStunned)
            {
                attacker.ProcessStun();
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
                player1.GetState(),
                player2.GetState());

            battleEvents.RaiseStateChanged(state);
        }

        private bool TryGetAttack(
            Character character,
            out ICharacterAttack attack)
        {
            return character.TryGetComponent(out attack);
        }
    }
}