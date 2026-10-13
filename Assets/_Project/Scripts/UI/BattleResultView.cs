using TMPro;
using UnityEngine;

namespace Duels.UI
{
    public sealed class BattleResultView : MonoBehaviour
    {
        [SerializeField] private GameObject winScreen;
        [SerializeField] private TextMeshProUGUI winText;

        public void ShowWinner(string winner)
        {
            winText.text = $"Победитель: {winner}";
            winScreen.SetActive(true);
        }
    }
}