using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyCharacter : Character
{
    const float statIncreaseFactor = 1.1f;
    const int requiredXPincrease = 500;
    public const float guardDamageMultiplier = 0.5f;
    protected int levelsToUnlockSkill = 5;

    public int Skill { get; set; }
    public int MaxSkill { get; set; }
    public int Level { get; set; }
    public int CurrentXP { get; set; }
    public int XPToNextLevel { get; set; }
    public int UnlockedSkills { get; set; }
    public int SpriteIndex { get; set; }
    public bool IsGuarding { get; set; }
    public float HealingMultiplier {  get; set; }
    public float ItemEnhancementMultiplier {  get; set; }
    public string AbilityDescription { get; set; }
    public string CharacterDescription { get; set; }
    public List<PlayableSkill> skillSet = new List<PlayableSkill>();
    public Wearable[] wearablesWorn = new Wearable[4];

    public FriendlyCharacter()
    {
        NegativeEffectsImmunity = 0;
        Level = 1;
        CurrentXP = 0;
        XPToNextLevel = 500;
        UnlockedSkills = 2;
        ItemEnhancementMultiplier = 1;
        HealingMultiplier = 1;
        KnockedOut = false;
        IsGuarding = false;
        for (int i = 0; i < wearablesWorn.Length; i++)
        {
            wearablesWorn[i] = null;
        }
    }

    public override void HandleTimers()
    {
        HandleGuard();
        for (int i = 0; i < StatusTimers.Length; i++)
        {
            if (StatusTimers[i] > 0)
            {
                StatusTimers[i]--;
            }
            else if (StatusTimers[i] < 0)
            {
                StatusTimers[i]++;
            }
            if (StatusTimers[i] == 0)
            {
                CancelStatusEffect(i);
            }
        }
        if (NegativeEffectsImmunity > 0)
        {
            NegativeEffectsImmunity--;
        }
    }

    public List<string> HandleLevel(int exp)
    {

        CurrentXP += exp;
        List<string> charInfoLog = new List<string>();
        while (CurrentXP >= XPToNextLevel && Level < 20)
        {
            charInfoLog.Add(LevelUp());
        }
        return charInfoLog;
    }

    public override void Heal(int healing)
    {
        base.Heal((int)(healing* HealingMultiplier));
    }

    public override void Heal(float healing)
    {
        base.Heal(HealingMultiplier * healing);
    }

    public void RestoreSkill(int skill)
    {
        Skill = Mathf.Min(Skill + skill, MaxSkill);
    }

    public void RestoreSkill(float skill)
    {
        Skill = Mathf.Min(Skill + (int)(MaxSkill * skill), MaxSkill);
    }

    public void DepleteSkill(int skill)
    {
        Skill = Mathf.Max(Skill - skill, 0);
    }

    public void DepleteSkill(float skill)
    {
        Skill = Mathf.Max(Skill - (int)(MaxSkill * skill), 0);
    }

    virtual public void StartGuard()
    {
        IsGuarding = true;
        RestoreSkill(0.2f);
        HealingMultiplier = 1.5f;
    }

    virtual public void HandleGuard()
    {
        IsGuarding = false;
        HealingMultiplier = 1;
    }

    public string LevelUp()
    {
        Level++;
        string result = NominativeName + " osi�ga poziom " + Level;
        if (Level % levelsToUnlockSkill == 0 && UnlockedSkills < skillSet.Count)
        {
            UnlockedSkills++;
            result += " i odblokowuje umiej�tno�� " + skillSet[UnlockedSkills-1].Name;
        }
        result += "\n";
        MaxHealth = (int)(MaxHealth * statIncreaseFactor);
        MaxSkill = (int)(MaxSkill * statIncreaseFactor);
        DefaultAttack -= GetAttackFromWearables();
        DefaultAttack = (int)(DefaultAttack * statIncreaseFactor);
        DefaultAttack += GetAttackFromWearables();
        DefaultDefense -= GetDefenseFromWearables();
        DefaultDefense = (int)(DefaultDefense * statIncreaseFactor);
        DefaultDefense += GetDefenseFromWearables();
        CurrentXP -= XPToNextLevel;
        XPToNextLevel = 500 + (Level - 1) * requiredXPincrease;
        Debug.Log(NominativeName + " reached level " + Level);
        return result;
    }

    public int GetAttackFromWearables()
    {
        int result = 0;
        foreach (var wearable in wearablesWorn) {
            if (wearable == null) continue;
            result += wearable.AttackAdded;
        }
        return result;
    }

    public int GetDefenseFromWearables()
    {
        int result = 0;
        foreach (var wearable in wearablesWorn)
        {
            if (wearable == null) continue;
            result += wearable.DefenseAdded;
        }
        return result;
    }

    public float GetHealingFromWearables()
    {
        float result = 1;
        foreach (var wearable in wearablesWorn)
        {
            if (wearable == null) continue;
            result *= wearable.HealingMultiplier;
        }
        return result;
    }

    public float GetAccuracyFromWearables()
    {
        float result = 1;
        foreach (var wearable in wearablesWorn)
        {
            if (wearable == null) continue;
            result *= wearable.AccuracyMultiplier;
        }
        return result;
    }

    protected override void AdditionalChangesOnDeath()
    {
        IsGuarding = false;
    }

    protected override void AdditionalChangesOnReset()
    {
        Skill = MaxSkill;
        IsGuarding = false;
    }

    public override void TakeDamage(int damage)
    {
        if (IsGuarding)
        {
            damage /= 2;
        }
        base.TakeDamage(damage);
    }

    public virtual void OnItemUsed(Item item)
    {

    }
}
