using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overrun : PlayableSkill
{

    public Overrun()
    {
        Name = "Rozjazd";
        SkillDescription = "wzywa swojego konia i rozje¿d¿a nim wszystkich wrogów";
        InFightDescription = "traktuje wrogów jako tor jeŸdziecki i ";
        Cost = 50;
        AccuracyMultiplier = 1.2f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = true;
        TargetIsRandom = false;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " spada z konia.";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int damage = (int)(source.Attack * 1.2f) - target.Defense;
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
