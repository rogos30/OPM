using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosswords : PlayableSkill
{
    public Crosswords()
    {
        Name = "Krzy¿ówki";
        SkillDescription = "rozwi¹zuje krzy¿ówki. Zwiêksza sobie celnoœæ oraz mo¿e zmniejszyæ j¹ wrogowi.";
        InFightDescription = " rozwi¹zuje krzy¿ówki, zwiêkszaj¹c sobie celnoœæ ";
        Cost = 0.4f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
        IsUnlocked = true;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " nie przyci¹ga uwagi " + target.AccusativeName + "krzy¿ówkami (nic dziwnego)";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc + " i zmniejszaj¹c j¹ " + target.DativeName;
            target.ApplyDebuff((int)Character.StatusEffects.ACCURACY, turns);
        }
        source.ApplyBuff((int)Character.StatusEffects.ACCURACY, turns);
        finalDesc = finalDesc + "na " + (turns-1) + " tury";
        return finalDesc;
    }
    public override void upgrade()
    {
        return;
    }
}
