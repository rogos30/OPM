using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseChickenToast : Sandwich
{
    public CheeseChickenToast()
    {
        Name = "Tost ser-kurczak";
        Description = "Przywraca 67% HP";
        Resurrects = false;
        Cost = 15;
        Amount = 0;
        HealthRestored = 0.67f;
        Id = 8;
    }
}
