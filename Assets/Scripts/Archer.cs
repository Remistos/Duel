using UnityEngine;

public class Archer : Character
{
    private void Start()
    {
        ClassName = "Лучник";
        MaxHealth = 110;
        DamageBase = 20;
        Health = MaxHealth;
    }
    public override void Attack(Character target)
    {
        Debug.Log("Лучник выпускает отравленную стрелу!");
        target.PoisonDuration = 3;
        dmg = Mathf.RoundToInt(DamageBase * DamageMultiplier);
        target.Health -= dmg;
    }
}
