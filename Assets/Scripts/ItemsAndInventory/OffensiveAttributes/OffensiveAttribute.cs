using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OffensiveAttribute : Wearable
{
    override public void PutOn(FriendlyCharacter target)
    {
        target.wearablesWorn[(int)Wearable.WEARABLES.OFFENSIVE] = this;
        Amount--;
        target.HealingMultiplier *= HealingMultiplier;
        target.DefaultAttack += AttackAdded;
    }

    public override void TakeOff(FriendlyCharacter target)
    {
        target.wearablesWorn[(int)Wearable.WEARABLES.OFFENSIVE] = null;
        Amount++;
        target.HealingMultiplier /= HealingMultiplier;
        target.DefaultAttack -= AttackAdded;
    }
}
