using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrosswords : EnemySkill
{
    //* Krzy¿ówki - rozwi¹zuje krzy¿ówki. Zwiêksza sobie celnoœæ oraz mo¿e uœpiæ (sparali¿owaæ) dowolnego wroga.

    public EnemyCrosswords()
    {
        Name = "Krzy¿ówki";
        InFightDescription = " rozwi¹zuje krzy¿ówki, zwiêkszaj¹c sobie celnoœæ ";
        TargetIsFriendly = false;
        TargetIsSelf = false;
        MultipleTargets = false;
    }

    public override string execute(EnemyCharacter source, Character target)
    {
        if (Random.Range(0, 1f) > source.Accuracy * AccuracyMultiplier)
        {
            return source.NominativeName + " nie przyci¹ga uwagi " + target.AccusativeName + "krzy¿ówkami (nic dziwnego)";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int turns = 3;
        if (Random.Range(0, 1f) < criticalChance)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc + " i zmniejszaj¹c j¹ " + target.DativeName + " ";
            target.ApplyDebuff((int)Character.StatusEffects.ACCURACY, turns);
        }
        source.ApplyBuff((int)Character.StatusEffects.ACCURACY, turns);
        finalDesc = finalDesc + "na " + (turns - 1) + " tury";
        return finalDesc;
    }
}
