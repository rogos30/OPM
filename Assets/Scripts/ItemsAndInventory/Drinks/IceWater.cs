using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IceWater : Drink
{
    public IceWater()
    {
        this.Name = "Woda z lodem";
        this.Description = "Odnawia 50% SP";
        this.Resurrects = false;
        this.Cost = 21;
        this.Amount = 0;
        SkillRestored = 0.5f;
        Id = 9;
    }
}
