using Duels.Battle;
using UnityEngine;

namespace Duels.UI
{
    public sealed class BattleView : MonoBehaviour
    {
        [SerializeField] private PlayerBattleView player1View;
        [SerializeField] private PlayerBattleView player2View;
        [SerializeField] private BattleResultView resultView;

        public void ShowState(BattleState state)
        {
            player1View.ShowState(state.Player1);
            player2View.ShowState(state.Player2);
        }

        public void ShowWinScreen(string winner)
        {
            resultView.ShowWinner(winner);
        }
    }
}