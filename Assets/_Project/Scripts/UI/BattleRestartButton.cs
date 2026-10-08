using System;
using UnityEngine;
using UnityEngine.UI;

namespace Duels.UI
{
    public sealed class BattleRestartButton : MonoBehaviour
    {
        [SerializeField] private Button restartButton;

        public event Action RestartRequested;

        private void Awake()
        {
            restartButton.onClick.AddListener(OnRestartClicked);
        }

        private void OnDestroy()
        {
            restartButton.onClick.RemoveListener(OnRestartClicked);
        }

        private void OnRestartClicked()
        {
            RestartRequested?.Invoke();
        }
    }
}