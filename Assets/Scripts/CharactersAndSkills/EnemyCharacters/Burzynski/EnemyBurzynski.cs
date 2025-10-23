using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBurzynski : EnemyCharacter
{
    public EnemyBurzynski() : base()
    {
        NominativeName = "Burzy�ski";
        DativeName = "Burzy�skiemu";
        AccusativeName = "Burzy�skiego";
        Health = MaxHealth = DefaultMaxHealth = 40000;
        DifficultyHealthChange = 40000;
        Attack = DefaultAttack = 450;
        DifficultyAttackChange = 225;
        Defense = DefaultDefense = 100;
        Accuracy = DefaultAccuracy = 0.9f;
        Turns = DefaultTurns = 1;
        Speed = 200;
        MoneyDropped = 500;
        XPDropped = 15000;
        EnemyAttack attack = new EnemyAttack();
        skillSet.Add(attack);
        EnemyBalaclava balaclava = new EnemyBalaclava();
        skillSet.Add(balaclava);
        EnemySwordMayhem swordMayhem = new EnemySwordMayhem();
        skillSet.Add(swordMayhem);
        EnemySwordTwirl swordTwirl = new EnemySwordTwirl();
        skillSet.Add(swordTwirl);
        EnemyKorwinsPistol korwinsPistol = new EnemyKorwinsPistol();
        skillSet.Add(korwinsPistol);
        EnemyShitMonologue enemyShitMonologue = new EnemyShitMonologue();
        skillSet.Add(enemyShitMonologue);
        EnemyDinology dinology = new EnemyDinology();
        skillSet.Add(dinology);
    }
}
