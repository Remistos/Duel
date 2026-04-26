using UnityEngine;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private PlayerUI player1UI;
    [SerializeField] private PlayerUI player2UI;

    [SerializeField] private GameObject winScreen;
    [SerializeField] private TMPro.TextMeshProUGUI winText;

    public void UpdateUI(Character player1, Character player2)
    {
        player1UI.UpdateView(player1);
        player2UI.UpdateView(player2);
    }

    public void ShowWinner(string winner)
    {
        winScreen.SetActive(true);
        winText.text = $"Победитель: {winner}";
    }
}