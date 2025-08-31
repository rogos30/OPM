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
        Health = MaxHealth = DefaultMaxHealth = 30000;
        DifficultyHealthChange = 5000;
        Attack = DefaultAttack = 500;
        DifficultyAttackChange = 50;
        Defense = DefaultDefense = 60;
        Accuracy = DefaultAccuracy = 0.90f;
        Turns = DefaultTurns = 1;
        Speed = 450;
        MoneyDropped = 2500;
        XPDropped = 500;
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
