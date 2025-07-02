using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : Item
{
    public float SkillRestored { get; set; }


    public override string Use(FriendlyCharacter source, FriendlyCharacter target)
    {
        source.OnItemUsed(this);
        float multiplier = source.ItemEnhancementMultiplier;
        string finalDesc = target.NominativeName + " odzyskuje ";
        if (SkillRestored <= 1)
        {
            target.RestoreSkill(SkillRestored * multiplier);
            finalDesc = finalDesc + SkillRestored * multiplier * 100 + "% SP";
        }
        else
        {
            target.RestoreSkill((int)(SkillRestored * multiplier));
            finalDesc = finalDesc + SkillRestored * multiplier + " SP";
        }
        Amount--;
        return finalDesc;
    }
}
