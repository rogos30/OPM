using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Magnum : IceCream
{
    public Magnum()
    {
        this.Name = "Magnum";
        this.Description = "Usuwa negatywne efekty i broni przed nimi przez 1 turê";
        this.Resurrects = false;
        this.Cost = 15;
        this.Amount = 0;
        EffectImmunity = 1;
    }
}
