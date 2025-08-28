using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosswords : PlayableSkill
{
    //* Krzy��wki - rozwi�zuje krzy��wki. Zwi�ksza sobie celno�� oraz mo�e u�pi� (sparali�owa�) dowolnego wroga.

    public Crosswords()
    {
        Name = "Krzy��wki";
        SkillDescription = "rozwi�zuje krzy��wki. Zwi�ksza sobie celno�� oraz mo�e u�pi� (sparali�owa�) dowolnego wroga.";
        InFightDescription = " rozwi�zuje krzy��wki, zwi�kszaj�c sobie celno�� ";
        Cost = 0.4f;
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
        TargetIsRandom = false;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.NominativeName + " nie przyci�ga uwagi " + target.AccusativeName + "krzy��wkami (nic dziwnego)";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc + " i zmniejszaj�c j� " + target.DativeName;
            target.ApplyDebuff((int)Character.StatusEffects.ACCURACY, turns);
        }
        source.ApplyBuff((int)Character.StatusEffects.ACCURACY, turns);
        finalDesc = finalDesc + "na " + (turns-1) + " tury";
        return finalDesc;
    }
}
