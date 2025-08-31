using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrosswords : EnemySkill
{
    //* Krzy��wki - rozwi�zuje krzy��wki. Zwi�ksza sobie celno�� oraz mo�e u�pi� (sparali�owa�) dowolnego wroga.

    public EnemyCrosswords()
    {
        Name = "Krzy��wki";
        InFightDescription = " rozwi�zuje krzy��wki, zwi�kszaj�c sobie celno�� ";
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.NominativeName + " nie przyci�ga uwagi " + target.AccusativeName + "krzy��wkami (nic dziwnego)";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int turns = 3;
        if (Random.Range(0, 1f) < criticalChance)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc + " i zmniejszaj�c j� " + target.DativeName + " ";
            target.ApplyDebuff((int)Character.StatusEffects.ACCURACY, turns);
        }
        source.ApplyBuff((int)Character.StatusEffects.ACCURACY, turns);
        finalDesc = finalDesc + "na " + (turns - 1) + " tury";
        return finalDesc;
    }
}
