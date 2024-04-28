using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Armor : Wearable
{
    override public void PutOn(FriendlyCharacter target)
    {
        target.wearablesWorn[(int)Wearable.WEARABLES.ARMOR] = this;
        Amount--;
        target.DefaultDefense += DefenseAdded;
    }

    public override void TakeOff(FriendlyCharacter target)
    {
        target.wearablesWorn[(int)Wearable.WEARABLES.ARMOR] = null;
        Amount++;
        target.DefaultDefense -= DefenseAdded;
    }
}
