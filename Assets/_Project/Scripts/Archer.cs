using UnityEngine;

public class Archer : Character
{
    private const int PoisonDuration = 3;

    protected override void Awake()
    {
        base.Awake();
        SetStats("Лучник", 110, 20);
    }

    public override void Attack(Character target)
    {
        Debug.Log("Лучник выпускает отравленную стрелу!");
        target.ApplyPoison(PoisonDuration);

        DealDamage(target);
    }
}
