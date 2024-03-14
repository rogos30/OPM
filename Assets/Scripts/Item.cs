using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Item
{
    public string Name { get; set; }
    string DefaultDescription { get; set; }
    public string Description { get; set; }
    public float HealthRestored {  get; set; }
    public float SkillRestored { get; set; }
    public bool Resurrects { get; }
    int EffectImmunity { get; }
    public int Amount { get; set; }
    public int Cost { get; set; }

    public Item(string name, string description, float healthRestored, float skillRestored, bool resurrects, int clearsEffects, int cost, int amount = 0)
    {
        Name = name;
        DefaultDescription = description;
        HealthRestored = healthRestored;
        SkillRestored = skillRestored;
        Resurrects = resurrects;
        EffectImmunity = clearsEffects;
        Cost = cost;
        Amount = amount;
        UpdateDescription();
    }

    public void UpdateDescription()
    {
        if (HealthRestored > 0)
        {
            if (HealthRestored > 1)
            {
                Description = DefaultDescription + HealthRestored + "HP";
            }
            else
            {
                Description = DefaultDescription + HealthRestored * 100 + "%HP";
            }
        }
        else if (SkillRestored > 0)
        {
            if (SkillRestored > 1)
            {
                Description = DefaultDescription + SkillRestored + "SP";
            }
            else
            {
                Description = DefaultDescription + SkillRestored * 100 + "%SP";
            }
        }
        else if (EffectImmunity > 0)
        {
            Description = DefaultDescription + " i zapewnia odpornoœæ na nie na " + EffectImmunity + " tur" + (EffectImmunity == 1 ? "ê" : "y");
        }
        else
        {
            Description = DefaultDescription;
        }
    }

    public void Add(int amount)
    {
        Amount += amount;
    }
    public string Use(Character target)
    {
        string finalDesc = target.Name + " ";
        float multiplier = 1;
        if (Resurrects && target.KnockedOut)
        {
            target.KnockedOut = false;
            finalDesc += "wraca do ¿ywych i ";
        }
        if (target.IsGuarding)
        {
            multiplier = 1.5f;
        }
        foreach (var wearable in target.wearablesWorn)
        {
            if (wearable == null) continue;
            multiplier *= wearable.HealingMultiplier;
        }
        if (HealthRestored > 1)
        {
            target.Health = Mathf.Min(target.Health + (int)(HealthRestored * multiplier), target.MaxHealth);
            finalDesc += "odzyskuje " + (int)(HealthRestored * multiplier) + " HP";
        }
        else if (HealthRestored > 0)
        {
            target.Health = Mathf.Min(target.Health + (int)(target.MaxHealth * HealthRestored), target.MaxHealth);
            finalDesc += "odzyskuje " + HealthRestored * 100 + "% HP";
        }

        if (SkillRestored > 1)
        {
            target.Skill = Mathf.Min(target.Skill + (int)SkillRestored, target.MaxSkill);
            finalDesc += "odzyskuje " + SkillRestored + " SP";
        }
        else if (SkillRestored > 0)
        {
            target.Skill = Mathf.Min(target.Skill + (int)(target.MaxSkill * SkillRestored), target.MaxSkill);
            finalDesc += "odzyskuje " + SkillRestored * 100 + "% SP";
        }

        if (EffectImmunity > 0)
        {
            for (int i=0; i < target.NegativeStatusTimers.Length; i++)
            {
                target.NegativeStatusTimers[i] = 0;
            }
            finalDesc += " pozbywa siê negatywnych efektów";
            if (EffectImmunity > 1)
            {
                finalDesc += " na " + EffectImmunity + " tur!";
            }
        }
        Amount--;
        return finalDesc;
    }

}
