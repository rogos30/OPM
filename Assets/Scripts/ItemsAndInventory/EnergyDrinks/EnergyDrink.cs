using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : Item
{
    public float HealthRestored { get; set; }

    public override string Use(FriendlyCharacter source, FriendlyCharacter target)
    {
        float multiplier = source.ItemEnhancementMultiplier;
        string finalDesc = target.NominativeName + " wraca do ¿ywych z " + HealthRestored * multiplier * 100 + "% HP";
        target.KnockedOut = false;
        target.Heal(HealthRestored * multiplier);
        Amount--;
        return finalDesc;
    }
}
