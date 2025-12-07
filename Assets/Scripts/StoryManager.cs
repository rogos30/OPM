using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public static StoryManager instance;
    [SerializeField] Camera cam;
    [SerializeField] PlayerController player;
    [SerializeField] TMP_Text currentQuestText;
    [SerializeField] GameObject[] storyNPCs;
    [SerializeField] GameObject[] storyTeleports;
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

    public int[] sideQuestLengths = new int[5];
    bool jaronaldMentioned = false;

    public bool canReturnToMainStory = true;

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
        if (marlborosCollected++ == Marlboros.Length)
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
    public void CollectPonySticker()
    {
        ponyStickersCollected++;
        if (ponyStickersCollected == PonyStickers.Length)
        {
            string[] lines = {
                    "Dobra, to chyba wszystkie"
                    };
            int[] speakerIndexes = { 3 };
            DialogueManager.instance.onDialogueEnd.RemoveAllListeners();
            DialogueManager.instance.StartDialogue(lines, speakerIndexes, additionalVoiceLines);
            DialogueManager.instance.onDialogueEnd.AddListener(() => {
                ProgressSideQuest(3);
            });
        }
    }

    void EnablePonyStickers()
    {
        Debug.Log("enabling pony stickers");
        foreach (var sticker in PonyStickers)
        {
            sticker.SetActive(true);
        }
    }

    void DisablePonyStickers()
    {
        Debug.Log("disabling pony stickers");
        foreach (var sticker in PonyStickers)
        {
            sticker.SetActive(false);
        }
    }
    public void HandleAllNPCs()
    {
        HandleStoryNPCs();
        HandleStoryTeleports();
        HandleFollowerNPCs();
        HandleSideQuestNPCs();
    }

    public void DisableAllNPCs()
    {
        DisableStoryNPCs();
        DisableStoryTeleports();
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
        foreach (var npc in PatrolNPCs)
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

    public void HandleStoryTeleports()
    {
        foreach (var tp in storyTeleports)
        {
            if (currentMainQuest >= tp.GetComponent<Interactable>().appearanceAtQuest && currentMainQuest < tp.GetComponent<Interactable>().disappearanceAtQuest)
            {
                tp.SetActive(true);
            }
            else
            {
                tp.SetActive(false);
            }
        }
    }

    public void DisableStoryTeleports()
    {
        foreach (var tp in storyTeleports)
        {
            tp.SetActive(false);
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
            if (sqtrigger.appearsBasedOnMainStoryProgress)
            {
                if (currentMainQuest >= sqtrigger.appearanceAtQuest && currentMainQuest < sqtrigger.disappearanceAtQuest && sqtrigger.GetProgressInSidequest() == 0
                    && (!sqtrigger.appearsBasedOnOtherSideStoryProgress || sqtrigger.appearsBasedOnOtherSideStoryProgress && IsSideQuestCompleted(sqtrigger.otherSideQuestRequiredId))
                    && (sqtrigger.sideQuestId == sideQuestId || sideQuestId == -1))
                {
                    trigger.SetActive(true);
                }
                else
                {
                    trigger.SetActive(false);
                }
            }
            else
            {
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
    }

    public void DisableSideQuestNPCs()
    {
        foreach (var npc in sideQuestNPCs)
        {
            npc.SetActive(false);
        }
        foreach (var trigger in sideQuestTriggers)
        {
            trigger.SetActive(false);
        }
    }

    void AdjustFollowerNPCsDistance(bool keepClose)
    {
        float distance;
        if (keepClose)
        {
            distance = 0.4f;
        }
        else
        {
            distance = 1.2f;
        }
        foreach (var npc in followerNPCs)
        {
            npc.GetComponent<PlayerFollowerController>().distanceFromTarget = distance;
        }
    }

    public void ProgressStory(bool isPlayingGameNormally = true)
    {
        Debug.Log("progressing story");
        currentQuestText.text = questDescriptions[++currentMainQuest];
        //FriendlyCharacter character;
        switch (currentMainQuest)
        {
            case 2:
                //zdjecie kurtki
                player.ChangeAnimator(1);
                break;
            case 9:
                //dodanie Welenca
                BattleManager.instance.currentPartyCharacters.Add(1);
                GameManager.instance.currentFreeroamMusicStage = 1;
                if (isPlayingGameNormally) GameManager.instance.PlayFreeroamMusic();
                break;
            case 10:
                GameManager.instance.currentFreeroamMusicStage = 2;
                if (isPlayingGameNormally) GameManager.instance.PlayFreeroamMusic();
                break;
            case 19:
                //dodanie Stasiaka;
                BattleManager.instance.currentPartyCharacters.Add(2);
                player.AllowRandomEncounters();
                break;
            case 24:
                //dodanie Mai
                BattleManager.instance.currentPartyCharacters.Add(3);
                break;
            case 27:
                //skradanka elektrotechnik
                AdjustFollowerNPCsDistance(true);
                break;
            case 29:
                //koniec skradanki elektrotechnik
                AdjustFollowerNPCsDistance(false);
                break;
            case 30:
                EnableMarlboros();
                break;
            case 31:
                DisableMarlboros();
                break;
            case 34:
                //Maja zostaje pod lazienka
                BattleManager.instance.currentPartyCharacters.Remove(3);
                break;
            case 36:
                //Maja wraca do druzyny
                BattleManager.instance.currentPartyCharacters.Add(3);
                break;
            case 47:
                //dodanie Brudzyñskiego
                player.currentRandomEncounterStage = 1;
                BattleManager.instance.currentPartyCharacters.Add(4);
                if (isPlayingGameNormally)
                {
                    while (BattleManager.instance.playableCharacters[4].Level < 8)
                    {
                        BattleManager.instance.playableCharacters[4].HandleLevel(500);
                    }
                }
                break;
            case 51:
                GameManager.instance.currentFreeroamMusicStage = 3;
                if (isPlayingGameNormally) GameManager.instance.PlayFreeroamMusic();
                break;
            case 52:
                GameManager.instance.currentFreeroamMusicStage = 4;
                if (isPlayingGameNormally) GameManager.instance.PlayFreeroamMusic();
                break;
            case 60:
                //przejœcie do Lory
                player.PreventRandomEncounters();
                if (isPlayingGameNormally)
                {
                    StartCoroutine(GameManager.instance.FadeToBlack(0.7f));
                    StartCoroutine(TransitionToLora());
                }
                else
                {
                    player.ChangeAnimator(2);
                    player.transform.position = new Vector2(-207, 52);
                }
                BattleManager.instance.currentPartyCharacters.RemoveAll(x => x >= 0);
                BattleManager.instance.currentPartyCharacters.Add(6); //Janek
                BattleManager.instance.currentPartyCharacters.Add(5); //Lora
                break;
            case 69:
                //ucieczka przed caryca
                player.ChangeAnimator(1);
                BattleManager.instance.currentPartyCharacters.RemoveAll(x => x >= 0);
                BattleManager.instance.currentPartyCharacters.Add(0); //Rogos
                BattleManager.instance.currentPartyCharacters.Add(1); //Welenc
                BattleManager.instance.currentPartyCharacters.Add(2); //Stasiak
                BattleManager.instance.currentPartyCharacters.Add(3); //Kaja
                AdjustFollowerNPCsDistance(true);
                if (isPlayingGameNormally)
                {
                    GameManager.instance.currentFreeroamMusicStage = 5;
                    GameManager.instance.PlayFreeroamMusic();
                    cam.orthographicSize = 5f;
                }
                break;
            case 73:
                //koniec ucieczki
                if (isPlayingGameNormally)
                {
                    PlayerPrefs.SetInt("cutsceneId", 1);
                    SceneManager.LoadScene("cutscenes");
                }
                break;
            case 74:
                //poczatek outside'u
                player.transform.position = new Vector2(-414, 185);
                cam.orthographicSize = 5f;
                AdjustFollowerNPCsDistance(false);
                GameManager.instance.currentFreeroamMusicStage = 6;
                if (isPlayingGameNormally) GameManager.instance.PlayFreeroamMusic();
                GameManager.instance.currentLocationText.text = "Za szko³¹";
                break;
            case 81:
                player.moveSpeed = 8;
                break;
            case 82:
                player.moveSpeed = 3.5f;
                break;
            case 83:
                player.moveSpeed = 8;
                break;
            case 84:
                player.moveSpeed = 3.5f;
                break;
            case 87:
                player.moveSpeed = 8;
                break;
            case 88:
                player.moveSpeed = 3.5f;
                break;
            case 90:
                player.moveSpeed = 8;
                break;
            case 91:
                //wejscie do podziemi
                player.currentRandomEncounterStage = 2;
                GameManager.instance.currentFreeroamMusicStage = 7;
                GameManager.instance.currentLocationText.text = "Podziemia szko³y";
                cam.orthographicSize = 3.5f;
                player.moveSpeed = 3.5f;
                player.AllowRandomEncounters();
                if (isPlayingGameNormally)
                {
                    StartCoroutine(GameManager.instance.FadeToBlack(0.7f));
                    StartCoroutine(TransitionToUnderground());
                    GameManager.instance.PlayFreeroamMusic();
                }
                else
                {
                    player.transform.position = new Vector2(-258, 175);
                }
                break;
            case 101:
                //poczatek patrolujacych szkieletow
                AdjustFollowerNPCsDistance(true);
                break;
            case 108:
                //koniec patrolujacych szkieletow
                AdjustFollowerNPCsDistance(false);
                break;
            case 110:
                //wyjscie z podziemi
                if (isPlayingGameNormally)
                {
                    PlayerPrefs.SetInt("cutsceneId", 2);
                    SceneManager.LoadScene("cutscenes");
                }
                break;
            case 111:
                player.transform.position = new Vector2(-300.5f, 45);
                GameManager.instance.currentFreeroamMusicStage = 4;
                if (isPlayingGameNormally) GameManager.instance.PlayFreeroamMusic();
                GameManager.instance.currentLocationText.text = "Parter, blok D";
                break;
            case 999:
                //powrot do g³ównej ekipy
                break;
        }
        if (isPlayingGameNormally)
        {
            foreach (int i in ShopManager.instance.storyRequirementsForUpgrades)
            {
                if (i == currentMainQuest)
                {
                    List<string> gameInfoLines = new List<string>();
                    gameInfoLines.Add("Ulepszenie sklepu dostêpne");
                    DialogueManager.instance.StartGameInfo(gameInfoLines.ToArray());
                }
            }
        }
        HandleAllNPCs();
    }

    void FinishSideQuest()
    {
        //showStoryNPCs jest true gdy gra nie jest wczytywana aka jest grana normalnie
        if (canReturnToMainStory) //gdy gra nie jest wczytywana LUB nie jestesmy w side quescie
        {
            HandleAllNPCs();
            currentQuestText.text = questDescriptions[currentMainQuest];
        }
        else //gry gra jest wczytywana i jestesmy w side questach
        {
            HandleSideQuestNPCs();
        }
    }

    public void ProgressSideQuest(int sideQuest, bool readDialogue = true)
    {
        DisableStoryNPCs();
        switch (sideQuest)
        {
            case 0:
                ProgressPingPongScams(readDialogue);
                break;
            case 1:
                ProgressFollowingReferees(readDialogue);
                break;
            case 2:
                ProgressDiversionAndSearch(readDialogue);
                break;
            case 3:
                ProgressStrongStuff(readDialogue);
                break;
            case 4:
                ProgressFaceTheCheater(readDialogue);
                break;
        }
    }

    void ProgressPingPongScams(bool readDialogue)
    {
        if (currentPingPongScamsQuest + 1 >= pingPongScamsQuestDescriptions.Length)
        {
            currentPingPongScamsQuest++;
            FinishSideQuest();
        }
        else
        {
            currentPingPongScamsQuest++;
            if (canReturnToMainStory)
            {
                currentQuestText.text = pingPongScamsQuestDescriptions[currentPingPongScamsQuest];
            }
            HandleSideQuestNPCs(0);
        }
    }

    void ProgressFollowingReferees(bool readDialogue)
    {
        if (currentFollowingRefereesQuest + 1 >= followingRefereesQuestDescriptions.Length)
        {
            currentFollowingRefereesQuest++;
            FinishSideQuest();
        }
        else
        {
            currentFollowingRefereesQuest++;
            if (canReturnToMainStory)
            {
                currentQuestText.text = followingRefereesQuestDescriptions[currentFollowingRefereesQuest];
            }
            switch (currentFollowingRefereesQuest)
            {
                case 1:
                    DisableFollowerNPCs();
                    break;
            }
            HandleSideQuestNPCs(1);
        }
    }

    void ProgressDiversionAndSearch(bool readDialogue)
    {
        if (currentDiversionAndSearchQuest + 1 >= diversionAndSearchQuestDescriptions.Length)
        {
            if (jaronaldMentioned)
            {
                if (readDialogue)
                {
                    string[] lines = {
                    "Czekajcie! Jaronald... Gdzieœ s³ysza³em to imiê.",
                    "To nie o nim mówili dresiarze?",
                    "Cholera.",
                    "Trzeba daæ znaæ Matiemu.",
                    "Napisa³em do niego, ¿eby na niego uwa¿a³."
                };
                    int[] speakerIndexes = { 1, 0, 2, 3, 0 };
                    DialogueManager.instance.StartDialogue(lines, speakerIndexes, additionalVoiceLines);
                }
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
            currentDiversionAndSearchQuest++;
            if (canReturnToMainStory)
            {
                currentQuestText.text = diversionAndSearchQuestDescriptions[currentDiversionAndSearchQuest];
            }
            HandleSideQuestNPCs(2);
        }
    }

    void ProgressStrongStuff(bool readDialogue)
    {
        if (currentStrongStuffQuest + 1 >= strongStuffQuestDescriptions.Length)
        {
            if (jaronaldMentioned)
            {
                if (readDialogue)
                {
                    string[] lines = {
                    "Czekajcie! Jaronald... Gdzieœ s³ysza³em to imiê.",
                    "To nie on by³ zaznaczony na liœcie zawodników turnieju?",
                    "Cholera.",
                    "Trzeba daæ znaæ Matiemu.",
                    "Napisa³em do niego, ¿eby na niego uwa¿a³."
                };
                    int[] speakerIndexes = { 1, 0, 2, 3, 0 };
                    DialogueManager.instance.StartDialogue(lines, speakerIndexes, additionalVoiceLines);
                }
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
            currentStrongStuffQuest++;
            if (canReturnToMainStory)
            {
                currentQuestText.text = strongStuffQuestDescriptions[currentStrongStuffQuest];
            }

            switch (currentStrongStuffQuest)
            {
                case 2:
                    EnablePonyStickers();
                    break;
                case 4:
                    DisablePonyStickers();
                    if (readDialogue)
                    {
                        string[] lines = {
                            "Ej, Wy! Co to ma kurna byæ, co?!",
                            "Oho..."
                        };
                        int[] speakerIndexes = { 19, 0 };
                        DialogueManager.instance.onDialogueEnd.RemoveAllListeners();
                        DialogueManager.instance.StartDialogue(lines, speakerIndexes, additionalVoiceLines);
                    }
                    
                    break;
            }
            HandleSideQuestNPCs(3);
        }
    }

    void ProgressFaceTheCheater(bool readDialogue)
    {
        if (currentFaceTheCheaterQuest + 1 >= faceTheCheaterQuestDescriptions.Length)
        {
            currentFaceTheCheaterQuest++;
            FinishSideQuest();
        }
        else
        {
            currentFaceTheCheaterQuest++;
            if (canReturnToMainStory)
            {
                currentQuestText.text = faceTheCheaterQuestDescriptions[currentFaceTheCheaterQuest];
            }
            HandleSideQuestNPCs(4);
        }
    }

    IEnumerator TransitionToLora()
    {
        yield return new WaitForSeconds(0.4f);
        player.ChangeAnimator(2);
        player.transform.position = new Vector2(-207, 52);
    }
    IEnumerator TransitionToUnderground()
    {
        yield return new WaitForSeconds(0.4f);
        player.transform.position = new Vector2(-258, 175);
    }
}
