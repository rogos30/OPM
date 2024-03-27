using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    public int DifficultyHealthChange { get; set; }
    public int DifficultyAttackChange { get; set; }
    public int MoneyDropped { get; set; }
    public int XPDropped { get; set; }
    public List<EnemySkill> skillSet = new List<EnemySkill>();

    /*public EnemyCharacter(string name, int defaultMaxHealth, int difficultyHealthChange, int attack, int difficultyAttackChange, int defense,
        float accuracy, int turns, int speed, int moneyDropped, int xpDropped, List<Skill> skills)
    {
        NominativeName = name;
        Health = MaxHealth = DefaultMaxHealth = defaultMaxHealth;
        DifficultyHealthChange = difficultyHealthChange;
        Attack = DefaultAttack = attack;
        DifficultyAttackChange = difficultyAttackChange;
        Defense = DefaultDefense = defense;
        Accuracy = DefaultAccuracy = accuracy;
        Turns = DefaultTurns = turns;
        NegativeEffectsImmunity = 0;
        Speed = speed;
        MoneyDropped = moneyDropped;
        XPDropped = xpDropped;
        KnockedOut = false;
        foreach (Skill skill in skills)
        {
            skillSet.Add(skill);
        }
    }*/

    public void UpdateDifficulty(int difficulty)
    {
        MaxHealth = DefaultMaxHealth + difficulty * DifficultyHealthChange;
    }

    public override void HandleTimers()
    {
        for (int i = 0; i < StatusTimers.Length; i++)
        {
            if (StatusTimers[i] > 0)
            {
                StatusTimers[i]--;
            }
        }
        if (NegativeEffectsImmunity > 0)
        {
            NegativeEffectsImmunity--;
        }
    }

    override public void HandleEffects()
    {
        for (int i = 0; i < StatusTimers.Length; i++)
        {
            float constant = 1;
            int type = 0;
            if (StatusTimers[i] > 0)
            {
                constant = statBoostMultiplier;
                type = 1;
            }
            else if (StatusTimers[i] < 0)
            {
                constant = 1 / statBoostMultiplier;
                type = 2;
            }

            switch (i)
            {
                case 0: //attack change
                    Attack = (int)((DefaultAttack + GameManager.instance.difficulty * DifficultyAttackChange) * constant);
                    break;
                case 1: //defense change
                    Defense = (int)(DefaultDefense * constant);
                    break;
                case 2: //accuracy change
                    Accuracy = DefaultAccuracy * constant;
                    break;
                case 3: //health change
                    if (type == 1)
                    { //regeneration
                        Heal(poisonAndRegenMultiplier);
                    }
                    else if (type == 2)
                    { //poison
                        TakeDamage(poisonAndRegenMultiplier);
                    }
                    break;
                case 4: //turns change
                    if (type == 1)
                    { //more turns
                        Turns = DefaultTurns + 1;
                    }
                    else if (type == 2)
                    { //paralysis
                        Turns = 0;
                    }
                    else
                    { //no change
                        Turns = DefaultTurns;
                    }
                    break;
            }
        }
    }
}
