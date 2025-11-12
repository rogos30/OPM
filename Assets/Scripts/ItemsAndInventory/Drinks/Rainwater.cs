using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainwater : Drink
{
    public Rainwater()
    {
        this.Name = "Deszczówka";
        this.Description = "Odnawia 60 SP";
        this.Resurrects = false;
        this.Cost = 5;
        this.Amount = 0;
        SkillRestored = 60;
        Id = 1;
    }
}
