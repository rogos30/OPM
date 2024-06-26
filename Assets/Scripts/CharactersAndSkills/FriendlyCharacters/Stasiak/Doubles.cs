using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doubles : PlayableSkill
{
    //skill = new Skill("Debel", "gra z kompanem debla. Przywraca sobie i kompanowi 1000hp.", "gra z kompanem debla", 0.4f, 1, 0, 1, 1000, true, true, false, false, statusEffects);

    public Doubles()
    {
        Name = "Debel";
        SkillDescription = "gra z kompanem debla. Przywraca sobie i kompanowi 1000hp.";
        InFightDescription = " gra z kompanem debla, lecz�c jego i siebie za ";
        Cost = 0.4f;
        TargetIsFriendly = true;
        TargetIsSelf = true;
        MultipleTargets = false;
        TargetIsRandom = false;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " jednak woli gra� w pojedynk�";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int healing = 1000;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            healing *= source.criticalDamageMultiplier;
        }
        finalDesc = finalDesc + " " + healing + " HP!";
        source.Heal(healing);
        target.Heal(healing);
        return finalDesc;
    }
}
