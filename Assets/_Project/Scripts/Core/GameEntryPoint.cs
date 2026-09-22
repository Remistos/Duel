using Duels.Battle;
using Duels.Characters;
using Duels.UI;
using UnityEngine;

namespace Duels
{
    public sealed class GameEntryPoint : MonoBehaviour
    {
        [SerializeField] private CharacterFactory factory;
        [SerializeField] private BattleSystem battleSystem;
        [SerializeField] private BattlePresenter battlePresenter;

        private BattleEvents battleEvents;

        private void Start()
        {
            battleEvents = new BattleEvents();

            battleSystem.Initialize(battleEvents);
            battlePresenter.Initialize(battleEvents);

            Character player1 = factory.CreatePlayer(true);
            Character player2 = factory.CreatePlayer(false);

            StartCoroutine(
                battleSystem.Fight(player1, player2));
        }
    }
}