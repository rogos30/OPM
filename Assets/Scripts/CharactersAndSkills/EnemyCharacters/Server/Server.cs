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
        Health = MaxHealth = DefaultMaxHealth = 12000;
        DifficultyHealthChange = 6000;
        Attack = DefaultAttack = 600;
        DifficultyAttackChange = 250;
        Defense = DefaultDefense = 60;
        Accuracy = DefaultAccuracy = 0.90f;
        Turns = DefaultTurns = 1;
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
