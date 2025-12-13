using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public int money;
    public List<Item> items = new List<Item>();
    public List<Wearable> wearables = new List<Wearable>();
    public readonly int maxOwnedItems = 99;

    public Sprite[] itemsImages;
    public Sprite[] wearablesImages;

    private void Awake()
    {
        money = 50;
        InitializeItems();
        InitializeWearables();
        instance = this;
    }

    public void InitializeItems()
    {
        Item item = new CheeseToast(); items.Add(item);
        item = new Rainwater(); items.Add(item);
        item = new Tiger(); items.Add(item);
        item = new BigMilk(); items.Add(item);

        item = new CheeseHamToast(); items.Add(item);
        item = new TapWater(); items.Add(item);
        item = new Redbull(); items.Add(item);
        item = new Magnum(); items.Add(item);

        item = new CheeseChickenToast(); items.Add(item);
        item = new IceWater(); items.Add(item);
        item = new Monster(); items.Add(item);
        item = new Koktajlowe(); items.Add(item);

        item = new FatToast(); items.Add(item);
        item = new IcebergWater(); items.Add(item);
        item = new Dzik(); items.Add(item);
        item = new Marletto(); items.Add(item);
    }
    public void InitializeWearables()
    {
        Wearable item = new WoodenSword(); wearables.Add(item);
        item = new LeatherArmor(); wearables.Add(item);
        item = new SchoolHoodie(); wearables.Add(item);
        item = new FatumBox(); wearables.Add(item);

        item = new StoneSword(); wearables.Add(item);
        item = new ChainmailArmor(); wearables.Add(item);
        item = new Necklace(); wearables.Add(item);
        item = new FriendMarkedNotebook(); wearables.Add(item);

        item = new IronSword(); wearables.Add(item);
        item = new IronArmor(); wearables.Add(item);
        item = new KwasnysChair(); wearables.Add(item);
        item = new TennisTable(); wearables.Add(item);

        item = new DiamondSword(); wearables.Add(item);
        item = new DiamondArmor(); wearables.Add(item);
        item = new CarycasSpeech(); wearables.Add(item);
        item = new FailedTest(); wearables.Add(item);

        item = new NetheriteSword(); wearables.Add(item);
        item = new NetheriteArmor(); wearables.Add(item);
        item = new SuperToast(); wearables.Add(item);
        item = new GoldMedal(); wearables.Add(item);
    }
}
