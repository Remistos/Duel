using UnityEngine;

public class Mage : Character
{
    private const int DebuffDuration = 2;

    protected override void Awake()
    {
        base.Awake();
        SetStats("Маг", 100, 15);
    }

    public override void Attack(Character target)
    {
        Debug.Log("Маг накладывает дебафф!");
        target.ApplyDebuff(DebuffDuration);

        DealDamage(target);
    }
}