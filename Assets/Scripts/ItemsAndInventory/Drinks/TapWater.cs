using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapWater : Drink
{
    public TapWater()
    {
        this.Name = "Kranówka";
        this.Description = "Odnawia 150 SP";
        this.Resurrects = false;
        this.Cost = 13;
        this.Amount = 0;
        SkillRestored = 150;
        Id = 5;
    }
}
