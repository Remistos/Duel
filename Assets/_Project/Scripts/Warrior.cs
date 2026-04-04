using UnityEngine;


public class Warrior : Character
{
    private const float StunChance = 0.3f;
    private const int StunDuration = 1;
    protected override void Awake()
    {
        base.Awake();
        SetStats("Воин", 130, 25);
    }

    public override void Attack(Character target)
    {
        if (Random.value < StunChance)
        {
            Debug.Log("Воин оглушает противника!");
            target.ApplyStun(StunDuration);
        }

        DealDamage(target);
    }
}
