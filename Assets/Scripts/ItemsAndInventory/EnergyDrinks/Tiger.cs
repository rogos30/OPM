using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger : EnergyDrink
{
    public Tiger()
    {
        this.Name = "Tiger";
        this.Description = "Stawia powalonych na nogi z 25% HP";
        this.Resurrects = true;
        this.Cost = 15;
        this.Amount = 0;
        HealthRestored = 0.25f;
        Id = 2;
    }
}
