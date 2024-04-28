using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : EnergyDrink
{
    public Monster()
    {
        this.Name = "Monster";
        this.Description = "Stawia powalonych na nogi z 50% HP";
        this.Resurrects = true;
        this.Cost = 55;
        this.Amount = 0;
        HealthRestored = 0.5f;
    }
}
