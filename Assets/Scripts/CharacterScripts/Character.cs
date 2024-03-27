using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.Rendering.VolumeComponent;

public abstract class Character
{
    protected const float statBoostMultiplier = 1.25f;
    protected const float poisonAndRegenMultiplier = 0.1f;
    public int criticalDamageMultiplier = 2;

    public string NominativeName { get; set; }
    public string DativeName { get; set; }
    public string AccusativeName { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public float Accuracy { get; set; }
    public int Turns { get; set; }
    public int DefaultMaxHealth { get; set; }
    public int DefaultAttack { get; set; }
    public int DefaultDefense { get; set; }
    protected float DefaultAccuracy { get; set; }
    protected int DefaultTurns { get; set; }
    public int NegativeEffectsImmunity { get; set; }
    public int Speed { get; set; }
    public bool KnockedOut { get; set; }
    public int[] StatusTimers = new int[5];


    abstract public void HandleTimers();

    abstract public void HandleEffects();

    public void Reset()
    {
        KnockedOut = false;
        Health = MaxHealth;
        ResetTimers();
        AdditionalChangesOnReset();
    }

    void ResetTimers()
    {
        for (int i = 0;i < StatusTimers.Length;i++)
        {
            StatusTimers[i] = 0;
        }
        NegativeEffectsImmunity = 0;
    }

    public void Heal(int healing)
    {
        Health = Mathf.Min(Health + healing, MaxHealth);
    }

    public void Heal(float healing)
    {
        Health = Mathf.Min(Health + (int)(MaxHealth * healing), MaxHealth);
    }

    public void TakeDamage(int damage)
    {
        damage = Mathf.Max(damage, 1);
        Health = Mathf.Max(Health - damage, 0);
        if (Health == 0)
        {
            Death();
        }
    }

    public void TakeDamage(float damage)
    {
        Health = Mathf.Max(Health - (int)(Health * damage), 0);
        if (Health == 0)
        {
            Death(); 
        }
    }

    public void Death()
    {
        KnockedOut = true;
        ResetTimers();
        AdditionalChangesOnDeath();
    }

    virtual protected void AdditionalChangesOnReset()
    {

    }

    virtual protected void AdditionalChangesOnDeath()
    {

    }

}
