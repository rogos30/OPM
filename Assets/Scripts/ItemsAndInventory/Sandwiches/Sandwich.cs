using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sandwich : Item
{
    public float HealthRestored { get; set; }
    

    public override string Use(FriendlyCharacter source, FriendlyCharacter target)
    {
        source.OnItemUsed(this);
        float multiplier = source.ItemEnhancementMultiplier;
        foreach (var wearable in target.wearablesWorn)
        {
            if (wearable == null) continue;
            multiplier *= wearable.HealingMultiplier;
        }
        if (target.IsGuarding)
        {
            multiplier *= 1.5f;
        }
        string finalDesc = target.NominativeName + " odzyskuje ";
        if (HealthRestored <= 1)
        {
            target.Heal(HealthRestored * multiplier);
            finalDesc = finalDesc + HealthRestored * multiplier * 100 + "% HP";
        }
        else
        {
            target.Heal((int)(HealthRestored * multiplier));
            finalDesc = finalDesc + HealthRestored * multiplier + " HP";
        }
        Amount--;
        return finalDesc;
    }
}
