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
    public bool IsGuarding { get; set; }
    public List<PlayableSkill> skillSet = new List<PlayableSkill>();
    public Wearable[] wearablesWorn = new Wearable[4];

    public FriendlyCharacter()
    {
        NegativeEffectsImmunity = 0;
        Level = 1;
        CurrentXP = 0;
        XPToNextLevel = 500;
        UnlockedSkills = 1;
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
                if (NegativeEffectsImmunity > 0)
                {
                    StatusTimers[i] = 0;
                }
                else
                {
                    StatusTimers[i]++;
                }
            }
        }
        if (NegativeEffectsImmunity > 0)
        {
            NegativeEffectsImmunity--;
        }
    }

    public void HandleLevel(int exp)
    {
        CurrentXP += exp;
        while (CurrentXP >= XPToNextLevel && Level < 20)
        {
            LevelUp();
        }
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
        Skill = Mathf.Max(Skill - (int)(MaxSkill * skill), MaxSkill);
    }

    virtual public void StartGuard()
    {
        IsGuarding = true;
        RestoreSkill(0.2f);
    }

    virtual public void HandleGuard()
    {
        IsGuarding = false;
    }

    override public void HandleEffects()
    {
        for (int i = 0; i < StatusTimers.Length; i++)
        {
            float multiplier = 1;
            int type = 0;
            if (StatusTimers[i] > 0)
            {
                multiplier = statBoostMultiplier;
                type = 1; 
            }
            else if (StatusTimers[i] < 0)
            {
                multiplier = 1 / statBoostMultiplier;
                type = 2; 
            }

            switch (i)
            {
                case 0: //attack debuff
                    Attack = (int)(DefaultAttack * multiplier);
                    break;
                case 1: //defense debuff
                    Defense = (int)(DefaultDefense * multiplier);
                    break;
                case 2: //accuracy debuff
                    Accuracy = DefaultAccuracy * multiplier;
                    break;
                case 3: //poison
                    if(type == 1)
                    {
                        Health = Mathf.Min((int)(Health + poisonAndRegenMultiplier * MaxHealth), MaxHealth);
                    }
                    else if(type == 2)
                    {
                        Health = Mathf.Max((int)(Health * (1 - poisonAndRegenMultiplier)), 1);
                    }
                    break;
                case 4: //paralysis
                    if(type == 1)
                    {
                        Turns = DefaultTurns + 1;
                    }
                    else if(type == 2)
                    {
                        Turns = 0;
                    }
                    else
                    {
                        Turns = DefaultTurns;
                    }
                    break;
            } 
        }
    }

    public void LevelUp()
    {
        Level++;
        if (Level % levelsToUnlockSkill == 0 && UnlockedSkills < skillSet.Count)
        {
            UnlockedSkills++;
        }
        MaxHealth = (int)(MaxHealth * statIncreaseFactor);
        MaxSkill = (int)(MaxSkill * statIncreaseFactor);
        DefaultAttack = (int)(DefaultAttack * statIncreaseFactor);
        DefaultDefense = (int)(DefaultDefense * statIncreaseFactor);
        CurrentXP -= XPToNextLevel;
        XPToNextLevel += requiredXPincrease;
        Debug.Log(NominativeName + " reached level " + Level);
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
}
