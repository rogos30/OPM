using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseHamToast : Sandwich
{
    public CheeseHamToast()
    {
        Name = "Tost ser-szynka";
        Description = "Przywraca 500 HP";
        Resurrects = false;
        Cost = 8;
        Amount = 0;
        HealthRestored = 500;
        Id = 4;
    }
}
