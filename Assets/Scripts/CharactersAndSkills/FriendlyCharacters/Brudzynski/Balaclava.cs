using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balaclava : PlayableSkill
{
    //skill = new Skill("Kominiarka", "zak³ada kominiarkê, zwiêkszaj¹c sobie obronê", "zak³ada kominiarkê", 35, 1, 0, 1, 0, false, true, false, false, statusEffects);

    public Balaclava()
    {
        Name = "Kominiarka";
        SkillDescription = "zak³ada kominiarkê, zwiêkszaj¹c sobie obronê.";
        InFightDescription = " zak³ada kominiarkê i zwiêksza sobie obronê na ";
        Cost = 35;
        TargetIsFriendly = false;
        TargetIsSelf = true;
        MultipleTargets = false;
        TargetIsRandom = false;
        SkillSoundId = 7;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (skillPerformance == 0)
        {
            return source.DativeName + " nie udaje siê za³o¿yæ kominiarki.";
        }
        string finalDesc = source.NominativeName + InFightDescription;
        int turns = 3;
        if (skillPerformance == 2)
        {
            finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
            turns = 5;
        }
        finalDesc = finalDesc + turns + " tur!";
        target.ApplyBuff(1, turns);
        return finalDesc;
    }
}
