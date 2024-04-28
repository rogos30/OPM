using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BigMilk : IceCream
{
    public BigMilk()
    {
        this.Name = "BigMilk";
        this.Description = "Usuwa negatywne efekty i broni przed nimi przez 2 tury";
        this.Resurrects = false;
        this.Cost = 25;
        this.Amount = 0;
        EffectImmunity = 2;
    }
}
