using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class IndianKid : EnemyCharacter
{
    // Start is called before the first frame update
    public IndianKid()
    {
        //character = new EnemyCharacter("Hinduski dzieciak", 150, 30, 60, 20, 15, 0.9f, 1, 350, 25, 150, skills);

        NominativeName = "Hinduski dzieciak";
        DativeName = "Hinduskiemu dzieciakowi";
        AccusativeName = "Hinduskiego dzieciaka";
        Health = MaxHealth = DefaultMaxHealth = 150;
        DifficultyHealthChange = 35;
        Attack = DefaultAttack = 90;
        DifficultyAttackChange = 30;
        Defense = DefaultDefense = 15;
        Accuracy = DefaultAccuracy = 0.9f;
        Turns = DefaultTurns = 1;
        Speed = 350;
        MoneyDropped = 25;
        XPDropped = 150;
        EnemyAttack attack = new EnemyAttack();
        skillSet.Add(attack);
        skillSet.Add(attack);
        skillSet.Add(attack);
        HeavyPunch heavyPunch = new HeavyPunch();
        skillSet.Add(heavyPunch);
    }
}
