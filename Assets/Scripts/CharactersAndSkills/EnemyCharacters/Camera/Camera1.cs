using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera1 : EnemyCharacter
{
    public Camera1() : base()
    {
        NominativeName = "Kamera";
        DativeName = "Kamerze";
        AccusativeName = "Kamerê";
        Health = MaxHealth = DefaultMaxHealth = BaseMaxHealth = 8000;
        DifficultyHealthChange = 4000;
        Attack = DefaultAttack = BaseAttack = 300;
        DifficultyAttackChange = 150;
        Defense = DefaultDefense = BaseDefense = 70;
        Accuracy = DefaultAccuracy = BaseAccuracy = 0.85f;
        Turns = DefaultTurns = BaseTurns = 1;
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
