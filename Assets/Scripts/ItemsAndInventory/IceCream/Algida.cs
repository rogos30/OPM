using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Algida : IceCream
{
    public Algida()
    {
        this.Name = "Algida";
        this.Description = "Usuwa negatywne efekty";
        this.Resurrects = false;
        this.Cost = 5;
        this.Amount = 0;
        EffectImmunity = 0;
    }
}
