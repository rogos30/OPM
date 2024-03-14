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

    readonly string[,] itemNames = new string[4, 4]
    {
        {"Tost z serem", "Cisowianka 0.5L", "Redbull", "Algida" },
        {"Tost ser-szynka", "Muszynianka 0.5L", "Monster", "Magnum"},
        {"Tost ser-kurczak", "¯ywiec zdrój 0.5L", "Dzik", "Big Milk" },
        {"Grubasotost", "0.5L", "Jagerbomba", "Marletto" }
    };
    readonly string[] itemDescriptions = new string[4] { "Leczy ", "Odnawia ", "Stawia powalonych na nogi z ", "Usuwa negatywne efekty" };
    readonly float[,] itemHealth = new float[4, 4]
    {
        {250, 0, 0.16f, 0},
        {500, 0, 0.33f, 0 },
        {0.67f, 0, 0.5f, 0 },
        { 1, 0, 0.67f, 0}
    };
    readonly float[,] itemSkill = new float[4, 4]
    {
        {0, 60, 0, 0},
        {0, 150, 0, 0 },
        {0, 0.5f, 0, 0 },
        {0, 0.8f, 0, 0 }
    };
    readonly int[,] itemCosts = new int[4, 4]
    {
        {3, 5, 15, 5 },
        {8, 13, 35, 15 },
        {15, 21, 50, 25 },
        {23, 28, 75, 35 }
    };
    readonly int[] iceCreamEffectImmunity = new int[4] { 0, 1, 2, 3 };

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
        Item item;
        for (int i = 0; i < 4; i++)
        {
            item = new Item(itemNames[i, 0], itemDescriptions[0], itemHealth[i, 0], itemSkill[i, 0], false, -1, itemCosts[i, 0]);
            items.Add(item);
            item = new Item(itemNames[i, 1], itemDescriptions[1], itemHealth[i, 1], itemSkill[i, 1], false, -1, itemCosts[i, 1]);
            items.Add(item);
            item = new Item(itemNames[i, 2], itemDescriptions[2], itemHealth[i, 2], itemSkill[i, 2], true, -1, itemCosts[i, 2]);
            items.Add(item);
            item = new Item(itemNames[i, 3], itemDescriptions[3], itemHealth[i, 3], itemSkill[i, 3], false, iceCreamEffectImmunity[i], itemCosts[i, 3]);
            items.Add(item);
        }
        
    }
    public void InitializeWearables()
    {
        Wearable item;

        for (int i = 0; i < 4; i++)
        {
            item = new Wearable(wearablesNames[i,0], "Dodaje", wearablesAttack[i,0], wearablesDefense[i, 0], 1, 1, wearablesCosts[i,0]);
            wearables.Add(item);
            item = new Wearable(wearablesNames[i, 1], "Dodaje", wearablesAttack[i, 1], wearablesDefense[i, 1], 1, 1, wearablesCosts[i, 1]);
            wearables.Add(item);
            item = new Wearable(wearablesNames[i, 2], "Dodaje", wearablesAttack[i, 2], wearablesDefense[i, 2], attributesHealingMultiplier[i], 1, wearablesCosts[i, 2]);
            wearables.Add(item);
            item = new Wearable(wearablesNames[i, 3], "Dodaje", wearablesAttack[i, 3], wearablesDefense[i, 3], 1, jeweleryAccuracyMultiplier[i], wearablesCosts[i, 3]);
            wearables.Add(item);
        }
    }
}
