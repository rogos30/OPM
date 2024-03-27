using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public abstract class Skill
{
    public string Name { get; set; }
    public string InFightDescription { get; set; }
    public int Repetitions { get; set; }
    public float AccuracyMultiplier { get; set; }
    public bool TargetIsFriendly { get; set; }
    public bool TargetIsSelf {  get; set; }
    public bool MultipleTargets { get; set; }
    public bool[] PositiveStatusEffects = new bool[5];
    public bool[] NegativeStatusEffects = new bool[5];

    public Skill()
    {
        Repetitions = 1;
        AccuracyMultiplier = 1;
    }
    /*public Skill(string name, string skillDescription, string inFightDescription, float cost, int repetitions, float attackMultiplier,
        float accuracyMultiplier, float healing, bool targetIsFriendly, bool targetIsSelf, bool targetIsRandom, bool multipleTargets, int[] statusChanges)
    {
        Name = name;
        SkillDescription = skillDescription;
        InFightDescription = inFightDescription;
        Cost = cost;
        Repetitions = repetitions;
        AttackMultiplier = attackMultiplier;
        Healing = healing;
        AccuracyMultiplier = accuracyMultiplier;
        TargetIsFriendly = targetIsFriendly;
        TargetIsSelf = targetIsSelf;
        TargetIsRandom = targetIsRandom;
        MultipleTargets = multipleTargets;

        for (int i = 0; i < statusChanges.Length; i++)
        {
            PositiveStatusEffects[i] = false;
            NegativeStatusEffects[i] = false;
            if (statusChanges[i] > 0)
            {
                PositiveStatusEffects[i] = true;
            }
            if (statusChanges[i] < 0)
            {
                NegativeStatusEffects[i] = true;
            }
        }
    }*/

    //public abstract string execute(Character source, Character target, int skillPerformance);
    /*{
        if (Cost > 1 || Cost == 0)
        {
            source.Skill -= (int)Cost;
        }
        else
        {
            source.Skill = (int)(source.Skill - source.MaxSkill * Cost);
        }
        float hit = Random.Range(0, 1);
        float wearablesAccuracyModifier = 1;
        foreach (var wearable in source.wearablesWorn)
        {
            if (wearable == null) continue;
            wearablesAccuracyModifier *= wearable.AccuracyMultiplier;
        }
        if ((skillPerformance == -1 && hit < source.Accuracy * AccuracyMultiplier * wearablesAccuracyModifier) || skillPerformance > 0) // hit
        {
            string finalDesc = source.Name + " ";
            int changeInHealth = 0, addedSourceAttack = 0, addedTargetDefense = 0;
            float crit = Random.value, multiplier = 1;
            if ((skillPerformance == -1 && crit < criticalChance) || skillPerformance == 2) // critical
            {
                multiplier = 2;
                finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            }
            finalDesc += InFightDescription;
            if (TargetIsFriendly || TargetIsSelf)
            {
                for (int i = 0; i < PositiveStatusEffects.Length; i++)
                {
                    if (PositiveStatusEffects[i])
                    {
                        if (target.NegativeStatusTimers[i] > 0)
                        {
                            target.NegativeStatusTimers[i] = 0;
                        }
                        else
                        {
                            target.PositiveStatusTimers[i] = 2 * (int)multiplier + 1;
                        }
                        if (TargetIsFriendly && TargetIsSelf)
                        {
                            if (target.NegativeStatusTimers[i] > 0)
                            {
                                target.NegativeStatusTimers[i] = 0;
                            }
                            else
                            {
                                target.PositiveStatusTimers[i] = 2 * (int)multiplier + 1;
                            }
                        }
                    }
                }

                if (target.IsGuarding)
                { //an ally that is guarding gets increased healing
                    multiplier *= 1.5f;
                }
                float wearablesHealingModifier = 1;
                foreach (var wearable in target.wearablesWorn)
                {
                    if (wearable == null) continue;
                    wearablesHealingModifier *= wearable.HealingMultiplier;
                }
                if (Healing > 1)
                {
                    changeInHealth = (int)(Healing * multiplier * wearablesHealingModifier);
                    target.Health = Mathf.Min(target.Health + changeInHealth, target.MaxHealth);
                }
                else if (Healing > 0)
                {
                    changeInHealth = (int)(Healing * multiplier * wearablesHealingModifier * target.MaxHealth);
                    target.Health = Mathf.Min(target.Health + changeInHealth, target.MaxHealth);
                }
                if (TargetIsFriendly && TargetIsSelf)
                {
                    multiplier = 1;
                    if (source.IsGuarding)
                    { //an ally that is guarding gets increased healing
                        multiplier *= 1.5f;
                    }
                    wearablesHealingModifier = 1;
                    foreach (var wearable in source.wearablesWorn)
                    {
                        if (wearable == null) continue;
                        wearablesHealingModifier *= wearable.HealingMultiplier;
                    }
                    if (Healing > 1)
                    {
                        changeInHealth = (int)(Healing * multiplier * wearablesHealingModifier);
                        source.Health = Mathf.Min(target.Health + changeInHealth, target.MaxHealth);
                    }
                    else if (Healing > 0)
                    {
                        changeInHealth = (int)(Healing * multiplier * wearablesHealingModifier * target.MaxHealth);
                        source.Health = Mathf.Min(target.Health + changeInHealth, target.MaxHealth);
                    }
                }
            }
            else
            {
                for (int i = 0; i < NegativeStatusEffects.Length; i++)
                {
                    if (NegativeStatusEffects[i])
                    {
                        if (target.PositiveStatusTimers[i] > 0)
                        {
                            target.PositiveStatusTimers[i] = 0;
                        }
                        else
                        {
                            target.NegativeStatusTimers[i] = 2 * (int)multiplier + 1;
                        }
                    }
                    if (target.NegativeEffectsImmunity > 0)
                    {
                        target.NegativeStatusTimers[i] = 0;
                        finalDesc += ", ale " + target.Name + " ma odpornoœæ na efekty!";
                    }
                }
                if (target.IsGuarding)
                { //an enemy that is guarding takes heavily reduced damage
                    multiplier *= 0.5f;
                }
                for (int i = 0; i < 4; i++)
                {
                    if (source.wearablesWorn[i] != null)
                    {
                        addedSourceAttack += source.wearablesWorn[i].AttackAdded;
                    }
                    if (target.wearablesWorn[i] != null)
                    {
                        addedTargetDefense += target.wearablesWorn[i].DefenseAdded;
                    }
                }
                changeInHealth = (int)(Random.Range(0.8f, 1.2f) * (source.Attack + addedSourceAttack - target.Defense - addedTargetDefense) * AttackMultiplier);
                changeInHealth = (int)(changeInHealth * multiplier);
                if (multiplier > 0)
                {
                    changeInHealth = Mathf.Max(changeInHealth, 1);
                }
                target.Health = Mathf.Max(target.Health - changeInHealth, 0);
                if (target.Health == 0)
                {
                    target.Death();
                }
            }
            if (changeInHealth > 0)
            {
                if (TargetIsFriendly)
                {
                    finalDesc += " i leczy " + target.Name + "a za " + changeInHealth + " HP!";
                }
                else
                {
                    finalDesc += " i zadaje " + target.Name + "owi " + changeInHealth + " obra¿eñ!";
                }
            }
            else
            {
                finalDesc += target.Name + "a";
            }
            return finalDesc;
        }
        else //miss
        {
            return source.Name + " nie trafia umiejêtnoœci¹ " + this.Name + "!";
        }
    }*/

}
