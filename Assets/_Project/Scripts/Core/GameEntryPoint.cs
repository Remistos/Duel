using Duels.Battle;
using Duels.Characters;
using Duels.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Duels
{
    public sealed class GameEntryPoint : MonoBehaviour
    {
        [SerializeField] private CharacterFactory factory;
        [SerializeField] private BattleSystem battleSystem;
        [SerializeField] private BattlePresenter battlePresenter;
        [SerializeField] private BattleRestartButton restartButton;

        private BattleEvents battleEvents;

        private void Start()
        {
            battleEvents = new BattleEvents();

            battleSystem.Initialize(battleEvents);
            battlePresenter.Initialize(battleEvents);

            restartButton.RestartRequested += RestartBattle;

            Character player1 = factory.CreatePlayer(true);
            Character player2 = factory.CreatePlayer(false);

            StartCoroutine(
                battleSystem.Fight(player1, player2));
        }

        private void OnDestroy()
        {
            if (restartButton != null)
            {
                restartButton.RestartRequested -= RestartBattle;
            }
        }

        private void RestartBattle()
        {
            Scene currentScene = SceneManager.GetActiveScene();

            SceneManager.LoadScene(
                currentScene.buildIndex);
        }
    }
}