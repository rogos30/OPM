using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bocian : Drink
{
    public Bocian()
    {
        this.Name = "Bocian";
        this.Description = "Odnawia 150 SP";
        this.Resurrects = false;
        this.Cost = 13;
        this.Amount = 0;
        SkillRestored = 150;
    }
}
