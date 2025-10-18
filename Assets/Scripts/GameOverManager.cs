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


public class GameOverManager : MonoBehaviour
{
    public static GameOverManager instance;
    [SerializeField] AudioClip backgroundMusic;

    public Canvas mainCanvas;

    [SerializeField] AudioMixer mixer;
    AudioSource musicSource, sfxSource;
    public AudioMixerGroup musicMixerGroup, sfxMixerGroup;

    int currentRow, minCurrentRow, maxCurrentRow, currentState;
    int sfxVolume = 25, musicVolume = 25, showFPS = 0;
    [NonSerialized] public int difficulty = 0;
    [SerializeField] TMP_Text guideText;
    [SerializeField] TMP_Text[] mainTexts;
    Color orange = new Color(0.976f, 0.612f, 0.007f);

    [SerializeField] AudioClip navigationScrollSound;
    [SerializeField] AudioClip navigationCancelSound;
    [SerializeField] AudioClip navigationAcceptSound;

    [SerializeField] GameObject mainTextArea;
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
        maxCurrentRow = 3;
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
            sfxSource.clip = navigationCancelSound;
            sfxSource.loop = false;
            sfxSource.Play();
            switch (currentState)
            {
                case 0: //main view
                    currentState = 2;
                    PrintYesNo();
                    break;
                case 1: //restart - are you sure?
                    currentState = 0;
                    PrintMainView();
                    break;
                case 2: //exit - are you sure?
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
            sfxSource.clip = navigationScrollSound;
            sfxSource.loop = false;
            sfxSource.Play();
            mainTexts[currentRow].color = Color.white;
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                currentRow = (currentRow + 1 >= maxCurrentRow) ? (minCurrentRow) : (currentRow + 1);
            }
            else
            {
                currentRow = (currentRow - 1 < minCurrentRow) ? (maxCurrentRow - 1) : (currentRow - 1);
            }
            mainTexts[currentRow].color = orange;
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            sfxSource.clip = navigationAcceptSound;
            sfxSource.loop = false;
            sfxSource.Play();
            switch (currentState)
            {
                case 0: //currently in main view
                    mainTexts[currentRow].color = Color.white;
                    switch (currentRow)
                    {
                        case 0: //restart
                            currentState = 1;
                            PrintYesNo();
                            break;
                        case 1: //exit to main
                            currentState = 2;
                            PrintYesNo();
                            break;
                    }
                    mainTexts[currentRow].color = orange;
                    break;
                case 1: //load game slots - are you sure?
                    switch (currentRow)
                    {
                        case 0: //yes
                            SceneManager.LoadScene("world");
                            break;
                        case 1: //no
                            currentState = 0;
                            PrintYesNo();
                            mainTexts[currentRow].color = orange;
                            break;
                    }
                    break;
                case 2: //exit to main - are you sure?
                    switch (currentRow)
                    {
                        case 0: //yes
                            SceneManager.LoadScene("start");
                            break;
                        case 1: //no
                            currentState = 0;
                            PrintMainView();
                            mainTexts[currentRow].color = orange;
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
        mainTexts[0].color = orange;
        currentRow = minCurrentRow = 0;
        mainTexts[0].text = "PotwierdŸ";
        mainTexts[1].text = "Cofnij";
        maxCurrentRow = 2;
    }

    void PrintMainView()
    {
        minCurrentRow = 0;
        ClearTexts();
        mainTexts[0].text = "Powtórz";
        mainTexts[1].text = "WyjdŸ do menu";
        maxCurrentRow = 2;
    }

    void UpdateGuideText()
    {
        switch(currentState)
        {
            case 0: //main view
                switch (currentRow)
                {
                    case 0:
                        guideText.text = "Wczytaj od ostatniego punktu kontrolnego";
                        break;
                    case 1:
                        guideText.text = "Wróæ do ekranu startowego";
                        break;
                }
                break;
            case 1: //load game - are you sure?
                guideText.text = "Czy na pewno wczytaæ grê z ostatniego punktu kontrolnego?";
                break;
            case 2: //exit game - are you sure?
                guideText.text = "Czy na pewno chcesz wróciæ do ekranu startowego?";
                break;
        }
        
    }

    void ClearTexts()
    {
        mainTexts[currentRow].color = Color.white;
        foreach (var action in mainTexts)
        {
            action.text = "";
        }
        mainTexts[currentRow = 0].color = orange;
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
                guideText.text = "Data zapisu: " + date + ", postêp: " + (100f * currentMainQuest / 15) + "%";
            }
        }
    }
}
