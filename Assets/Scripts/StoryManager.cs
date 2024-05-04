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
    [NonSerialized] public int currentQuest = 0;
    readonly string[] questDescriptions = { "Porozmawiaj z Chrobotem", "Zbierz Marlboraski", "Oddaj Chrobotowi Marlboraski", "Zwerbuj Œwietlika", "Pobij Welenca dla zasady", "KONIEC GRY!!!1!" };
    int marlborosCollected;
    void Awake()
    {
        instance = this;
        currentQuestText.text = questDescriptions[currentQuest];
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

    void HandleNPCs()
    {
        foreach (var npc in NPCs)
        {
            if (currentQuest >= npc.GetComponent<Interactable>().appearanceAtQuest && currentQuest < npc.GetComponent<Interactable>().disappearanceAtQuest)
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
        currentQuestText.text = questDescriptions[++currentQuest];
        Skill[] skillTable = BattleManager.instance.skillTable;
        FriendlyCharacter character;
        switch (currentQuest)
        {
            case 1:
                Debug.Log("almost enabling marlboros");
                EnableMarlboros();
                break;
            case 4:
                character = new Swietlik();
                BattleManager.instance.playableCharacters.Add(character);
                break;
            case 5:
                character = new Stasiak();
                BattleManager.instance.playableCharacters.Add(character);

                character = new Kaja();
                BattleManager.instance.playableCharacters.Add(character);

                character = new Brudzynski();
                BattleManager.instance.playableCharacters.Add(character);
                break;
        }

        HandleNPCs();
    }
}
