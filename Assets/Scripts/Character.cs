using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.Rendering.VolumeComponent;

public class Character
{
    const float statIncreaseFactor = 1.1f;
    const int requiredXPincrease = 500;
    const float statBoostMultiplier = 1.25f;
    const float poisonAndRegenMultiplier = 0.1f;

    public string Name { get; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int DefaultMaxHealth { get; set; }
    public int DifficultyHealthChange { get; set; }
    public int Skill { get; set; }
    public int MaxSkill { get; set; }
    public int Attack { get; set; }
    public int DifficultyAttackChange { get; set; }
    public int Defense { get; set; }
    public float Accuracy { get; set; }
    public int Turns { get; set; }
    public int DefaultAttack { get; set; }
    public int DefaultDefense { get; set; }
    float DefaultAccuracy { get; set; }
    int DefaultTurns { get; set; }
    public int NegativeEffectsImmunity { get; set; }
    public int Speed { get; }
    public int MoneyDropped { get; }
    public int XPDropped { get; }
    public int Level { get; set; }
    public int CurrentXP { get; set; }
    public int XPToNextLevel { get; set; }
    public int UnlockedSkills { get; set; }
    public bool KnockedOut { get; set; }
    public bool IsGuarding { get; set; }
    public int[] PositiveStatusTimers = new int[5];
    public int[] NegativeStatusTimers = new int[5];
    public List<Skill> skillSet = new List<Skill>();
    public Wearable[] wearablesWorn = new Wearable[4];
    public Character(string name, int defaultMaxHealth, int difficultyHealthChange, int maxSkill, int attack, int difficultyAttackChange, int defense,
        float accuracy, int turns, int speed, int moneyDropped, int xpDropped, List<Skill> skills)
    {
        Name = name;
        Health = MaxHealth = DefaultMaxHealth = defaultMaxHealth;
        DifficultyHealthChange = difficultyHealthChange;
        Skill = MaxSkill = maxSkill;
        Attack = DefaultAttack = attack;
        DifficultyAttackChange = difficultyAttackChange;
        Defense = DefaultDefense = defense;
        Accuracy = DefaultAccuracy = accuracy;
        Turns = DefaultTurns = turns;
        NegativeEffectsImmunity = 0;
        Speed = speed;
        MoneyDropped = moneyDropped;
        XPDropped = xpDropped;
        Level = 1;
        CurrentXP = 0;
        XPToNextLevel = 500;
        UnlockedSkills = 1;
        KnockedOut = false;
        IsGuarding = false;
        foreach (Skill skill in skills)
        {
            skillSet.Add(skill);
        }
        for (int i = 0; i < wearablesWorn.Length; i++)
        {
            wearablesWorn[i] = null;
        }
    }

    void LevelUp()
    {
        Level++;
        if (Level % 5 == 0)
        {
            UnlockedSkills++;
        }
        MaxHealth = (int)(MaxHealth * statIncreaseFactor);
        MaxSkill = (int)(MaxSkill * statIncreaseFactor);
        DefaultAttack = (int)(DefaultAttack * statIncreaseFactor);
        DefaultDefense = (int)(DefaultDefense * statIncreaseFactor);
        CurrentXP -= XPToNextLevel;
        XPToNextLevel += requiredXPincrease;
        Debug.Log(Name + " reached level " + Level);
    }

    public void HandleLevel()
    {
        while (CurrentXP >= XPToNextLevel && Level < 20)
        {
            LevelUp();
        }
    }

    public void HandleTimers()
    {
        for (int i=0; i < PositiveStatusTimers.Length; i++)
        {
            if (PositiveStatusTimers[i] > 0)
            {
                PositiveStatusTimers[i]--;
            }
        }
        if (NegativeEffectsImmunity > 0)
        {
            NegativeEffectsImmunity--;
        }
        for (int i = 0; i < NegativeStatusTimers.Length; i++)
        {
            if (NegativeStatusTimers[i] > 0)
            {
                NegativeStatusTimers[i]--;
            }
        }
    }

    public void HandleEffects()
    {
        for (int i = 0; i < PositiveStatusTimers.Length; i++)
        {
            if (PositiveStatusTimers[i] > 0 || NegativeStatusTimers[i] > 0)
            {
                if (PositiveStatusTimers[i] > 0)
                {
                    switch (i)
                    {
                        case 0:
                            Attack = (int)(statBoostMultiplier * DefaultAttack + GameManager.instance.difficulty * DifficultyAttackChange);
                            break;
                        case 1:
                            Defense = (int)(statBoostMultiplier * DefaultDefense);
                            break;
                        case 2:
                            Accuracy = (int)(statBoostMultiplier * DefaultAccuracy);
                            break;
                        case 3:
                            Health = Mathf.Min((int)(Health + poisonAndRegenMultiplier * MaxHealth), MaxHealth);
                            break;
                        case 4:
                            Turns = DefaultTurns + 1;
                            break;
                    }
                }
                else
                {
                    switch (i)
                    {
                        case 0: //attack debuff
                            Attack = (int)((DefaultAttack + GameManager.instance.difficulty * DifficultyAttackChange) / statBoostMultiplier);
                            break;
                        case 1: //defense debuff
                            Defense = (int)(DefaultDefense / statBoostMultiplier);
                            break;
                        case 2: //accuracy debuff
                            Accuracy = DefaultAccuracy / statBoostMultiplier;
                            break;
                        case 3: //poison
                            Health = Mathf.Max((int)(Health * (1 - poisonAndRegenMultiplier)), 1);
                            break;
                        case 4: //paralysis
                            Turns = 0;
                            break;
                    }
                }
            }
            else
            {
                switch (i)
                {
                    case 0:
                        Attack = DefaultAttack + GameManager.instance.difficulty * DifficultyAttackChange;
                        break;
                    case 1:
                        Defense = DefaultDefense;
                        break;
                    case 2:
                        Accuracy = DefaultAccuracy;
                        break;
                    case 4:
                        Turns = DefaultTurns;
                        break;
                }
            }
        }
    }

    public void Reset()
    {
        KnockedOut = false;
        Health = MaxHealth;
        Skill = MaxSkill;
        IsGuarding = false;
        for (int i = 0; i < PositiveStatusTimers.Length; i++)
        {
            PositiveStatusTimers[i] = 0;
        }
        for (int i = 0; i < NegativeStatusTimers.Length; i++)
        {
            NegativeStatusTimers[i] = 0;
        }
    }

    public void UpdateDifficulty()
    {
        MaxHealth = DefaultMaxHealth + GameManager.instance.difficulty * DifficultyHealthChange;
    }

    public void Death()
    {
        KnockedOut = true;
        IsGuarding = false;
    }

}
