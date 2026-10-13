using Duels.Characters;

namespace Duels.Battle
{
    public sealed class BattleController
    {
        private readonly Character player1;
        private readonly Character player2;
        private readonly ICharacterAttack player1Attack;
        private readonly ICharacterAttack player2Attack;
        private readonly BattleEvents battleEvents;

        private bool isPlayer1Turn = true;
        private bool isFinished;

        public bool IsFinished => isFinished;

        public BattleController(
            Character player1,
            Character player2,
            ICharacterAttack player1Attack,
            ICharacterAttack player2Attack,
            BattleEvents battleEvents)
        {
            this.player1 = player1;
            this.player2 = player2;
            this.player1Attack = player1Attack;
            this.player2Attack = player2Attack;
            this.battleEvents = battleEvents;
        }

        public void StartBattle()
        {
            PublishState();
        }

        public void PerformTurn()
        {
            if (isFinished)
                return;

            Character attacker = isPlayer1Turn ? player1 : player2;
            Character target = isPlayer1Turn ? player2 : player1;

            ICharacterAttack attack = isPlayer1Turn
                ? player1Attack
                : player2Attack;

            attacker.ProcessEffects();

            if (attacker.IsAlive)
            {
                if (attacker.IsStunned)
                    attacker.ProcessStun();
                else
                    attack.Attack(target);
            }

            isFinished = !player1.IsAlive || !player2.IsAlive;

            if (!isFinished)
                isPlayer1Turn = !isPlayer1Turn;
        }

        public void PublishState()
        {
            BattleState state = new BattleState(
                player1.GetState(),
                player2.GetState());

            battleEvents.RaiseStateChanged(state);
        }

        public void FinishBattle()
        {
            if (!isFinished)
                return;

            string winner = player1.IsAlive
                ? player1.Definition.ClassName
                : player2.Definition.ClassName;

            battleEvents.RaiseBattleFinished(winner);
        }
    }
}