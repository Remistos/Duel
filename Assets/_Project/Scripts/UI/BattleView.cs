using System.Collections;
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

        private Coroutine player1HealthRoutine;
        private Coroutine player2HealthRoutine;

        public void ShowState(BattleState state)
        {
            ShowPlayer1(state.Player1);
            ShowPlayer2(state.Player2);
        }

        public void ShowWinScreen(string winner)
        {
            winScreen.SetActive(true);
            winText.text = $"Победитель: {winner}";
        }

        private void ShowPlayer1(PlayerState state)
        {
            player1Name.text = state.Name;
            player1Health.text =
                $"HP: {state.Health}/{state.MaxHealth}";
            player1Damage.text =
                $"Урон: {state.Damage}";
            player1Effects.text = BuildEffectsText(
                state.Stunned,
                state.PoisonDuration,
                state.DebuffDuration);

            player1HealthBar.maxValue = state.MaxHealth;

            StartHealthAnimation(
                player1HealthBar,
                player1Fill,
                state.Health,
                ref player1HealthRoutine);
        }

        private void ShowPlayer2(PlayerState state)
        {
            player2Name.text = state.Name;
            player2Health.text =
                $"HP: {state.Health}/{state.MaxHealth}";
            player2Damage.text =
                $"Урон: {state.Damage}";
            player2Effects.text = BuildEffectsText(
                state.Stunned,
                state.PoisonDuration,
                state.DebuffDuration);

            player2HealthBar.maxValue = state.MaxHealth;

            StartHealthAnimation(
                player2HealthBar,
                player2Fill,
                state.Health,
                ref player2HealthRoutine);
        }

        private void StartHealthAnimation(
            Slider healthBar,
            Image fill,
            float targetHealth,
            ref Coroutine routine)
        {
            if (routine != null)
            {
                StopCoroutine(routine);
            }

            routine = StartCoroutine(
                AnimateHealth(
                    healthBar,
                    fill,
                    targetHealth));
        }

        private IEnumerator AnimateHealth(
            Slider healthBar,
            Image fill,
            float targetHealth)
        {
            while (!Mathf.Approximately(
                healthBar.value,
                targetHealth))
            {
                healthBar.value = Mathf.Lerp(
                    healthBar.value,
                    targetHealth,
                    Time.deltaTime * smoothSpeed);

                UpdateHealthColor(
                    fill,
                    healthBar.value,
                    healthBar.maxValue);

                yield return null;
            }

            healthBar.value = targetHealth;

            UpdateHealthColor(
                fill,
                healthBar.value,
                healthBar.maxValue);
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