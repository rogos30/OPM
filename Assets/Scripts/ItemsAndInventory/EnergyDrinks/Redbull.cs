using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Redbull : EnergyDrink
{
    public Redbull()
    {
        this.Name = "Red Bull";
        this.Description = "Stawia powalonych na nogi z 37.5% HP";
        this.Resurrects = true;
        this.Cost = 35;
        this.Amount = 0;
        HealthRestored = 0.375f;
        Id = 6;
    }
}
