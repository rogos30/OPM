using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class OffendedKid : EnemyCharacter
{
    // Start is called before the first frame update
    public OffendedKid()
    {
        //character = new EnemyCharacter("Dzieciak z fochem", 200, 20, 55, 15, 20, 0.85f, 1, 250, 15, 250, skills);

        NominativeName = "Dzieciak z fochem";
        DativeName = "Dzieciakowi z fochem";
        AccusativeName = "Dzieciaka z fochem";
        Health = MaxHealth = DefaultMaxHealth = 200;
        DifficultyHealthChange = 20;
        Attack = DefaultAttack = 55;
        DifficultyAttackChange = 15;
        Defense = DefaultDefense = 20;
        Accuracy = DefaultAccuracy = 0.85f;
        Turns = DefaultTurns = 1;
        Speed = 250;
        MoneyDropped = 15;
        XPDropped = 250;
        EnemyAttack attack = new EnemyAttack();
        skillSet.Add(attack);
    }
}
