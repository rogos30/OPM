using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IcebergWater : Drink
{
    public IcebergWater()
    {
        this.Name = "Woda z lodowca";
        this.Description = "Odnawia 67% SP";
        this.Resurrects = false;
        this.Cost = 28;
        this.Amount = 0;
        SkillRestored = 0.67f;
        Id = 13;
    }
}
