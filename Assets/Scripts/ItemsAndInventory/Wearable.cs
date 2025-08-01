using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wearable
{
    public enum WEARABLES
    {
        WEAPON,
        ARMOR,
        DEFENSIVE,
        OFFENSIVE
    }
    public string Name { get; set; }
    public string Description { get; set; }
    public int AttackAdded { get; set; }
    public int DefenseAdded { get; set; }
    public float HealingMultiplier { get; set; }
    public float AccuracyMultiplier { get; set; }
    public int Cost { get; set; }
    public int Id { get; set; }
    public int Amount { get; set; }

    public void Add(int amount)
    {
        Amount += amount;
    }

    public abstract void PutOn(FriendlyCharacter target);
    public abstract void TakeOff(FriendlyCharacter target);
}
