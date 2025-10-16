using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public static StoryManager instance;
    [SerializeField] TMP_Text currentQuestText;
    [SerializeField] GameObject[] storyNPCs;
    [SerializeField] GameObject[] sideQuestNPCs;
    [SerializeField] GameObject[] followerNPCs;
    [SerializeField] GameObject[] storyTriggers;
    [SerializeField] GameObject[] sideQuestTriggers;
    [SerializeField] public GameObject[] PatrolNPCs;
    [SerializeField] GameObject[] Marlboros;
    int marlborosCollected;
    [NonSerialized] public int currentMainQuest = 0;
    [SerializeField] string[] questDescriptions;

    [NonSerialized] public int currentPingPongScamsQuest = 0;
    [SerializeField] string[] pingPongScamsQuestDescriptions;

    [NonSerialized] public int currentFollowingRefereesQuest = 0;
    [SerializeField] string[] followingRefereesQuestDescriptions;

    [NonSerialized] public int currentDiversionAndSearchQuest = 0;
    [SerializeField] string[] diversionAndSearchQuestDescriptions;

    [SerializeField] GameObject[] PonyStickers;
    int ponyStickersCollected;
    [NonSerialized] public int currentStrongStuffQuest = 0;
    [SerializeField] string[] strongStuffQuestDescriptions;

    [NonSerialized] public int currentFaceTheCheaterQuest = 0;
    [SerializeField] string[] faceTheCheaterQuestDescriptions;

    int[] sideQuestLengths = new int[5];
    bool jaronaldMentioned = false;

    [SerializeField] AudioClip[] additionalVoiceLines;
    void Awake()
    {
        sideQuestLengths[0] = pingPongScamsQuestDescriptions.Length;
        sideQuestLengths[1] = followingRefereesQuestDescriptions.Length;
        sideQuestLengths[2] = diversionAndSearchQuestDescriptions.Length;
        sideQuestLengths[3] = strongStuffQuestDescriptions.Length;
        sideQuestLengths[4] = faceTheCheaterQuestDescriptions.Length;
        instance = this;
        currentQuestText.text = questDescriptions[currentMainQuest];
        HandleAllNPCs(); //initializing all npcs
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
    public void HandleAllNPCs()
    {
        HandleStoryNPCs();
        HandleFollowerNPCs();
        HandleSideQuestNPCs();
    }

    public void DisableAllNPCs()
    {
        DisableStoryNPCs();
        DisableSideQuestNPCs();
        DisableFollowerNPCs();
    }

    public void HandleStoryNPCs()
    {
        foreach (var npc in storyNPCs)
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
        foreach (var trigger in storyTriggers)
        {
            if (currentMainQuest >= trigger.GetComponent<Interactable>().appearanceAtQuest && currentMainQuest < trigger.GetComponent<Interactable>().disappearanceAtQuest)
            {
                trigger.SetActive(true);
            }
            else
            {
                trigger.SetActive(false);
            }
        }
    }

    public void DisableStoryNPCs()
    {
        foreach(var npc in storyNPCs)
        {
            npc.SetActive(false);
        }
        foreach(var trigger in storyTriggers)
        {
            trigger.SetActive(false);
        }
    }

    public void HandleFollowerNPCs()
    {
        foreach (var npc in followerNPCs)
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

    public void DisableSideQuestNPCs()
    {
        foreach(var npc in sideQuestNPCs)
        {
            npc.SetActive(false);
        }
        foreach (var trigger in sideQuestTriggers)
        {
            trigger.SetActive(false);
        }
    }

    public void DisableFollowerNPCs()
    {
        foreach (var npc in followerNPCs)
        {
            npc.SetActive(false);
        }
    }

    bool IsSideQuestCompleted(int sideQuest)
    {
        switch (sideQuest)
        {
            case 0:
                return currentPingPongScamsQuest >= pingPongScamsQuestDescriptions.Length;
            case 1:
                return currentFollowingRefereesQuest >= followingRefereesQuestDescriptions.Length;
            case 2:
                return currentDiversionAndSearchQuest >= diversionAndSearchQuestDescriptions.Length;
            case 3:
                return currentStrongStuffQuest >= strongStuffQuestDescriptions.Length;
            case 4:
                return currentFaceTheCheaterQuest >= faceTheCheaterQuestDescriptions.Length;
        }
        return false;
    }

    public void HandleSideQuestNPCs(int sideQuestId = -1)
    {
        foreach (var npc in sideQuestNPCs)
        {
            var sqnpc = npc.GetComponent<SidequestNPCController>();
            if (sqnpc.appearsBasedOnMainStoryProgress)
            {
                if (currentMainQuest >= sqnpc.appearanceAtQuest && currentMainQuest < sqnpc.disappearanceAtQuest && sqnpc.GetProgressInSidequest() == 0
                    && (!sqnpc.appearsBasedOnOtherSideStoryProgress || sqnpc.appearsBasedOnOtherSideStoryProgress && IsSideQuestCompleted(sqnpc.otherSideQuestRequiredId))
                    && (sqnpc.sideQuestId == sideQuestId || sideQuestId == -1))
                {
                    npc.SetActive(true);
                }
                else
                {
                    npc.SetActive(false);
                }
            }
            else
            {
                switch (sqnpc.sideQuestId) 
                {
                    case 0:
                        if (currentPingPongScamsQuest >= sqnpc.appearanceAtQuest && currentPingPongScamsQuest < sqnpc.disappearanceAtQuest)
                        {
                            npc.SetActive(true);
                        }
                        else
                        {
                            npc.SetActive(false);
                        }
                        break;
                    case 1:
                        if (currentFollowingRefereesQuest >= sqnpc.appearanceAtQuest && currentFollowingRefereesQuest < sqnpc.disappearanceAtQuest)
                        {
                            npc.SetActive(true);
                        }
                        else
                        {
                            npc.SetActive(false);
                        }
                        break;
                    case 2:
                        if (currentDiversionAndSearchQuest >= sqnpc.appearanceAtQuest && currentDiversionAndSearchQuest < sqnpc.disappearanceAtQuest)
                        {
                            npc.SetActive(true);
                        }
                        else
                        {
                            npc.SetActive(false);
                        }
                        break;
                    case 3:
                        if (currentStrongStuffQuest >= sqnpc.appearanceAtQuest && currentStrongStuffQuest < sqnpc.disappearanceAtQuest)
                        {
                            npc.SetActive(true);
                        }
                        else
                        {
                            npc.SetActive(false);
                        }
                        break;
                    case 4:
                        if (currentFaceTheCheaterQuest >= sqnpc.appearanceAtQuest && currentFaceTheCheaterQuest < sqnpc.disappearanceAtQuest)
                        {
                            npc.SetActive(true);
                        }
                        else
                        {
                            npc.SetActive(false);
                        }
                        break;
                }
            }
        }
        foreach (var trigger in sideQuestTriggers)
        {
            var sqtrigger = trigger.GetComponent<SidequestTriggerController>();
            switch (sqtrigger.sideQuestId)
            {
                case 0:
                    if (currentPingPongScamsQuest >= sqtrigger.appearanceAtQuest && currentPingPongScamsQuest < sqtrigger.disappearanceAtQuest)
                    {
                        trigger.SetActive(true);
                    }
                    else
                    {
                        trigger.SetActive(false);
                    }
                    break;
                case 1:
                    if (currentFollowingRefereesQuest >= sqtrigger.appearanceAtQuest && currentFollowingRefereesQuest < sqtrigger.disappearanceAtQuest)
                    {
                        trigger.SetActive(true);
                    }
                    else
                    {
                        trigger.SetActive(false);
                    }
                    break;
                case 2:
                    if (currentDiversionAndSearchQuest >= sqtrigger.appearanceAtQuest && currentDiversionAndSearchQuest < sqtrigger.disappearanceAtQuest)
                    {
                        trigger.SetActive(true);
                    }
                    else
                    {
                        trigger.SetActive(false);
                    }
                    break;
                case 3:
                    if (currentStrongStuffQuest >= sqtrigger.appearanceAtQuest && currentStrongStuffQuest < sqtrigger.disappearanceAtQuest)
                    {
                        trigger.SetActive(true);
                    }
                    else
                    {
                        trigger.SetActive(false);
                    }
                    break;
                case 4:
                    if (currentFaceTheCheaterQuest >= sqtrigger.appearanceAtQuest && currentFaceTheCheaterQuest < sqtrigger.disappearanceAtQuest)
                    {
                        trigger.SetActive(true);
                    }
                    else
                    {
                        trigger.SetActive(false);
                    }
                    break;
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
                //dodanie Welenca
                BattleManager.instance.currentPartyCharacters.Add(1);
                break;
            case 3:
                //dodanie Stasiaka;
                BattleManager.instance.currentPartyCharacters.Add(2);
                break;
            case 4:
                //dodanie Kai
                BattleManager.instance.currentPartyCharacters.Add(3);
                break;
            case 5:
                EnableMarlboros();
                break;
            case 6:
                DisableMarlboros();
                break;
            case 11:
                //dodanie Brudzyñskiego
                BattleManager.instance.currentPartyCharacters.Add(4);
                break;
            case 14:
                //przejœcie do Lory
                BattleManager.instance.currentPartyCharacters.RemoveAll(x => x >= 0);
                BattleManager.instance.currentPartyCharacters.Add(6); //Janek
                BattleManager.instance.currentPartyCharacters.Add(5); //Lora

                for (int i = 0; i < BattleManager.instance.currentPartyCharacters.Count; i++)
                {
                    int index = BattleManager.instance.currentPartyCharacters[i];
                    while (BattleManager.instance.playableCharacters[index].Level < 12)
                    {
                        BattleManager.instance.playableCharacters[index].HandleLevel(1000);
                    }
                }

                break;
            case 15:
                //powrot do g³ównej ekipy
                BattleManager.instance.currentPartyCharacters.RemoveAll(x => x >= 0);
                BattleManager.instance.currentPartyCharacters.Add(0); //Rogos
                BattleManager.instance.currentPartyCharacters.Add(1); //Welenc
                BattleManager.instance.currentPartyCharacters.Add(2); //Stasiak
                BattleManager.instance.currentPartyCharacters.Add(3); //Kaja
                break;
        }

        HandleAllNPCs();
    }

    void FinishSideQuest()
    {
        currentQuestText.text = questDescriptions[currentMainQuest];
        HandleAllNPCs();
    }

    public void ProgressSideQuest(int sideQuest)
    {
        DisableStoryNPCs();
        switch (sideQuest)
        {
            case 0:
                ProgressPingPongScams();
                break;
            case 1:
                ProgressFollowingReferees();
                break;
            case 2:
                ProgressDiversionAndSearch();
                break;
            case 3:
                ProgressStrongStuff();
                break;
            case 4:
                ProgressFaceTheCheater();
                break;
        }
    }

    void ProgressPingPongScams()
    {
        if (currentPingPongScamsQuest + 1 >= pingPongScamsQuestDescriptions.Length)
        {
            if (jaronaldMentioned)
            {
                string[] lines = {
                    "Czekajcie! Jaronald... Gdzieœ s³ysza³em to imiê.",
                    "To nie on by³ zaznaczony na liœcie zawodników turnieju?",
                    "Cholera.",
                    "Trzeba daæ znaæ Matiemu.",
                    "Napisa³em do niego, ¿eby na niego uwa¿a³."
            };
                int[] speakerIndexes = { 1, 0, 2, 3, 0 };
                DialogManager.instance.StartDialogue(lines, speakerIndexes, additionalVoiceLines);
            }
            else
            {
                jaronaldMentioned = true;
            }
            currentPingPongScamsQuest++;
            FinishSideQuest();
        }
        else
        {
            currentQuestText.text = pingPongScamsQuestDescriptions[++currentPingPongScamsQuest];
            HandleSideQuestNPCs(0);
        }
    }

    void ProgressFollowingReferees()
    {
        if (currentFollowingRefereesQuest + 1 >= followingRefereesQuestDescriptions.Length)
        {
            currentFollowingRefereesQuest++;
            FinishSideQuest();
        }
        else
        {
            currentQuestText.text = followingRefereesQuestDescriptions[++currentFollowingRefereesQuest];
            switch (currentFollowingRefereesQuest)
            {
                case 1:
                    DisableFollowerNPCs();
                    break;
            }
            HandleSideQuestNPCs(1);
        }
    }

    void ProgressDiversionAndSearch()
    {
        if (currentDiversionAndSearchQuest + 1 >= diversionAndSearchQuestDescriptions.Length)
        {
            if (jaronaldMentioned)
            {
                string[] lines = {
                    "Czekajcie! Jaronald... Gdzieœ s³ysza³em to imiê.",
                    "To nie o nim mówili dresiarze?",
                    "Cholera.",
                    "Trzeba daæ znaæ Matiemu.",
                    "Napisa³em do niego, ¿eby na niego uwa¿a³."
            };
                int[] speakerIndexes = { 1, 0, 2, 3, 0 };
                DialogManager.instance.StartDialogue(lines, speakerIndexes, additionalVoiceLines);
            }
            else
            {
                jaronaldMentioned = true;
            }
            currentDiversionAndSearchQuest++;
            FinishSideQuest();
        }
        else
        {
            currentQuestText.text = diversionAndSearchQuestDescriptions[++currentDiversionAndSearchQuest];
            HandleSideQuestNPCs(2);
        }
    }

    void ProgressStrongStuff()
    {
        if (currentStrongStuffQuest + 1 >= strongStuffQuestDescriptions.Length)
        {
            if (jaronaldMentioned)
            {
                string[] lines = {
                    "Czekajcie! Jaronald... Gdzieœ s³ysza³em to imiê.",
                    "To nie on by³ zaznaczony na liœcie zawodników turnieju?",
                    "Cholera.",
                    "Trzeba daæ znaæ Matiemu.",
                    "Napisa³em do niego, ¿eby na niego uwa¿a³."
            };
                int[] speakerIndexes = { 1, 0, 2, 3, 0 };
                DialogManager.instance.StartDialogue(lines, speakerIndexes, additionalVoiceLines);
            }
            else
            {
                jaronaldMentioned = true;
            }
            currentStrongStuffQuest++;
            FinishSideQuest();
        }
        else
        {
            currentQuestText.text = strongStuffQuestDescriptions[++currentStrongStuffQuest];
            HandleSideQuestNPCs(3);
        }
    }

    void ProgressFaceTheCheater()
    {
        if (currentFaceTheCheaterQuest + 1 >= faceTheCheaterQuestDescriptions.Length)
        {
            currentFaceTheCheaterQuest++;
            FinishSideQuest();
        }
        else
        {
            currentQuestText.text = faceTheCheaterQuestDescriptions[++currentFaceTheCheaterQuest];
            HandleSideQuestNPCs(4);
        }
    }
}
