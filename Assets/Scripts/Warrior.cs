using UnityEngine;


public class Warrior :Character
{
    private void Start()
    {
        ClassName = "Воин";
        MaxHealth = 130;
        DamageBase = 25;
        Health = MaxHealth;

    }
    public override void Attack(Character target)
    {
        if(Random.value < 0.3f)
        {
            Debug.Log("Воин оглушает противника!");
            target.IsStunned = true;
            target.stunDuration = 1;
        }
        else
        {
            Debug.Log("Воин атакует без оглушения!");
        }
        dmg = Mathf.RoundToInt(DamageBase * DamageMultiplier);
        target.Health -= dmg;
    }
}
