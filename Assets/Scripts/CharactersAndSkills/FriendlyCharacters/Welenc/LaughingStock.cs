using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaughingStock : PlayableSkill
{
    // Start is called before the first frame update
    public LaughingStock()
    {
        Name = "Po�miewisko";
        SkillDescription = "�mieszkuje i przedrze�nia przeciwnik�w, zmniejszaj�c ich obron�";
        InFightDescription = " zmniejsza obron� ";
        Cost = 0.33f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = true;
        TargetIsRandom = false;
        AnimationId = 4;
        SkillSoundId = 10;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        int turns;
        if (skillPerformance == 0)
        {
            turns = 3;
            source.ApplyDebuff((int)Character.StatusEffects.DEFENSE, turns);
            return source.NominativeName + " robi po�miewisko z siebie, zmniejszaj�c swoj� obron� na " + (turns-1) + "tury";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.AccusativeName;
        turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 4;
        }
        finalDesc = finalDesc + " na " + (turns - 1) + " tury!";
        target.ApplyDebuff((int)Character.StatusEffects.DEFENSE, turns);
        return finalDesc;
    }
}
