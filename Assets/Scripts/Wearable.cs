using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wearable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int AttackAdded { get; set; }
    public int DefenseAdded { get; set; }
    public float HealingMultiplier { get; set; }
    public float AccuracyMultiplier { get; set; }
    public int Cost { get; set; }
    public int Amount { get; set; }

    public Wearable(string name, string description, int attackAdded, int defenseAdded, float healingMultiplier, float accuracyMultiplier, int cost, int amount = 0)
    {
        Name = name;
        Description = description;
        AttackAdded = attackAdded;
        DefenseAdded = defenseAdded;
        HealingMultiplier = healingMultiplier;
        AccuracyMultiplier = accuracyMultiplier;
        Cost = cost;
        Amount = amount;
    }

    public void Add(int amount)
    {
        Amount += amount;
    }
}
