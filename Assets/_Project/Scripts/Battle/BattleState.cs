namespace Duels.Battle
{
    public readonly struct BattleState
    {
        public BattleState(PlayerState player1, PlayerState player2)
        {
            Player1 = player1;
            Player2 = player2;
        }

        public PlayerState Player1 { get; }

        public PlayerState Player2 { get; }
    }

    public readonly struct PlayerState
    {
        public PlayerState(
            string name,
            int health,
            int maxHealth,
            int damage,
            bool stunned,
            int poisonDuration,
            int debuffDuration)
        {
            Name = name;
            Health = health;
            MaxHealth = maxHealth;
            Damage = damage;
            Stunned = stunned;
            PoisonDuration = poisonDuration;
            DebuffDuration = debuffDuration;
        }

        public string Name { get; }

        public int Health { get; }

        public int MaxHealth { get; }

        public int Damage { get; }

        public bool Stunned { get; }

        public int PoisonDuration { get; }

        public int DebuffDuration { get; }
    }
}