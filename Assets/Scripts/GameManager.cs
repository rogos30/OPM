using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] AudioClip freeRoamMusic;

    public Canvas pauseCanvas, inGameCanvas;
    [SerializeField] TMP_Text gameFpsText;
    [SerializeField] TMP_Text currentLocationText;
    [SerializeField] TMP_Text currentQuestText;
    [SerializeField] GameObject optionsColumn;
    [SerializeField] GameObject inventoryMainColumn;
    [SerializeField] GameObject inventoryItemsColumn;
    [SerializeField] GameObject inventoryEquipmentColumn;
    [SerializeField] GameObject inventoryEqChangeColumn;
    [SerializeField] GameObject characterInfoColumn;
    [SerializeField] Image characterSprite;

    [SerializeField] AudioMixer mixer;
    AudioSource musicSource, sfxSource;
    public AudioMixerGroup musicMixerGroup, sfxMixerGroup;

    readonly string[] difficultyNames = {"£atwy", "Œredni", "Trudny", "Fatalny" };
    readonly string[] showFpsNames = { "Nigdy", "W œwiecie gry", "W walce", "Zawsze" };
    [NonSerialized] public bool canPause = true;
    int currentRow, maxCurrentRow, currentColumn, currentPage;
    int chosenMain, chosenInv, chosenEq;
    int sfxVolume = 25, musicVolume = 25, showFPS = 0;
    [NonSerialized] public int difficulty = 0;
    int frames;
    float framesTime = 0f, maxFramesTime = 0.5f;
    [SerializeField] TMP_Text[] mainColumnTexts;
    [SerializeField] TMP_Text[] optionsTexts;
    [SerializeField] TMP_Text[] optionValuesTexts;
    [SerializeField] TMP_Text[] inventoryMainTexts;
    [SerializeField] TMP_Text[] inventoryItemsTexts;
    [SerializeField] TMP_Text[] inventoryEquipmentTexts;
    [SerializeField] TMP_Text[] inventoryEqChangeTexts;
    [SerializeField] TMP_Text[] characterInfoTexts;
    [SerializeField] TMP_Text itemDescriptionText;
    [SerializeField] TMP_Text itemPageText;
    [SerializeField] TMP_Text eqDescriptionText;
    Color orange = new Color(0.976f, 0.612f, 0.007f);


    [SerializeField] GameObject[] NPCs;
    [NonSerialized] public int currentQuest = 0;
    readonly string[] questDescriptions = { "Zwerbuj Œwietlika", "Pobij Welenca dla zasady", "KONIEC GRY!!!1!" };

    private void Start()
    {
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadSettings();
        }
        SaveSettings();
    }
    private void Awake()
    {
        pauseCanvas.enabled = false;
        inGameCanvas.enabled = true;
        instance = this;
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.outputAudioMixerGroup = musicMixerGroup;
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.outputAudioMixerGroup = sfxMixerGroup;
        currentQuestText.text = questDescriptions[currentQuest];
        Inventory.Instance.InitializeItems();
        Inventory.Instance.InitializeWearables();
        HandleNPCs(); //initializing all npcs
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseCanvas.enabled || inGameCanvas.enabled)
        {
            if (pauseCanvas.enabled)
            {
                HandlePauseInput();
            }
            else
            {
                CountFPS();
            }
            if (!musicSource.isPlaying)
            {
                musicSource.clip = freeRoamMusic;
                musicSource.loop = true;
                musicSource.Play();
            }
        }
        else
        {
            musicSource.Stop();
        }
    }

    public void CountFPS()
    {
        if (framesTime < maxFramesTime)
        {
            framesTime += Time.deltaTime;
            frames++;
        }
        else
        {
            if (showFPS == 1 || showFPS == 3)
            { //freeroam or always
                gameFpsText.text = (float)frames / maxFramesTime + " fps";
            }
            else
            {
                gameFpsText.text = "";
            }
            if (showFPS == 2 || showFPS == 3)
            { //battle or always
                BattleManager.instance.battleFpsText.text = (float)frames / maxFramesTime + " fps";
            }
            else
            {
                BattleManager.instance.battleFpsText.text = "";
            }
            framesTime -= maxFramesTime;
            frames = 0;
        }
    }

    public void Pause()
    {
        canPause = false;
        currentRow = 0;
        mainColumnTexts[currentRow].color = orange;
        maxCurrentRow = mainColumnTexts.Length;
        Time.timeScale = 0;
        pauseCanvas.enabled = true;
    }

    void Unpause()
    {
        Time.timeScale = 1;
        pauseCanvas.enabled = false;
        StartCoroutine(AllowToPause());
        mainColumnTexts[currentRow].color = Color.white;
    }


    void HandlePauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (currentColumn)
            {
                case 0: //main column
                    Unpause();
                    break;
                case 1: //settings column
                    SaveSettings();
                    optionsColumn.SetActive(false);
                    currentColumn = 0;
                    optionsTexts[currentRow].color = Color.white;
                    optionValuesTexts[currentRow].color = Color.white;
                    currentRow = chosenMain;
                    mainColumnTexts[currentRow].color = orange;
                    maxCurrentRow = mainColumnTexts.Length;
                    break;
                case 2: //inventory main column
                    inventoryMainColumn.SetActive(false);
                    currentColumn = 0;
                    inventoryMainTexts[currentRow].color = Color.white;
                    currentRow = chosenMain;
                    mainColumnTexts[currentRow].color = orange;
                    maxCurrentRow = mainColumnTexts.Length;
                    break;
                case 3: //healing column
                    inventoryItemsColumn.SetActive(false);
                    currentColumn = 2;
                    inventoryItemsTexts[currentRow].color = Color.white;
                    currentRow = chosenInv;
                    inventoryMainTexts[currentRow].color = orange;
                    maxCurrentRow = BattleManager.instance.playableCharacters.Count + 1;
                    break;
                case 4: //equipment column
                    inventoryEquipmentColumn.SetActive(false);
                    currentColumn = 2;
                    inventoryEquipmentTexts[currentRow].color = Color.white;
                    currentRow = chosenInv;
                    inventoryMainTexts[currentRow].color = orange;
                    maxCurrentRow = BattleManager.instance.playableCharacters.Count + 1;
                    break;
                case 5: //eq change column
                    inventoryEqChangeColumn.SetActive(false);
                    currentColumn = 4;
                    inventoryEqChangeTexts[currentRow].color = Color.white;
                    currentRow = chosenEq;
                    inventoryEquipmentTexts[currentRow].color = orange;
                    maxCurrentRow = inventoryEquipmentTexts.Length;
                    break;
                case 6: //char info column
                    characterInfoColumn.SetActive(false);
                    currentColumn = 0;
                    mainColumnTexts[currentRow].color = orange;
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            switch (currentColumn)
            {
                case 0: //main column
                    mainColumnTexts[currentRow].color = Color.white;
                    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                    {
                        currentRow = (currentRow + 1) % maxCurrentRow;
                    }
                    else
                    {
                        currentRow = (currentRow - 1 < 0) ? (maxCurrentRow - 1) : (currentRow - 1);
                    }
                    mainColumnTexts[currentRow].color = orange;
                    break;
                case 1: //settings column
                    optionsTexts[currentRow].color = Color.white;
                    optionValuesTexts[currentRow].color = Color.white;
                    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                    {
                        currentRow = (currentRow + 1) % maxCurrentRow;
                    }
                    else
                    {
                        currentRow = (currentRow - 1 < 0) ? (maxCurrentRow - 1) : (currentRow - 1);
                    }
                    optionsTexts[currentRow].color = orange;
                    optionValuesTexts[currentRow].color = orange;
                    break;
                case 2: //main inventory column
                    inventoryMainTexts[currentRow].color = Color.white;
                    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                    {
                        currentRow = (currentRow + 1) % maxCurrentRow;
                    }
                    else
                    {
                        currentRow = (currentRow - 1 < 0) ? (maxCurrentRow - 1) : (currentRow - 1);
                    }
                    inventoryMainTexts[currentRow].color = orange;
                    break;
                case 3: //items inventory column
                    inventoryItemsTexts[currentRow].color = Color.white;
                    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                    {
                        currentRow = (currentRow + 1) % maxCurrentRow;
                    }
                    else
                    {
                        currentRow = (currentRow - 1 < 0) ? (maxCurrentRow - 1) : (currentRow - 1);
                    }
                    inventoryItemsTexts[currentRow].color = orange;
                    itemDescriptionText.text = Inventory.Instance.items[currentPage * 4 + currentRow].Description + ". Masz: " +
                        Inventory.Instance.items[currentPage * 4 + currentRow].Amount;
                    break;
                case 4: //equipment column
                    inventoryEquipmentTexts[currentRow].color = Color.white;
                    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                    {
                        currentRow = (currentRow + 1) % maxCurrentRow;
                    }
                    else
                    {
                        currentRow = (currentRow - 1 < 0) ? (maxCurrentRow - 1) : (currentRow - 1);
                    }
                    inventoryEquipmentTexts[currentRow].color = orange;
                    break;
                case 5: //eq change column
                    inventoryEqChangeTexts[currentRow].color = Color.white;
                    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                    {
                        currentRow = (currentRow + 1) % maxCurrentRow;
                    }
                    else
                    {
                        currentRow = (currentRow - 1 < 0) ? (maxCurrentRow - 1) : (currentRow - 1);
                    }
                    inventoryEqChangeTexts[currentRow].color = orange;
                    if (currentRow > 0)
                    {
                        eqDescriptionText.text = Inventory.Instance.wearables[(currentRow - 1) * 4 + chosenEq].Description + ". Masz: " +
                        Inventory.Instance.wearables[(currentRow - 1) * 4 + chosenEq].Amount;
                    }
                    else
                    {
                        eqDescriptionText.text = "";
                    }
                    
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            switch (currentColumn)
            {
                case 1: //settings
                    switch(currentRow)
                    {
                        case 0:
                            sfxVolume = Mathf.Max(sfxVolume - 5, 0);
                            break;
                        case 1:
                            musicVolume = Mathf.Max(musicVolume - 5, 0);
                            break;
                        case 2:
                            difficulty = Mathf.Max(difficulty - 1, 0);
                            break;
                        case 3:
                            showFPS = Mathf.Max(showFPS - 1, 0);
                            break;
                    }
                    UpdateSettings();
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            switch (currentColumn)
            {
                case 1: //settings
                    switch (currentRow)
                    {
                        case 0:
                            sfxVolume = Mathf.Min(sfxVolume + 5, 100);
                            break;
                        case 1:
                            musicVolume = Mathf.Min(musicVolume + 5, 100);
                            break;
                        case 2:
                            difficulty = Mathf.Min(difficulty + 1, difficultyNames.Length - 1);
                            break;
                        case 3:
                            showFPS = Mathf.Min(showFPS + 1, showFpsNames.Length - 1);
                            break;
                    }
                    UpdateSettings();
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (currentColumn)
            {
                case 0: //currently in main column
                    switch (currentRow)
                    {
                        case 0: //resumed
                            Unpause();
                            break;
                        case 1: //entered inventory
                            currentColumn = 2;
                            chosenMain = currentRow;
                            mainColumnTexts[chosenMain].color = Color.red;
                            inventoryMainTexts[currentRow = 0].color = orange;
                            maxCurrentRow = BattleManager.instance.playableCharacters.Count + 1;
                            for (int i = 1; i < inventoryMainTexts.Length; i++)
                            {
                                inventoryMainTexts[i].text = "";
                            }
                            for (int i = 0; i < BattleManager.instance.playableCharacters.Count; i++)
                            {
                                inventoryMainTexts[i + 1].text = BattleManager.instance.playableCharacters[i].Name;
                            }
                            inventoryMainColumn.SetActive(true);
                            break;
                        case 2: //entered char info
                            currentColumn = 6;
                            chosenMain = currentRow;
                            mainColumnTexts[chosenMain].color = Color.red;
                            PrintCharInfo();
                            characterInfoColumn.SetActive(true);
                            break;
                        case 3: //entered settings
                            currentColumn = 1;
                            chosenMain = currentRow;
                            mainColumnTexts[chosenMain].color = Color.red;
                            optionsTexts[currentRow = 0].color = orange;
                            optionValuesTexts[currentRow].color = orange;
                            maxCurrentRow = optionsTexts.Length;
                            optionsColumn.SetActive(true);
                            break;
                        case 4: //returned to main
                            break;
                    }
                    break;
                case 2: //currently in main inventory
                    switch (currentRow)
                    {
                        case 0: //healing items
                            currentColumn = 3;
                            currentPage = 0;
                            PrintCurrentPageOfItems();
                            itemPageText.text = "Strona: " + (currentPage + 1) + "/" + (ShopManager.instance.level + 1);
                            itemDescriptionText.text = Inventory.Instance.items[currentPage * 4 + currentRow].Description + ". Masz: " +
                                Inventory.Instance.items[currentPage * 4 + currentRow].Amount;
                            chosenInv = currentRow;
                            inventoryMainTexts[chosenInv].color = Color.red;
                            inventoryItemsTexts[currentRow = 0].color = orange;
                            maxCurrentRow = inventoryItemsTexts.Length;
                            inventoryItemsColumn.SetActive(true);
                            break;
                        default: //character's equipment
                            currentColumn = 4;
                            chosenInv = currentRow;
                            inventoryMainTexts[chosenInv].color = Color.red;
                            inventoryEquipmentTexts[currentRow = 0].color = orange;
                            maxCurrentRow = inventoryEquipmentTexts.Length;
                            inventoryEquipmentColumn.SetActive(true);
                            break;
                    }
                    break;
                case 4: //currently in equipment inventory
                    currentColumn = 5;
                    chosenEq = currentRow;
                    inventoryEquipmentTexts[chosenEq].color = Color.red;
                    inventoryEqChangeTexts[currentRow = 0].color = orange;
                    maxCurrentRow = ShopManager.instance.level + 2;
                    inventoryEqChangeColumn.SetActive(true);
                    PrintAvailableEquipment();
                    eqDescriptionText.text = "";
                    break;
                case 5: //currently in eq change
                    switch (currentRow)
                    {
                        case 0: //taking off equipment
                            if (BattleManager.instance.playableCharacters[chosenInv - 1].wearablesWorn[chosenEq] != null)
                            {
                                BattleManager.instance.playableCharacters[chosenInv - 1].wearablesWorn[chosenEq].Amount++;
                                BattleManager.instance.playableCharacters[chosenInv - 1].wearablesWorn[chosenEq] = null;
                            }
                            break;
                        default: //replacing current equipment
                            if (Inventory.Instance.wearables[(currentRow - 1) * 4 + chosenEq].Amount > 0)
                            {
                                if (BattleManager.instance.playableCharacters[chosenInv - 1].wearablesWorn[chosenEq] != null)
                                { //is currently wearing something
                                    BattleManager.instance.playableCharacters[chosenInv - 1].wearablesWorn[chosenEq].Amount++;
                                }
                                Inventory.Instance.wearables[(currentRow - 1) * 4 + chosenEq].Amount--;
                                BattleManager.instance.playableCharacters[chosenInv - 1].wearablesWorn[chosenEq] = Inventory.Instance.wearables[(currentRow - 1) * 4 + chosenEq];
                            }
                            break;
                    }
                    inventoryEqChangeColumn.SetActive(false);
                    currentColumn = 4;
                    inventoryEqChangeTexts[currentRow].color = Color.white;
                    currentRow = chosenEq;
                    inventoryEquipmentTexts[currentRow].color = orange;
                    maxCurrentRow = inventoryEquipmentTexts.Length;
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            switch (currentColumn)
            {
                case 3: //healing items
                    currentPage = (currentPage - 1 < 0) ? currentPage : currentPage - 1;
                    itemPageText.text = "Strona: " + (currentPage + 1) + "/" + (ShopManager.instance.level + 1);
                    itemDescriptionText.text = Inventory.Instance.items[currentPage * 4 + currentRow].Description + ". Masz: " +
                        Inventory.Instance.items[currentPage * 4 + currentRow].Amount;
                    PrintCurrentPageOfItems();
                    break;
                case 6: //char info items
                    currentPage = (currentPage - 1 < 0) ? currentPage : currentPage - 1;
                    PrintCharInfo();
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            switch (currentColumn)
            {
                case 3: //healing items
                    currentPage = (currentPage + 1 > ShopManager.instance.level) ? currentPage : currentPage + 1;
                    itemPageText.text = "Strona: " + (currentPage + 1) + "/" + (ShopManager.instance.level + 1);
                    itemDescriptionText.text = Inventory.Instance.items[currentPage * 4 + currentRow].Description + ". Masz: " +
                        Inventory.Instance.items[currentPage * 4 + currentRow].Amount;
                    PrintCurrentPageOfItems();
                    break;
                case 6:
                    currentPage = (currentPage + 1 >= BattleManager.instance.playableCharacters.Count) ? currentPage : currentPage + 1;
                    PrintCharInfo();
                    break;

            }
        }
    }

    void PrintCharInfo()
    {
        int addedAttack = 0, addedDefense = 0;
        float accuracyModifier = 1, healingModifier = 1;
        for (int i = 0; i < 4; i++)
        {
            if (BattleManager.instance.playableCharacters[currentPage].wearablesWorn[i] != null)
            {
                addedAttack += BattleManager.instance.playableCharacters[currentPage].wearablesWorn[i].AttackAdded;
                addedDefense += BattleManager.instance.playableCharacters[currentPage].wearablesWorn[i].DefenseAdded;
                accuracyModifier *= BattleManager.instance.playableCharacters[currentPage].wearablesWorn[i].AccuracyMultiplier;
                healingModifier *= BattleManager.instance.playableCharacters[currentPage].wearablesWorn[i].HealingMultiplier;
            }
        }
        characterSprite.sprite = DialogManager.instance.speakerSprites[currentPage];
        characterInfoTexts[0].text = BattleManager.instance.playableCharacters[currentPage].Name;
        characterInfoTexts[1].text = "Poziom: " + BattleManager.instance.playableCharacters[currentPage].Level;
        characterInfoTexts[2].text = "XP do nast.: " + (BattleManager.instance.playableCharacters[currentPage].XPToNextLevel - BattleManager.instance.playableCharacters[currentPage].CurrentXP);
        characterInfoTexts[3].text = "HP: " + BattleManager.instance.playableCharacters[currentPage].MaxHealth;
        characterInfoTexts[4].text = "SP: " + BattleManager.instance.playableCharacters[currentPage].MaxSkill;
        characterInfoTexts[5].text = "Atak: " + (BattleManager.instance.playableCharacters[currentPage].DefaultAttack + addedAttack);
        characterInfoTexts[6].text = "Obrona: " + (BattleManager.instance.playableCharacters[currentPage].DefaultAttack + addedDefense);
        characterInfoTexts[7].text = "Szybkoœæ: " + BattleManager.instance.playableCharacters[currentPage].Speed;
        characterInfoTexts[8].text = "Ruchy w turze: " + BattleManager.instance.playableCharacters[currentPage].Turns;
        characterInfoTexts[9].text = "Leczenie: " + healingModifier * 100 + "%";
        characterInfoTexts[10].text = "Celnoœæ: " + BattleManager.instance.playableCharacters[currentPage].Accuracy * accuracyModifier * 100 + "%";
        characterInfoTexts[11].text = "Opis - work in progress";
    }

    void LoadSettings()
    {
        musicVolume = PlayerPrefs.GetInt("musicVolume");
        if (musicVolume == 0)
        {
            mixer.SetFloat("musicVolume", Mathf.Log10(0.01f));
        }
        else
        {
            mixer.SetFloat("musicVolume", Mathf.Log10((float)musicVolume / 100) * 20);
        }
        sfxVolume = PlayerPrefs.GetInt("sfxVolume");
        if (sfxVolume == 0)
        {
            mixer.SetFloat("sfxVolume", Mathf.Log10(0.01f));
        }
        else
        {
            mixer.SetFloat("sfxVolume", Mathf.Log10((float)sfxVolume / 100) * 20);
        }
        difficulty = PlayerPrefs.GetInt("difficulty");
        showFPS = PlayerPrefs.GetInt("showFPS");
        UpdateSettings();
    }
    void SaveSettings()
    {
        PlayerPrefs.SetInt("sfxVolume", sfxVolume);
        if (sfxVolume == 0)
        {
            mixer.SetFloat("sfxVolume", Mathf.Log10(0.0001f) * 20);
        }
        else
        {
            mixer.SetFloat("sfxVolume", Mathf.Log10((float)sfxVolume / 100) * 20);
        }
        PlayerPrefs.SetInt("musicVolume", musicVolume);
        if (musicVolume == 0)
        {
            mixer.SetFloat("musicVolume", Mathf.Log10(0.0001f) * 20);
        }
        else
        {
            mixer.SetFloat("musicVolume", Mathf.Log10((float)musicVolume / 100) * 20);
        }
        PlayerPrefs.SetInt("difficulty", difficulty);
        foreach (var enemy in BattleManager.instance.enemyCharacters)
        {
            enemy.UpdateDifficulty();
        }
        PlayerPrefs.SetInt("showFPS", showFPS);
        PlayerPrefs.Save();
    }
    void UpdateSettings()
    {
        optionValuesTexts[0].text = sfxVolume.ToString();
        optionValuesTexts[1].text = musicVolume.ToString();
        optionValuesTexts[2].text = difficultyNames[difficulty];
        optionValuesTexts[3].text = showFpsNames[showFPS];
    }

    void PrintCurrentPageOfItems()
    {
        for (int i = 0; i < inventoryItemsTexts.Length; i++)
        {
            inventoryItemsTexts[i].text = Inventory.Instance.items[currentPage * 4 + i].Name;
        }
    }

    void PrintAvailableEquipment()
    {
        for (int i = 1; i < maxCurrentRow; i++)
        {
            inventoryEqChangeTexts[i].text = Inventory.Instance.wearables[(i-1) * 4 + chosenEq].Name;
        }
    }

    IEnumerator AllowToPause()
    {
        yield return new WaitForSeconds(1);
        canPause = true;
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
        currentQuestText.text = questDescriptions[++currentQuest];
        Skill[] skillTable = BattleManager.instance.skillTable;
        List<Skill> skills;
        Character character;
        switch (currentQuest)
        {
            case 1:
                skills = new List<Skill> { skillTable[0], skillTable[2], skillTable[8], skillTable[12], skillTable[13] };
                character = new Character("Œwietlik", 500, 0, 80, 110, 0, 40, 0.9f, 1, 500, 0, 0, skills);
                BattleManager.instance.playableCharacters.Add(character);
                break;
            case 2:
                skills = new List<Skill> { skillTable[0], skillTable[14], skillTable[15], skillTable[16], skillTable[17] };
                character = new Character("Stasiak", 450, 0, 100, 110, 0, 30, 1, 1, 400, 0, 0, skills);
                BattleManager.instance.playableCharacters.Add(character);

                skills = new List<Skill> { skillTable[0], skillTable[18], skillTable[19], skillTable[20], skillTable[21] };
                character = new Character("Kaja", 350, 0, 80, 70, 0, 30, 0.9f, 1, 300, 0, 0, skills);
                BattleManager.instance.playableCharacters.Add(character);

                skills = new List<Skill> { skillTable[0], skillTable[22], skillTable[23], skillTable[24], skillTable[25], skillTable[26] };
                character = new Character("Brudzyñski", 450, 0, 100, 100, 0, 30, 0.8f, 1, 200, 0, 0, skills);
                BattleManager.instance.playableCharacters.Add(character);
                break;
        }

        HandleNPCs();
    }
}
