using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

public class ZahirTrip : PlayableSkill
{
    bool giveaway = false;
    public ZahirTrip()
    {
        SetToMission();
    }

    public void SetToMission()
    {
        giveaway = false;
        Name = "Podr� po kebaby";
        SkillDescription = "jedzie rowerem po kebaby. Nie b�dzie go kilka tur, ale kebaby pomog� wszystkim";
        InFightDescription = " wybywa na misj� na ";
        Cost = 0.67f;
        TargetIsFriendly = false;
        TargetIsSelf = true;
        MultipleTargets = false;
        TargetIsRandom = false;
        SkillSoundId = 23;

    }

    public void SetToGiveaway()
    {
        giveaway = true;
        Name = "Rozdanie kebab�w";
        SkillDescription = "rozdaje dru�ynie zdobyte kebaby. Leczy i buffuje wszystkich";
        InFightDescription = " m�wi \"bierzcie i jedzcie\" ";
        Cost = 0.67f;
        TargetIsFriendly = true;
        TargetIsSelf = false;
        MultipleTargets = true;
        TargetIsRandom = false;
    }

    public override string execute(FriendlyCharacter source, Character target, int skillPerformance)
    {
        if (!giveaway)
        {
            if (skillPerformance == 0)
            {
                return source.NominativeName + " spada z rowerka";
            }
            string finalDesc = source.NominativeName + InFightDescription;
            int turns = 2;
            if (skillPerformance == 2)
            {
                finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
                turns = 1;
            }
            finalDesc = finalDesc + " " + turns + " tur!";
            target.ApplyDebuff((int)Character.StatusEffects.TURNS, turns);
            target.ApplyDebuff((int)Character.StatusEffects.TURNS, turns);
            SetToGiveaway();
            return finalDesc;
        }
        
        else
        {
            if (skillPerformance == 0)
            {
                SetToMission();
                return source.DativeName + " rozsypa�y si� kebaby! Ju� nikt ich nie zje :(";
            }
            string finalDesc = source.NominativeName + InFightDescription;
            int turns = 2;
            int healing = 750;
            if (skillPerformance == 2)
            {
                finalDesc = "KRYTYCZNE TRAFIENIE! " + finalDesc;
                turns = 3;
                healing = 1250;
            }
            finalDesc = finalDesc + " " + turns + " tur!";
            for (int i = 0; i < 5; i++)
            {
                target.ApplyBuff(i, turns);
            }
            target.Heal(healing);
            return finalDesc;
        }
    }
}
