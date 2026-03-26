using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class UIBattle : MonoBehaviour
{
    public float smoothSpeed = 5f;
    [Header("Player1 UI")]
    public TextMeshProUGUI p1Name;
    public TextMeshProUGUI p1Health;
    public TextMeshProUGUI p1Damage;
    public TextMeshProUGUI p1Effects;
    public Slider player1HP;
    private float p1SmoothHP;
    public Image player1Fill;
    

    [Header("Player2 UI")]
    public TextMeshProUGUI p2Name;
    public TextMeshProUGUI p2Health;
    public TextMeshProUGUI p2Damage;
    public TextMeshProUGUI p2Effects;
    public Slider player2HP;
    private float p2SmoothHP;
    public Image player2Fill;

    [Header("Win Screen")]
    public GameObject winScreen;
    public TextMeshProUGUI winText;

    void Start()
    {
        p1SmoothHP = player1HP.value;
        p2SmoothHP = player2HP.value;
    }

    public void UpdateUI(Character p1, Character p2)

    {
        p1Name.text = p1.ClassName;
        p1Health.text = $"HP: {p1.Health}/{p1.MaxHealth}";
        p1Damage.text = "Урон:" + p1.dmg;
        p1Effects.text = EffectsToText(p1);

        player1HP.maxValue = p1.MaxHealth;
        p1SmoothHP = p1.Health;

        p2Name.text = p2.ClassName;
        p2Health.text = $"HP: {p2.Health}/{p2.MaxHealth}";
        p2Damage.text = "Урон:" + p2.dmg;
        p2Effects.text = EffectsToText(p2);

        player2HP.maxValue = p2.MaxHealth;
        p2SmoothHP = p2.Health;

        player1HP.value = Mathf.Lerp(p1SmoothHP, player1HP.value, Time.deltaTime * smoothSpeed);

        player2HP.value = Mathf.Lerp(p2SmoothHP, player2HP.value, Time.deltaTime * smoothSpeed);
        player1HP.value = Mathf.Lerp(player1HP.value, p1SmoothHP, Time.deltaTime * smoothSpeed);
        player2HP.value = Mathf.Lerp(player2HP.value, p2SmoothHP, Time.deltaTime * smoothSpeed);
        UpdateHPColor(player1Fill, player1HP.value, player1HP.maxValue);
        UpdateHPColor(player2Fill, player2HP.value, player2HP.maxValue);
    }
    string EffectsToText(Character p)
    {
        string result = "";
        if (p.IsStunned)

            result += "Оглушен\n";
        if (p.PoisonDuration > 0) result += "Отравлен (" + p.PoisonDuration + ")\n";
        if (p.DebuffDuration > 0) result += "Дебафф (" + p.DebuffDuration + ")\n";
        return result;
    }
    void UpdateHPColor(Image fill, float current, float max)
    {
        float percent = (current / max) * 100f;

        if (percent <= 30f)
        {
            fill.color = Color.red;
        }
        else if (percent <= 60f)
        {
            fill.color = Color.yellow;
        }
        else
        {
            fill.color = Color.green;
        }
    }

    public void ShowWinScreen(string winner)
    {
        winScreen.SetActive(true);
        winText.text = "Победитель: " + winner;
    }



}