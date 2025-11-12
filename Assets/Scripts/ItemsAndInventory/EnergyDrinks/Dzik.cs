using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dzik : EnergyDrink
{
    public Dzik()
    {
        this.Name = "Dzik";
        this.Description = "Stawia powalonych na nogi z 67% HP";
        this.Resurrects = true;
        this.Cost = 75;
        this.Amount = 0;
        HealthRestored = 0.67f;
        Id = 14;
    }
}
