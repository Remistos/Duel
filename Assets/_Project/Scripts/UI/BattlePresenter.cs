using Duels.Battle;
using UnityEngine;

namespace Duels.UI
{
    public sealed class BattlePresenter : MonoBehaviour
    {
        [SerializeField] private BattleView view;

        private BattleEvents battleEvents;

        public void Initialize(BattleEvents events)
        {
            Unsubscribe();

            battleEvents = events;

            if (battleEvents == null)
            {
                return;
            }

            battleEvents.StateChanged += OnStateChanged;
            battleEvents.BattleFinished += OnBattleFinished;
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void OnStateChanged(BattleState state)
        {
            view.ShowState(state);
        }

        private void OnBattleFinished(string winner)
        {
            view.ShowWinScreen(winner);
        }

        private void Unsubscribe()
        {
            if (battleEvents == null)
            {
                return;
            }

            battleEvents.StateChanged -= OnStateChanged;
            battleEvents.BattleFinished -= OnBattleFinished;
        }
    }
}