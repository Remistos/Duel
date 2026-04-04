using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public abstract class Character : MonoBehaviour
{
    [field: SerializeField] public string ClassName { get; private set; }
    [field: SerializeField] public int MaxHealth { get; private set; }
    [field: SerializeField] public int DamageBase { get; private set; }

    public int Health { get; private set; }
    public bool IsStunned { get; private set; }

    private int stunDuration;
    private int poisonDuration;
    private int debuffDuration;

    private float damageMultiplier = 1f;

    private const int PoisonDamage = 5;

    protected virtual void Awake()
    {
        Health = MaxHealth;
    }

    protected void SetStats(string className, int maxHealth, int damage)
    {
        ClassName = className;
        MaxHealth = maxHealth;
        DamageBase = damage;
        Health = maxHealth;
    }
    public abstract void Attack(Character target);

    public void ProcessEffects()
    {
        if (IsStunned)
        {
            stunDuration--;

            if (stunDuration <= 0)
                IsStunned = false;

            Debug.Log(ClassName + " оглушен!");
            return;
        }

        if (poisonDuration > 0)
        {
            poisonDuration--;
            TakeDamage(PoisonDamage);
        }

        if (debuffDuration > 0)
        {
            debuffDuration--;
            damageMultiplier = 0.5f;
        }
        else
        {
            damageMultiplier = 1f;
        }
    }

    protected void DealDamage(Character target)
    {
        int damage = Mathf.RoundToInt(DamageBase * damageMultiplier);
        target.TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health < 0)
            Health = 0;
    }

    public void ApplyStun(int duration)
    {
        IsStunned = true;
        stunDuration = duration;
    }

    public void ApplyPoison(int duration)
    {
        poisonDuration = duration;
    }

    public void ApplyDebuff(int duration)
    {
        debuffDuration = duration;
    }
    public int GetDamage()
    {
        return Mathf.RoundToInt(DamageBase * damageMultiplier);
    }

    public bool HasPoison()
    {
        return poisonDuration > 0;
    }

    public bool HasDebuff()
    {
        return debuffDuration > 0;
    }
    public bool IsAlive()
    {
        return Health > 0;
    }
}