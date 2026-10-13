public sealed class CharacterHealth
{
    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; }
    public bool IsAlive => CurrentHealth > 0;

    public CharacterHealth(int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        CurrentHealth = System.Math.Max(CurrentHealth - damage, 0);
    }
}