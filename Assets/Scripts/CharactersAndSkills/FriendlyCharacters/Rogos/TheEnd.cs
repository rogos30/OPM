using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnd : PlayableSkill
{
    public TheEnd() : base()
    {
        Name = "Fina³";
        SkillDescription = "robi, co trzeba";
        InFightDescription = " wyprowadza koñcz¹cy cios i zadaje ";
        Cost = 1;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
        SkillSoundId = 26;
        AccuracyMultiplier = 10;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " nie wykañcza " + target.AccusativeName;
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        int damage = Random.Range(2445723, 89078942);
        finalDesc = finalDesc + " " + damage + " obra¿eñ!";
        target.TakeDamage(damage);
        return finalDesc;
    }
}
