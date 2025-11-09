using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stock : Drink
{
    public Stock()
    {
        this.Name = "Woda mineralna";
        this.Description = "Odnawia 50% SP";
        this.Resurrects = false;
        this.Cost = 21;
        this.Amount = 0;
        SkillRestored = 0.5f;
    }
}
