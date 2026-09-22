using System;

namespace Duels.Battle
{
    public sealed class BattleEvents
    {
        public event Action<BattleState> StateChanged;
        public event Action<string> BattleFinished;

        public void RaiseStateChanged(BattleState state)
        {
            StateChanged?.Invoke(state);
        }

        public void RaiseBattleFinished(string winnerName)
        {
            BattleFinished?.Invoke(winnerName);
        }
    }
}