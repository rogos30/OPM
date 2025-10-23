using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera1 : EnemyCharacter
{
    //character = new EnemyCharacter("Kamera 1", 15000, 2500, 400, 40, 70, 85, 1, 350, 0, 1, skills);

    public Camera1() : base()
    {
        NominativeName = "Kamera";
        DativeName = "Kamerze";
        AccusativeName = "Kamerê";
        Health = MaxHealth = DefaultMaxHealth = 8000;
        DifficultyHealthChange = 4000;
        Attack = DefaultAttack = 400;
        DifficultyAttackChange = 200;
        Defense = DefaultDefense = 70;
        Accuracy = DefaultAccuracy = 0.85f;
        Turns = DefaultTurns = 1;
        Speed = 350;
        MoneyDropped = 100;
        XPDropped = 5000;
        DoublePunch attack = new DoublePunch();
        skillSet.Add(attack);
        skillSet.Add(attack);
        Observation observation = new Observation();
        skillSet.Add(observation);
    }
}
