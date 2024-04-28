using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseToast : Sandwich
{
    public CheeseToast()
    {
        Name = "Tost z serem";
        Description = "Przywraca 250hp";
        Resurrects = false;
        Cost = 3;
        Amount = 0;
        HealthRestored = 250;
    }
}
