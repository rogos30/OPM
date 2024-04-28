using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monitoring : EnemyCharacter
{
    //character = new EnemyCharacter("Monitoring", 30000, 5000, 500, 50, 60, 90, 1, 450, 2500, 1, skills);

    public Monitoring() : base()
    {
        NominativeName = "Monitoring";
        DativeName = "Monitoringowi";
        AccusativeName = "Monitoring";
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
        skillSet.Add(attack);
        Surveillance surveillance = new Surveillance();
        skillSet.Add(surveillance);
        ToxicGas toxicGas = new ToxicGas();
        skillSet.Add(toxicGas);
    }
}
