using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public static StoryManager instance;
    [SerializeField] TMP_Text currentQuestText;
    [SerializeField] GameObject[] NPCs;
    [SerializeField] GameObject[] Marlboros;
    [NonSerialized] public int currentMainQuest = 0;
    [SerializeField] string[] questDescriptions;
    int marlborosCollected;
    void Awake()
    {
        instance = this;
        currentQuestText.text = questDescriptions[currentMainQuest];
        HandleNPCs(); //initializing all npcs
        marlborosCollected = 0;
    }

    void Update()
    {
        
    }

    public void CollectMarlboro()
    {
        marlborosCollected++;
        if (marlborosCollected == Marlboros.Length)
        {
            ProgressStory();
        }
    }

    void EnableMarlboros()
    {
        Debug.Log("enabling marlboros");
        foreach (var marlboro in Marlboros)
        {
            marlboro.SetActive(true);
        }
    }

    void DisableMarlboros()
    {
        Debug.Log("disabling marlboros");
        foreach (var marlboro in Marlboros)
        {
            marlboro.SetActive(false);
        }
    }

    public void DisableNPCs()
    {
        foreach(var npc in NPCs)
        {
            npc.SetActive(false);
        }
    }

    public void HandleNPCs()
    {
        foreach (var npc in NPCs)
        {
            if (currentMainQuest >= npc.GetComponent<Interactable>().appearanceAtQuest && currentMainQuest < npc.GetComponent<Interactable>().disappearanceAtQuest)
            {
                npc.SetActive(true);
            }
            else
            {
                npc.SetActive(false);
            }
        }
    }



    public void ProgressStory()
    {
        Debug.Log("progressing story");
        currentQuestText.text = questDescriptions[++currentMainQuest];
        //FriendlyCharacter character;
        switch (currentMainQuest)
        {
            case 2:
                //character = new Welenc();
                BattleManager.instance.currentPartyCharacters.Add(1);
                break;
            case 3:
                //character = new Stasiak();
                BattleManager.instance.currentPartyCharacters.Add(2);
                break;
            case 4:
                //character = new Kaja();
                BattleManager.instance.currentPartyCharacters.Add(3);
                break;
            case 5:
                EnableMarlboros();
                break;
            case 6:
                DisableMarlboros();
                break;
            case 11:
                //character = new Brudzynski();
                BattleManager.instance.currentPartyCharacters.Add(4);
                break;
        }

        HandleNPCs();
    }
}
