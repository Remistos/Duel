using Duels.Battle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Duels.UI
{
    public sealed class BattleView : MonoBehaviour
    {
        [Header("Health Bar")]
        [SerializeField] private float smoothSpeed = 5f;
        [SerializeField] private float lowHealthThreshold = 0.3f;
        [SerializeField] private float mediumHealthThreshold = 0.6f;

        [Header("Player 1 UI")]
        [SerializeField] private TextMeshProUGUI player1Name;
        [SerializeField] private TextMeshProUGUI player1Health;
        [SerializeField] private TextMeshProUGUI player1Damage;
        [SerializeField] private TextMeshProUGUI player1Effects;
        [SerializeField] private Slider player1HealthBar;
        [SerializeField] private Image player1Fill;

        [Header("Player 2 UI")]
        [SerializeField] private TextMeshProUGUI player2Name;
        [SerializeField] private TextMeshProUGUI player2Health;
        [SerializeField] private TextMeshProUGUI player2Damage;
        [SerializeField] private TextMeshProUGUI player2Effects;
        [SerializeField] private Slider player2HealthBar;
        [SerializeField] private Image player2Fill;

        [Header("Win Screen")]
        [SerializeField] private GameObject winScreen;
        [SerializeField] private TextMeshProUGUI winText;

        private float player1TargetHealth;
        private float player2TargetHealth;

        private void Awake()
        {
            player1TargetHealth = player1HealthBar.value;
            player2TargetHealth = player2HealthBar.value;
        }

        private void Update()
        {
            player1HealthBar.value = Mathf.Lerp(
                player1HealthBar.value,
                player1TargetHealth,
                Time.deltaTime * smoothSpeed);

            player2HealthBar.value = Mathf.Lerp(
                player2HealthBar.value,
                player2TargetHealth,
                Time.deltaTime * smoothSpeed);

            UpdateHealthColor(
                player1Fill,
                player1HealthBar.value,
                player1HealthBar.maxValue);

            UpdateHealthColor(
                player2Fill,
                player2HealthBar.value,
                player2HealthBar.maxValue);
        }

        public void ShowState(BattleState state)
        {
            player1Name.text = state.Player1Name;
            player1Health.text =
                $"HP: {state.Player1Health}/{state.Player1MaxHealth}";
            player1Damage.text =
                $"Урон: {state.Player1Damage}";
            player1Effects.text = BuildEffectsText(
                state.Player1Stunned,
                state.Player1PoisonDuration,
                state.Player1DebuffDuration);

            player1HealthBar.maxValue = state.Player1MaxHealth;
            player1TargetHealth = state.Player1Health;

            player2Name.text = state.Player2Name;
            player2Health.text =
                $"HP: {state.Player2Health}/{state.Player2MaxHealth}";
            player2Damage.text =
                $"Урон: {state.Player2Damage}";
            player2Effects.text = BuildEffectsText(
                state.Player2Stunned,
                state.Player2PoisonDuration,
                state.Player2DebuffDuration);

            player2HealthBar.maxValue = state.Player2MaxHealth;
            player2TargetHealth = state.Player2Health;
        }

        public void ShowWinScreen(string winner)
        {
            winScreen.SetActive(true);
            winText.text = $"Победитель: {winner}";
        }

        private string BuildEffectsText(
            bool isStunned,
            int poisonDuration,
            int debuffDuration)
        {
            string result = string.Empty;

            if (isStunned)
            {
                result += "Оглушение\n";
            }

            if (poisonDuration > 0)
            {
                result += $"Яд ({poisonDuration})\n";
            }

            if (debuffDuration > 0)
            {
                result += $"Ослабление ({debuffDuration})\n";
            }

            return result;
        }

        private void UpdateHealthColor(
            Image fill,
            float currentHealth,
            float maxHealth)
        {
            if (maxHealth <= 0)
            {
                return;
            }

            float healthPercent = currentHealth / maxHealth;

            if (healthPercent <= lowHealthThreshold)
            {
                fill.color = Color.red;
            }
            else if (healthPercent <= mediumHealthThreshold)
            {
                fill.color = Color.yellow;
            }
            else
            {
                fill.color = Color.green;
            }
        }
    }
}