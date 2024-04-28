using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseHamToast : Sandwich
{
    public CheeseHamToast()
    {
        Name = "Tost ser-szynka";
        Description = "Przywraca 500hp";
        Resurrects = false;
        Cost = 8;
        Amount = 0;
        HealthRestored = 500;
    }
}
