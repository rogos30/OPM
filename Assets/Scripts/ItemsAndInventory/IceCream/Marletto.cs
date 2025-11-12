using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Marletto : IceCream
{
    public Marletto()
    {
        this.Name = "Marletto";
        this.Description = "Usuwa negatywne efekty i broni przed nimi przez 3 tury";
        this.Resurrects = false;
        this.Cost = 35;
        this.Amount = 0;
        EffectImmunity = 3;
        Id = 15;
    }
}
