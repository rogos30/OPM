using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server : EnemyCharacter
{
    public Server() : base()
    {
        NominativeName = "Serwer";
        DativeName = "Serwerowi";
        AccusativeName = "Serwer";
        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 12000;
        DifficultyHealthChange = 6000;
        Attack = DefaultAttack = BaseAttack = 450;
        DifficultyAttackChange = 200;
        Defense = DefaultDefense = BaseDefense = 60;
        Accuracy = DefaultAccuracy = BaseAccuracy = 0.90f;
        Turns = DefaultTurns = BaseTurns = 1;
        Speed = 450;
        MoneyDropped = 250;
        XPDropped = 7500;
        TriplePunch attack = new TriplePunch();
        skillSet.Add(attack);
        HeavyPunch heavyPunch = new HeavyPunch();
        skillSet.Add(heavyPunch);
        GroupShock groupShock = new GroupShock();
        skillSet.Add(groupShock);
        ToxicGas toxicGas = new ToxicGas();
        skillSet.Add(toxicGas);
    }
}
