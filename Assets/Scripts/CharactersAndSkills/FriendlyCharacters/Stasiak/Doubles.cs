using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doubles : PlayableSkill
{
    //skill = new Skill("Debel", "gra z kompanem debla. Przywraca sobie i kompanowi 1000hp.", "gra z kompanem debla", 0.4f, 1, 0, 1, 1000, true, true, false, false, statusEffects);

    public Doubles()
    {
        Name = "Debel";
        SkillDescription = "gra z kompanem debla. Przywraca sobie i kompanowi 600hp.";
        InFightDescription = " gra z kompanem debla, lecz¹c jemu i sobie ";
        Cost = 0.4f;
        TargetIsFriendly = true;
        TargetIsSelf = true;
        MultipleTargets = false;
        TargetIsRandom = false;
        SkillSoundId = 16;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " jednak woli graæ w pojedynkê";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int healing = 600;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            healing *= source.criticalDamageMultiplier;
        }
        finalDesc = finalDesc + healing + " HP!";
        source.Heal(healing);
        target.Heal(healing);
        return finalDesc;
    }
}
