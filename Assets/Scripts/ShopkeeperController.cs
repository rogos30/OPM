using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperController : Interactable
{
    public override void Interact()
    {
        if (GameManager.instance.inGameCanvas.enabled)
        {
            ShopManager.instance.SetUpShop();
        }
    }
}
