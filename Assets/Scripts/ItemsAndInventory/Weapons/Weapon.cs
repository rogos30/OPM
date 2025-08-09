using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Wearable
{
    override public void PutOn(FriendlyCharacter target)
    {
        target.wearablesWorn[(int)Wearable.WEARABLES.WEAPON] = this;
        Amount--;
        if (Amount < 0)
        {
            Debug.LogError("problem!");
        }
        target.DefaultAttack += AttackAdded;
    }

    public override void TakeOff(FriendlyCharacter target)
    {
        target.wearablesWorn[(int)Wearable.WEARABLES.WEAPON] = null;
        Amount++;
        target.DefaultAttack -= AttackAdded;
    }
}
