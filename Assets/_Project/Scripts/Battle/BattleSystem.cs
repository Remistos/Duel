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
            if (battleEvents == null ||
                player1 == null ||
                player2 == null)
            {
                Debug.LogError(
                    "BattleSystem не инициализирован или персонажи не назначены.",
                    this);

                yield break;
            }

            if (!player1.TryGetComponent(
                    out ICharacterAttack player1Attack) ||
                !player2.TryGetComponent(
                    out ICharacterAttack player2Attack))
            {
                Debug.LogError(
                    "У одного из персонажей отсутствует компонент атаки.",
                    this);

                yield break;
            }

            BattleController controller = new BattleController(
                player1,
                player2,
                player1Attack,
                player2Attack,
                battleEvents);

            controller.StartBattle();

            while (!controller.IsFinished)
            {
                controller.PerformTurn();
                controller.PublishState();

                if (!controller.IsFinished)
                    yield return turnWait;
            }

            controller.FinishBattle();
        }
    }
}