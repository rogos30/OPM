using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zubrowka : Drink
{
    public Zubrowka()
    {
        this.Name = "¯ubrówka";
        this.Description = "Odnawia 60 SP";
        this.Resurrects = false;
        this.Cost = 5;
        this.Amount = 0;
        SkillRestored = 60;
    }
}
