using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public abstract class Character : MonoBehaviour
{
    public string ClassName;
    public int Health;
    public int DamageBase;
    public int dmg;
    public int MaxHealth;
    public bool IsStunned = false;
    public int stunDuration = 1;
    public int PoisonDuration = 0;
    public int DebuffDuration = 0;
    public float DamageMultiplier = 1f;
    public abstract void Attack(Character target);
    public void ProcessEffects()
    {
        if (IsStunned)
        {
            Debug.Log(ClassName + " оглушен и пропустит ход!");
            IsStunned = true;
            return;
        }
        if (PoisonDuration > 0)
        {
            PoisonDuration--;
            Health -= 5;
            Debug.Log(ClassName + " получает 5 урона от яда. Остаток жизни: " + Health);
        }
        if (DebuffDuration > 0)
        {
            DebuffDuration--;
            DamageMultiplier = 0.5f;
        }
        else
        {
            DamageMultiplier = 1f;
        }
    }
}