namespace Duels.Battle
{
    public readonly struct BattleState
    {
        public BattleState(
            string player1Name,
            int player1Health,
            int player1MaxHealth,
            int player1Damage,
            bool player1Stunned,
            int player1PoisonDuration,
            int player1DebuffDuration,
            string player2Name,
            int player2Health,
            int player2MaxHealth,
            int player2Damage,
            bool player2Stunned,
            int player2PoisonDuration,
            int player2DebuffDuration)
        {
            Player1Name = player1Name;
            Player1Health = player1Health;
            Player1MaxHealth = player1MaxHealth;
            Player1Damage = player1Damage;
            Player1Stunned = player1Stunned;
            Player1PoisonDuration = player1PoisonDuration;
            Player1DebuffDuration = player1DebuffDuration;

            Player2Name = player2Name;
            Player2Health = player2Health;
            Player2MaxHealth = player2MaxHealth;
            Player2Damage = player2Damage;
            Player2Stunned = player2Stunned;
            Player2PoisonDuration = player2PoisonDuration;
            Player2DebuffDuration = player2DebuffDuration;
        }

        public string Player1Name { get; }
        public int Player1Health { get; }
        public int Player1MaxHealth { get; }
        public int Player1Damage { get; }
        public bool Player1Stunned { get; }
        public int Player1PoisonDuration { get; }
        public int Player1DebuffDuration { get; }

        public string Player2Name { get; }
        public int Player2Health { get; }
        public int Player2MaxHealth { get; }
        public int Player2Damage { get; }
        public bool Player2Stunned { get; }
        public int Player2PoisonDuration { get; }
        public int Player2DebuffDuration { get; }
    }
}