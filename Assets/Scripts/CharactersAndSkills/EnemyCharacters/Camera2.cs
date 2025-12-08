using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2 : EnemyCharacter
{
    //character = new EnemyCharacter("Kamera 2", 13000, 2000, 500, 50, 65, 100, 1, 340, 0, 1, skills);


    public Camera2() : base()
    {
        NominativeName = "Kamera 2";
        DativeName = "Kamerze 2";
        AccusativeName = "Kamerê 2";
        Health = MaxHealth = DefaultMaxHealth = 13000;
        DifficultyHealthChange = 2000;
        Attack = DefaultAttack = 500;
        DifficultyAttackChange = 50;
        Defense = DefaultDefense = 65;
        Accuracy = DefaultAccuracy = 1f;
        Turns = DefaultTurns = 1;
        Speed = 340;
        MoneyDropped = 0;
        XPDropped = 0;
        DoublePunch attack = new DoublePunch();
        skillSet.Add(attack);
        skillSet.Add(attack);
        TriplePunch triplePunch = new TriplePunch();
        skillSet.Add(triplePunch);
        Connection connection = new Connection();
        skillSet.Add(connection);
    }
}
