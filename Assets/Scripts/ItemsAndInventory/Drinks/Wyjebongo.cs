using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wyjebongo : Drink
{
    public Wyjebongo()
    {
        this.Name = "Wyjebongo";
        this.Description = "Odnawia 67% SP";
        this.Resurrects = false;
        this.Cost = 28;
        this.Amount = 0;
        SkillRestored = 0.67f;
    }
}
