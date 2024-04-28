using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatToast : Sandwich
{
    public FatToast()
    {
        Name = "Grubasotost";
        Description = "Przywraca 100% hp";
        Resurrects = false;
        Cost = 23;
        Amount = 0;
        HealthRestored = 1;
    }
}
