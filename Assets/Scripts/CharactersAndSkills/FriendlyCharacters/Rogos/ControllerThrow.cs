using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerThrow : PlayableSkill
{
    public ControllerThrow()
    {
        Name = "Rzut padem";
        SkillDescription = "rzuca padem jak bumerangiem. Zadaje spore obra¿enia 2 razy.";
        InFightDescription = " rzuca padem jak bumerangiem i zadaje ";
        Cost = 80;
        Repetitions = 2;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = true;
    }
    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " nie trafia padem w " + target.AccusativeName;
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int damage = (int)(source.Attack * 1.5f * Random.Range(0.8f, 1.2f)) - target.Defense;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            damage *= source.criticalDamageMultiplier;
        }
        damage = Mathf.Max(damage, 1);
        finalDesc = finalDesc + " " + damage + " obra¿eñ!";
        target.TakeDamage(damage);
        return finalDesc;
    }
}
