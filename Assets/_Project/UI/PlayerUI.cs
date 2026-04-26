using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI effectsText;

    private const float LowHpThreshold = 30f;
    private const float MidHpThreshold = 60f;

    public void UpdateView(Character character)
    {
        nameText.text = character.ClassName;
        healthText.text = $"HP: {character.Health}/{character.MaxHealth}";
        healthText.color = GetHpColor(character);

        damageText.text = $"Урон: {character.GetDamage()}";
        effectsText.text = GetEffects(character);
    }

    private Color GetHpColor(Character character)
    {
        float percent = (float)character.Health / character.MaxHealth * 100f;

        if (percent <= LowHpThreshold) return Color.red;
        if (percent <= MidHpThreshold) return Color.yellow;

        return Color.green;
    }

    private string GetEffects(Character character)
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
}