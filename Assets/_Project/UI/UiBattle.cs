using UnityEngine;
using TMPro;

public class UIBattle : MonoBehaviour
{
    [Header("Player1 UI")]
    [SerializeField] private TextMeshProUGUI player1Name;
    [SerializeField] private TextMeshProUGUI player1Health;
    [SerializeField] private TextMeshProUGUI player1Damage;
    [SerializeField] private TextMeshProUGUI player1Effects;

    [Header("Player2 UI")]
    [SerializeField] private TextMeshProUGUI player2Name;
    [SerializeField] private TextMeshProUGUI player2Health;
    [SerializeField] private TextMeshProUGUI player2Damage;
    [SerializeField] private TextMeshProUGUI player2Effects;

    [Header("Win Screen")]
    [SerializeField] private GameObject winScreen;
    [SerializeField] private TextMeshProUGUI winText;

    private const float LowHpThreshold = 30f;
    private const float MidHpThreshold = 60f;

    public void UpdateUI(Character player1, Character player2)
    {
        UpdatePlayer(player1, player1Name, player1Health, player1Damage, player1Effects);
        UpdatePlayer(player2, player2Name, player2Health, player2Damage, player2Effects);
    }

    private void UpdatePlayer(
        Character character,
        TextMeshProUGUI nameText,
        TextMeshProUGUI healthText,
        TextMeshProUGUI damageText,
        TextMeshProUGUI effectsText)
    {
        nameText.text = character.ClassName;

        healthText.text = $"HP: {character.Health}/{character.MaxHealth}";
        healthText.color = GetHPColor(character.Health, character.MaxHealth);

        damageText.text = $"Урон: {character.GetDamage()}";
        effectsText.text = EffectsToText(character);
    }

    private Color GetHPColor(float current, float max)
    {
        float percent = (current / max) * 100f;

        if (percent <= LowHpThreshold)
            return Color.red;
        if (percent <= MidHpThreshold)
            return Color.yellow;

        return Color.green;
    }

    private string EffectsToText(Character character)
    {
        string result = "";

        if (character.IsStunned)
            result += "Оглушен\n";

        if (character.HasPoison())
            result += "Отравлен\n";

        if (character.HasDebuff())
            result += "Ослаблен\n";

        return result;
    }

    public void ShowWinScreen(string winner)
    {
        winScreen.SetActive(true);
        winText.text = $"Победитель: {winner}";
    }
}