using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.AudioSettings;

public class Lora : FriendlyCharacter
{

    public Lora() : base()
    {
        NominativeName = "Lora";
        DativeName = "Lorze";
        AccusativeName = "Lory";
        Health = MaxHealth = DefaultMaxHealth = 350;
        Skill = MaxSkill = 80;
        Attack = DefaultAttack = 80;
        Defense = DefaultDefense = 30;
        Turns = DefaultTurns = 2;
        Speed = 400;
        AbilityDescription = "Lora jest chwilowo hiperpobudzona przez kawê i ma 2 ruchy w ka¿dej turze";
        Attack attack = new Attack();
        skillSet.Add(attack);
    }
}
