using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private static Inventory instance;
    public static Inventory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Inventory();
            }
            return instance;
        }
    }

    public int Money { get; set; } = 50;
    public List<Item> items;
    public List<Wearable> wearables;
    public readonly int maxOwnedItems = 99;

    readonly string[,] wearablesNames = new string[4, 4]
    {
        {"Drewniany miecz", "Skórzana zbroja", "Attr1", "Bi¿u1" },
        {"Kamienny miecz", "Kolczuga", "Attr2", "Bi¿u2"},
        {"¯elazny miecz", "¯elazna zbroja", "Attr3", "Bi¿u3" },
        {"Diamentowy miecz", "Diamentowa zbroja", "Attr4", "Bi¿u4" }
    };
    readonly int[,] wearablesAttack = new int[4, 4]
    {
        {50, 0, 30, 0},
        {100, 0, 60, 0 },
        {200, 0, 120, 0 },
        {400, 0, 240, 0 }
    };
    readonly int[,] wearablesDefense = new int[4, 4]
    {
        {0, 20, 0, 12},
        {0, 40, 0, 25 },
        {0, 80, 0, 50 },
        {0, 160, 0, 100}
    };
    readonly float[] attributesHealingMultiplier = new float[4] {0.9f, 0.8f, 0.7f, 0.6f };
    readonly float[] jeweleryAccuracyMultiplier = new float[4] { 0.9f, 0.8f, 0.7f, 0.6f };
    readonly int[,] wearablesCosts = new int[4, 4]
    {
        {20, 20, 10, 10 },
        {50, 50, 20, 20 },
        {110, 110, 35, 35 },
        {170, 170, 60, 60 }
    };
    public Inventory()
    {
        items = new List<Item>();
        wearables = new List<Wearable>();
    }

    public int AvailableItems()
    {
        int result = 0;
        for (int i=0; i<items.Count; i++)
        {
            if (items[i].Amount > 0)
            {
                result++;
            }
        }
        return result;
    }

    public void InitializeItems()
    {
        Item item = new CheeseToast(); items.Add(item);
        item = new Zubrowka(); items.Add(item);
        item = new Tiger(); items.Add(item);
        item = new Algida(); items.Add(item);
        item = new CheeseHamToast(); items.Add(item);
        item = new Bocian(); items.Add(item);
        item = new Redbull(); items.Add(item);
        item = new Magnum(); items.Add(item);
        item = new CheeseChickenToast(); items.Add(item);
        item = new Stock(); items.Add(item);
        item = new Monster(); items.Add(item);
        item = new BigMilk(); items.Add(item);
        item = new FatToast(); items.Add(item);
        item = new Wyjebongo(); items.Add(item);
        item = new Dzik(); items.Add(item);
        item = new Marletto(); items.Add(item);
    }
    public void InitializeWearables()
    {
        Wearable item = new WoodenSword(); wearables.Add(item);
        item = new LeatherArmor(); wearables.Add(item);
        item = new MalachHoodie(); wearables.Add(item);
        item = new FatumBox(); wearables.Add(item);
        item = new StoneSword(); wearables.Add(item);
        item = new ChainmailArmor(); wearables.Add(item);
        item = new Necklace(); wearables.Add(item);
        item = new CeilingPart(); wearables.Add(item);
        item = new IronSword(); wearables.Add(item);
        item = new IronArmor(); wearables.Add(item);
        item = new OstrysChair(); wearables.Add(item);
        item = new TennisTable(); wearables.Add(item);
        item = new DiamondSword(); wearables.Add(item);
        item = new DiamondArmor(); wearables.Add(item);
        item = new BienkowskasRemains(); wearables.Add(item);
        item = new DivorcePapers(); wearables.Add(item);
    }
}
