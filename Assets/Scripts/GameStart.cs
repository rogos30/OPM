using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;
using static UnityEngine.GraphicsBuffer;


public class GameStart : MonoBehaviour
{
    public static GameStart instance;
    [SerializeField] AudioClip backgroundMusic;

    public Canvas mainCanvas;

    [SerializeField] AudioMixer mixer;
    AudioSource musicSource, sfxSource;
    public AudioMixerGroup musicMixerGroup, sfxMixerGroup;

    readonly string[] difficultyNames = {"�atwy", "�redni", "Trudny", "Fatalny" };
    readonly string[] showFpsNames = { "Nigdy", "W �wiecie gry", "W walce", "Zawsze" };
    int currentRow, maxCurrentRow, currentState, chosenSaveSlot;
    int sfxVolume = 25, musicVolume = 25, showFPS = 0;
    [NonSerialized] public int difficulty = 0;
    [SerializeField] TMP_Text guideText;
    [SerializeField] TMP_Text[] mainTexts;
    [SerializeField] TMP_Text[] optionValuesTexts;
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
        currentState = 0;
        currentRow = 0;
        mainTexts[0].color = orange;
        maxCurrentRow = 4;
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
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    void HandlePauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (currentState)
            {
                case 0: //main view
                    currentState = 6;
                    PrintYesNo();
                    break;
                case 1: //new game slots
                    currentState = 0;
                    PrintMainView();
                    break;
                case 2: //new game - are you sure?
                    currentState = 1;
                    PrintSlots();
                    break;
                case 3: //load game slots
                    currentState = 0;
                    PrintMainView();
                    break;
                case 4: //load game - are you sure?
                    currentState = 3;
                    PrintSlots();
                    break;
                case 5: //options
                    SaveSettings();
                    currentState = 0;
                    PrintMainView();
                    break;
                case 6: //exit - are you sure?
                    currentState = 0;
                    PrintMainView();
                    break;
            }
            mainTexts[currentRow].color = Color.white;
            mainTexts[currentRow = 0].color = orange;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            mainTexts[currentRow].color = Color.white;
            if (currentState == 5) //settings
            {
                optionValuesTexts[currentRow].color = Color.white;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                currentRow = (currentRow + 1) % maxCurrentRow;
            }
            else
            {
                currentRow = (currentRow - 1 < 0) ? (maxCurrentRow - 1) : (currentRow - 1);
            }
            mainTexts[currentRow].color = orange;
            if (currentState == 5) //settings
            {
                optionValuesTexts[currentRow].color = orange;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (currentState == 5) //settings
            {
                switch (currentRow)
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
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (currentState == 5) //settings
            {
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
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (currentState)
            {
                case 0: //currently in main view
                    mainTexts[currentRow].color = Color.white;
                    switch (currentRow)
                    {
                        case 0: //new game
                            currentState = 1;
                            PrintSlots();
                            break;
                        case 1: //load game
                            currentState = 3;
                            PrintSlots();
                            break;
                        case 2: //options
                            currentState = 5;
                            PrintSettings();
                            optionValuesTexts[currentRow = 0].color = orange;
                            break;
                        case 3: //exit
                            currentState = 6;
                            PrintYesNo();
                            break;
                    }
                    mainTexts[currentRow = 0].color = orange;
                    break;
                case 1: //new game slots
                    chosenSaveSlot = currentRow;
                    PrintYesNo();
                    currentState = 2;
                    break;
                case 2: //new game slots - are you sure?
                    switch (currentRow)
                    {
                        case 0: //yes

                            string dataDirPath1 = Application.persistentDataPath;
                            string dataFileName1 = currentRow.ToString();
                            string fullPath1 = Path.Combine(dataDirPath1, dataFileName1);
                            if (File.Exists(fullPath1))
                            {
                                File.Delete(fullPath1);
                            }
                            PlayerPrefs.SetString("lastSaveFile", chosenSaveSlot.ToString());
                            SceneManager.LoadScene("world");
                            break;
                        case 1: //no
                            currentState = 1;
                            PrintSlots();
                            mainTexts[currentRow = 0].color = orange;
                            break;
                    }
                    break;
                case 3: //load game slots
                    string dataDirPath = Application.persistentDataPath;
                    string dataFileName = currentRow.ToString();
                    string fullPath = Path.Combine(dataDirPath, dataFileName);
                    if (File.Exists(fullPath))
                    {
                        chosenSaveSlot = currentRow;
                        PrintYesNo();
                        currentState = 4;
                    }
                    break;
                case 4: //load game slots - are you sure?
                    switch (currentRow)
                    {
                        case 0: //yes
                            PlayerPrefs.SetString("lastSaveFile", chosenSaveSlot.ToString());
                            SceneManager.LoadScene("world");
                            break;
                        case 1: //no
                            currentState = 3;
                            PrintSlots();
                            mainTexts[currentRow = 0].color = orange;
                            break;
                    }
                    break;
                case 6:
                    switch (currentRow)
                    {
                        case 0: //yes
                            #if UNITY_EDITOR
                            UnityEditor.EditorApplication.isPlaying = false;
                            #endif
                            Application.Quit();

                            break;
                        case 1: //no
                            currentState = 0;
                            PrintMainView();
                            mainTexts[currentRow = 0].color = orange;
                            break;
                    }
                    break;
            }
        }

        if (Input.anyKeyDown)
        {
            UpdateGuideText();
            //Debug.Log("currentState " + currentState);
            //Debug.Log("currentRow " + currentRow);
        }
    }

    void PrintYesNo()
    {
        ClearTexts();
        mainTexts[0].text = "Potwierd�";
        mainTexts[1].text = "Cofnij";
        maxCurrentRow = 2;
    }
    void PrintSettings()
    {
        ClearTexts();
        mainTexts[0].text = "G�o�no�� d�wi�k�w";
        mainTexts[1].text = "G�o�no�� muzyki";
        mainTexts[2].text = "Poziom trudno�ci";
        mainTexts[3].text = "Poka� FPS";
        UpdateSettings();
        maxCurrentRow = 4;
    }

    void PrintSlots()
    {
        ClearTexts();
        mainTexts[0].text = "Slot 1";
        mainTexts[1].text = "Slot 2";
        mainTexts[2].text = "Slot 3";
        mainTexts[3].text = "Slot 4";
        maxCurrentRow = 4;
    }

    void PrintMainView()
    {
        ClearTexts();
        mainTexts[0].text = "Nowa gra";
        mainTexts[1].text = "Wczytaj gr�";
        mainTexts[2].text = "Ustawienia";
        mainTexts[3].text = "Wyjd� z gry";
        maxCurrentRow = 4;
    }

    void UpdateGuideText()
    {
        switch(currentState)
        {
            case 0: //main view
                switch (currentRow)
                {
                    case 0:
                        guideText.text = "Rozpocznij przygod� od pocz�tku";
                        break;
                    case 1:
                        guideText.text = "Kontynuuj przygod�";
                        break;
                    case 2:
                        guideText.text = "Zmie� ustawienia";
                        break;
                    case 3:
                        guideText.text = "Wyjd� do pulpitu";
                        break;
                }
                break;
            case 1: //new game slots
                GetInfoFromFile();
                break;
            case 2: //new game - are you sure?
                guideText.text = "Czy na pewno rozpocz�� now� gr� na miejscu zapisu " + (chosenSaveSlot + 1) + "?";
                break;
            case 3: //load game slots
                GetInfoFromFile();
                break;
            case 4: //load game - are you sure?
                guideText.text = "Czy na pewno wczyta� gr� z zapisu " + (chosenSaveSlot + 1) + "?";
                break;

            case 5: //settings
                switch (currentRow)
                {
                    case 0:
                        guideText.text = "G�o�no�� kr�tkich efekt�w d�wi�kowych";
                        break;
                    case 1:
                        guideText.text = "G�o�no�� muzyki w tle";
                        break;
                    case 2:
                        guideText.text = "Wybierz poziom trudno�ci gry";
                        break;
                    case 3:
                        guideText.text = "Kiedy wy�wietla� ilo�� klatek na sekund�?";
                        break;
                }
                break;

            case 6: //exit game - are you sure?
                guideText.text = "Czy na pewno chcesz wyj�� z gry?";
                break;
        }
        
    }

    void ClearTexts()
    {
        mainTexts[currentRow].color = Color.white;
        optionValuesTexts[currentRow].color = Color.white;
        foreach (var action in mainTexts)
        {
            action.text = "";
        }
        foreach (var action in optionValuesTexts)
        {
            action.text = "";
        }
        mainTexts[currentRow = 0].color = orange;
        optionValuesTexts[currentRow = 0].color = orange;
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

    void GetInfoFromFile()
    {
        string dataDirPath = Application.persistentDataPath;
        string dataFileName = currentRow.ToString();
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        if (!File.Exists(fullPath))
        {
            guideText.text = "Puste miejsce zapisu";
            return;
        }
        //Debug.Log(fullPath);
        using (FileStream stream = new FileStream(fullPath, FileMode.Open))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                int currentMainQuest = int.Parse(reader.ReadLine());
                string date = reader.ReadLine();
                guideText.text = "Data zapisu: " + date + ", post�p: " + (100f * currentMainQuest / 15) + "%";
            }
        }
    }
}
