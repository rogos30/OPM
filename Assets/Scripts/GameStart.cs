using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;
using static UnityEngine.GraphicsBuffer;


public class GameStart : MonoBehaviour
{
    public static GameStart instance;
    [SerializeField] AudioClip freeRoamMusic;

    public Canvas mainCanvas;
    [SerializeField] GameObject optionsColumn;
    [SerializeField] GameObject slotsColumn;
    [SerializeField] GameObject AYSColumn;
    [SerializeField] GameObject inventoryEquipmentColumn;
    [SerializeField] GameObject inventoryEqChangeColumn;
    [SerializeField] GameObject characterInfoColumn;
    [SerializeField] Image characterSprite;

    [SerializeField] AudioMixer mixer;
    AudioSource musicSource, sfxSource;
    public AudioMixerGroup musicMixerGroup, sfxMixerGroup;

    readonly string[] difficultyNames = {"£atwy", "Œredni", "Trudny", "Fatalny" };
    readonly string[] showFpsNames = { "Nigdy", "W œwiecie gry", "W walce", "Zawsze" };
    int currentRow, maxCurrentRow, currentColumn;
    int chosenMain, chosenSlot;
    int sfxVolume = 25, musicVolume = 25, showFPS = 0;
    [NonSerialized] public int difficulty = 0;
    [SerializeField] TMP_Text[] mainColumnTexts;
    [SerializeField] TMP_Text[] optionsTexts;
    [SerializeField] TMP_Text[] optionValuesTexts;
    [SerializeField] TMP_Text[] slotsTexts;
    [SerializeField] TMP_Text[] AYSTexts;
    [SerializeField] TMP_Text[] inventoryEquipmentTexts;
    [SerializeField] TMP_Text[] inventoryEqChangeTexts;
    [SerializeField] TMP_Text[] characterInfoTexts;
    [SerializeField] TMP_Text itemDescriptionText;
    [SerializeField] TMP_Text itemPageText;
    [SerializeField] TMP_Text eqDescriptionText;
    Color orange = new Color(0.976f, 0.612f, 0.007f);

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
        mainCanvas.enabled = true;
        instance = this;
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.outputAudioMixerGroup = musicMixerGroup;
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.outputAudioMixerGroup = sfxMixerGroup;
    }

    // Update is called once per frame
    void Update()
    {
        HandlePauseInput();
        if (!musicSource.isPlaying)
        {
            musicSource.clip = freeRoamMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    void HandlePauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (currentColumn)
            {
                case 0: //main column
                    #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                    #endif
                    Application.Quit();
                    break;
                case 1: //new game slots
                    slotsColumn.SetActive(false);
                    currentColumn = 0;
                    slotsTexts[currentRow].color = Color.white;
                    currentRow = chosenMain;
                    mainColumnTexts[currentRow].color = orange;
                    maxCurrentRow = mainColumnTexts.Length;
                    break;
                case 2: //new game - are you sure?
                    AYSColumn.SetActive(false);
                    currentColumn = 1;
                    AYSTexts[currentRow].color = Color.white;
                    currentRow = chosenSlot;
                    mainColumnTexts[currentRow].color = orange;
                    maxCurrentRow = slotsTexts.Length;
                    break;
                case 3: //load game slots
                    slotsColumn.SetActive(false);
                    currentColumn = 0;
                    slotsTexts[currentRow].color = Color.white;
                    currentRow = chosenMain;
                    slotsTexts[currentRow].color = orange;
                    maxCurrentRow = mainColumnTexts.Length;
                    break;
                case 4: //load game - are you sure?
                    AYSColumn.SetActive(false);
                    currentColumn = 3;
                    AYSTexts[currentRow].color = Color.white;
                    currentRow = chosenSlot;
                    AYSTexts[currentRow].color = orange;
                    maxCurrentRow = slotsTexts.Length;
                    break;
                case 5: //options
                    SaveSettings();
                    optionsColumn.SetActive(false);
                    currentColumn = 0;
                    optionsTexts[currentRow].color = Color.white;
                    currentRow = chosenMain;
                    optionsTexts[currentRow].color = orange;
                    maxCurrentRow = mainColumnTexts.Length;
                    break;
                case 6: //exit - are you sure?
                    AYSColumn.SetActive(false);
                    currentColumn = 0;
                    AYSTexts[currentRow].color = Color.white;
                    currentRow = chosenMain;
                    mainColumnTexts[currentRow].color = orange;
                    maxCurrentRow = mainColumnTexts.Length;
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
                        case 0: //new game
                            currentColumn = 1;
                            chosenMain = currentRow;
                            slotsTexts[currentRow = 0].color = orange;
                            maxCurrentRow = slotsTexts.Length - 1;
                            break;
                        case 1: //load game
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
                                inventoryMainTexts[i + 1].text = BattleManager.instance.playableCharacters[i].NominativeName;
                            }
                            inventoryMainColumn.SetActive(true);
                            break;
                        case 2: //options
                            currentColumn = 6;
                            chosenMain = currentRow;
                            mainColumnTexts[chosenMain].color = Color.red;
                            PrintCharInfo();
                            characterInfoColumn.SetActive(true);
                            break;
                        case 3: //exit
                            currentColumn = 1;
                            chosenMain = currentRow;
                            mainColumnTexts[chosenMain].color = Color.red;
                            optionsTexts[currentRow = 0].color = orange;
                            optionValuesTexts[currentRow].color = orange;
                            maxCurrentRow = optionsTexts.Length;
                            optionsColumn.SetActive(true);
                            break;
                    }
                    break;
                case 1: //new game slots
                    switch (currentRow)
                    {
                        case 0: //slot 1
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
                        default: //slot 2, slot 3 TODO
                            currentColumn = 4;
                            chosenInv = currentRow;
                            inventoryMainTexts[chosenInv].color = Color.red;
                            inventoryEquipmentTexts[currentRow = 0].color = orange;
                            maxCurrentRow = inventoryEquipmentTexts.Length;
                            inventoryEquipmentColumn.SetActive(true);
                            break;
                    }
                    break;
                case 2: //load game slots TODO
                    currentColumn = 5;
                    chosenEq = currentRow;
                    inventoryEquipmentTexts[chosenEq].color = Color.red;
                    inventoryEqChangeTexts[currentRow = 0].color = orange;
                    maxCurrentRow = ShopManager.instance.level + 2;
                    inventoryEqChangeColumn.SetActive(true);
                    PrintAvailableEquipment();
                    eqDescriptionText.text = "";
                    break;
                case 3: //options TODO
                    switch (currentRow)
                    {
                        case 0: //taking off equipment
                            if (BattleManager.instance.playableCharacters[chosenInv - 1].wearablesWorn[chosenEq] != null)
                            {
                                BattleManager.instance.playableCharacters[chosenInv - 1].wearablesWorn[chosenEq].TakeOff(BattleManager.instance.playableCharacters[chosenInv - 1]);
                            }
                            break;
                        default: //replacing current equipment
                            if (Inventory.Instance.wearables[(currentRow - 1) * 4 + chosenEq].Amount > 0)
                            {
                                if (BattleManager.instance.playableCharacters[chosenInv - 1].wearablesWorn[chosenEq] != null)
                                { //is currently wearing something
                                    BattleManager.instance.playableCharacters[chosenInv - 1].wearablesWorn[chosenEq].TakeOff(BattleManager.instance.playableCharacters[chosenInv - 1]);
                                }
                                Inventory.Instance.wearables[(currentRow - 1) * 4 + chosenEq].PutOn(BattleManager.instance.playableCharacters[chosenInv - 1]);
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
                case 4: //exit TODO
                    //switch (currentRow)
                    //{
                       
                    //}
                    break;
            }
        }
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
            enemy.UpdateDifficulty(difficulty);
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

}
