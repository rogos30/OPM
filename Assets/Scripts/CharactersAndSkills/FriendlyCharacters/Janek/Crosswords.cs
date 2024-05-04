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
            return source.DativeName + " nie zaciekawia " + target.AccusativeName + " (nic dziwnego)";
        }
        string finalDesc = source.NominativeName + InFightDescription + target.DativeName;
        float defenseDebuff = Random.Range(0, 1f);
        int turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 5;
            source.ApplyBuff((int)Character.StatusEffects.ACCURACY, turns);
            defenseDebuff *= source.criticalDamageMultiplier;
        }
        finalDesc = finalDesc + "na " + turns + " tur";
        if (defenseDebuff > 0.75f)
        {
            target.ApplyDebuff((int)Character.StatusEffects.TURNS, 3);
            finalDesc = finalDesc + " i usypiaj�c rywala na 3 tury!";
        }
        return finalDesc;
    }
}
