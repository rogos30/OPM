using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCream : Item
{
    public int EffectImmunity { get; set; }

    public override string Use(FriendlyCharacter source, FriendlyCharacter target)
    {
        source.OnItemUsed(this);
        string finalDesc = target.NominativeName + " pozbywa siê negatywnych efektów";
        for (int i = 0; i < target.StatusTimers.Length; i++)
        {
            if (target.StatusTimers[i] < 0)
            {
                target.StatusTimers[i] = 0;
            }
        }
        if (EffectImmunity >= 1)
        {
            finalDesc += " na " + EffectImmunity + " tur!";
        }
        target.NegativeEffectsImmunity = Mathf.Max(target.NegativeEffectsImmunity, EffectImmunity);
        Amount--;
        return finalDesc;
    }
}
