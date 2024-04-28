using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DefensiveAttribute : Wearable
{
    override public void PutOn(FriendlyCharacter target)
    {
        target.wearablesWorn[(int)Wearable.WEARABLES.DEFENSIVE] = this;
        Amount--;
        target.DefaultAccuracy *= AccuracyMultiplier;
        target.DefaultDefense += DefenseAdded;
    }

    public override void TakeOff(FriendlyCharacter target)
    {
        target.wearablesWorn[(int)Wearable.WEARABLES.DEFENSIVE] = this;
        Amount++;
        target.DefaultAccuracy /= AccuracyMultiplier;
        target.DefaultDefense -= DefenseAdded;
    }
}
