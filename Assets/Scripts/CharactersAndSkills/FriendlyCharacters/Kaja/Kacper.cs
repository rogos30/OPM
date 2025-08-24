using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kacper : PlayableSkill
{

    public Kacper()
    {
        Name = "Kacper";
        SkillDescription = "jest ponownie przy�miewana przez swojego brata, kt�ry losowo rozdaje sojusznikom SP.";
        InFightDescription = " patrzy jak jej brat odnawia ";
        Cost = 0;
        Repetitions = 10;
        TargetIsFriendly = true;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = true;
        SkillSoundId = 9;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return "Kacper ma jednak wa�niejsze rzeczy na g�owie";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        float sp = 0.1f;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            sp = 0.15f;
        }
        finalDesc = finalDesc + " " + sp * 100 + "% SP";
        ((FriendlyCharacter)target).RestoreSkill(sp);
        return finalDesc;
    }
}
