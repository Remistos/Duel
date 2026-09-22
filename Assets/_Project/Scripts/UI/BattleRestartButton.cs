using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Duels.UI
{
    public sealed class BattleRestartButton : MonoBehaviour
    {
        [SerializeField] private Button restartButton;

        private void Awake()
        {
            restartButton.onClick.AddListener(RestartBattle);
        }

        private void OnDestroy()
        {
            restartButton.onClick.RemoveListener(RestartBattle);
        }

        private void RestartBattle()
        {
            Scene currentScene = SceneManager.GetActiveScene();

            SceneManager.LoadScene(currentScene.buildIndex);
        }
    }
}