using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aphrodite : PlayableSkill
{
    // Start is called before the first frame update
    public Aphrodite()
    {
        Name = "Afrodyta";
        SkillDescription = "przebiera siê za babê, zniechêcaj¹c przeciwników do mocnych ciosów";
        InFightDescription = " zmniejsza atak ";
        Cost = 0.33f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = true;
        TargetIsRandom = false;
        AnimationId = 4;
        SkillSoundId = 0;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " rozrywa swoje przebranie i czar pryska";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.AccusativeName;
        int turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 4;
        }
        finalDesc = finalDesc + " na " + (turns-1) + " tury!";
        target.ApplyDebuff((int)Character.StatusEffects.ATTACK, turns);
        return finalDesc;
    }
}
