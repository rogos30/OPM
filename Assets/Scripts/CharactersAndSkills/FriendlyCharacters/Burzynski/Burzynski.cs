using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burzynski : FriendlyCharacter
{
    //specialty - has 6 skills instead of 5 and unlocks them every 3 levels instead of 5

    public Burzynski() : base()
    {
        NominativeName = "Burzyñski";
        DativeName = "Burzyñskiemu";
        AccusativeName = "Burzyñskiego";
        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 450;
        Skill = MaxSkill = 100;
        Attack = DefaultAttack = BaseAttack = 100;
        Defense = DefaultDefense = BaseDefense = 30;
        Accuracy = DefaultAccuracy = BaseAccuracy = 0.8f;
        Turns = DefaultTurns = 1;
        Speed = 200;
        SpriteIndex = 4;
        levelsToUnlockSkill = 3;
        AbilityDescription = "Burzyñski ma ³¹cznie 7 umiejêtnoœci i odblokowuje je szybciej";
        CharacterDescription = "Opis Burzyñskiego wip";
        Attack attack = new Attack();
        skillSet.Add(attack);
        Balaclava balaclava = new Balaclava();
        skillSet.Add(balaclava);
        SwordMayhem swordMayhem = new SwordMayhem();
        skillSet.Add(swordMayhem);
        SwordTwirl swordTwirl = new SwordTwirl();
        skillSet.Add(swordTwirl);
        KorwinsPistol korwinsPistol = new KorwinsPistol();
        skillSet.Add(korwinsPistol);
        MoraleDebuff moraleDebuff = new MoraleDebuff();
        skillSet.Add(moraleDebuff);
        Dinology dinology = new Dinology();
        skillSet.Add(dinology);
    }
}
