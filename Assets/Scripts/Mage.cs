using UnityEngine;

public class Mage : Character
{
    private void Start()
    {
        ClassName = "Маг";
        MaxHealth = 100;
        DamageBase = 15;
        Health = MaxHealth;
    }
    public override void Attack(Character target)
    {
        Debug.Log("Маг атакует и накладывает дебафф!");
        target.DebuffDuration = 2;
        dmg = Mathf.RoundToInt(DamageBase * DamageMultiplier);
        target.Health -= dmg;
    }
}
