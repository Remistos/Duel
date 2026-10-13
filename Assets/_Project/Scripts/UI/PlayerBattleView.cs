using System.Collections;
using Duels.Battle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Duels.UI
{
    public sealed class PlayerBattleView : MonoBehaviour
    {
        [Header("Health Bar")]
        [SerializeField] private float smoothSpeed = 5f;
        [SerializeField] private float lowHealthThreshold = 0.3f;
        [SerializeField] private float mediumHealthThreshold = 0.6f;

        [Header("Player UI")]
        [SerializeField] private TextMeshProUGUI playerName;
        [SerializeField] private TextMeshProUGUI playerHealth;
        [SerializeField] private TextMeshProUGUI playerDamage;
        [SerializeField] private TextMeshProUGUI playerEffects;
        [SerializeField] private Slider healthBar;
        [SerializeField] private Image healthFill;

        private Coroutine healthRoutine;
        private float targetHealth;
        private bool hasHealthState;

        public void ShowState(PlayerState state)
        {
            playerName.text = state.Name;
            playerHealth.text = $"HP: {state.Health}/{state.MaxHealth}";
            playerDamage.text = $"”рон: {state.Damage}";

            playerEffects.text = BuildEffectsText(
                state.Stunned,
                state.PoisonDuration,
                state.DebuffDuration);

            healthBar.maxValue = state.MaxHealth;

            if (!hasHealthState)
            {
                targetHealth = state.Health;
                healthBar.value = state.Health;
                hasHealthState = true;

                UpdateHealthColor();
                return;
            }

            if (Mathf.Approximately(targetHealth, state.Health))
            {
                return;
            }

            targetHealth = state.Health;

            if (healthRoutine != null)
            {
                StopCoroutine(healthRoutine);
            }

            healthRoutine = StartCoroutine(
                AnimateHealth(targetHealth));
        }

        private IEnumerator AnimateHealth(float target)
        {
            while (!Mathf.Approximately(healthBar.value, target))
            {
                healthBar.value = Mathf.Lerp(
                    healthBar.value,
                    target,
                    Time.deltaTime * smoothSpeed);

                UpdateHealthColor();

                yield return null;
            }

            healthBar.value = target;
            UpdateHealthColor();

            healthRoutine = null;
        }

        private string BuildEffectsText(
            bool isStunned,
            int poisonDuration,
            int debuffDuration)
        {
            string result = string.Empty;

            if (isStunned)
            {
                result += "ќглушение\n";
            }

            if (poisonDuration > 0)
            {
                result += $"яд ({poisonDuration})\n";
            }

            if (debuffDuration > 0)
            {
                result += $"ќслабление ({debuffDuration})\n";
            }

            return result;
        }

        private void UpdateHealthColor()
        {
            if (healthBar.maxValue <= 0)
            {
                return;
            }

            float healthPercent =
                healthBar.value / healthBar.maxValue;

            Color targetColor;

            if (healthPercent <= lowHealthThreshold)
            {
                targetColor = Color.red;
            }
            else if (healthPercent <= mediumHealthThreshold)
            {
                targetColor = Color.yellow;
            }
            else
            {
                targetColor = Color.green;
            }

            if (healthFill.color != targetColor)
            {
                healthFill.color = targetColor;
            }
        }

        private void OnDisable()
        {
            if (healthRoutine != null)
            {
                StopCoroutine(healthRoutine);
                healthRoutine = null;
            }
        }
    }
}