using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BigMilk : IceCream
{
    public BigMilk()
    {
        this.Name = "BigMilk";
        this.Description = "Usuwa negatywne efekty";
        this.Resurrects = false;
        this.Cost = 5;
        this.Amount = 0;
        EffectImmunity = 0;
        Id = 3;
    }
}
