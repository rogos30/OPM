using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;
using static UnityEngine.GraphicsBuffer;

public class BattleManager : MonoBehaviour
{
    Vector2 returnPosition, battlePosition = new Vector2(1000, 0);
    [SerializeField] GameObject player;

    public UnityEvent onBattleWon, onBattleLost;
    public UnityEvent onSkillCheckFinished, onMoveFinished;
    public static BattleManager instance;
    [SerializeField] Canvas battleCanvas;
    [SerializeField] Slider skillCheckSlider;
    [SerializeField] GameObject skillCheckBlueArea;
    [SerializeField] GameObject skillCheckGreenArea;
    [SerializeField] Image background;
    [SerializeField] GameObject[] enemySprites;
    [SerializeField] Sprite[] backgroundPhotos;
    [SerializeField] TMP_Text[] enemyNames;
    [SerializeField] TMP_Text[] characterNames;
    [SerializeField] GameObject nextCharacters;
    [SerializeField] TMP_Text[] charAnnouncementTexts;
    [SerializeField] Sprite[] effectSprites;
    [SerializeField] Image[] playerEffectSprites;
    [SerializeField] Image[] nextEffectSprites;
    [SerializeField] Image[] next1EffectSprites;
    [SerializeField] Image[] next2EffectSprites;
    [SerializeField] Image[] next3EffectSprites;
    [SerializeField] Image[] enemy1EffectSprites;
    [SerializeField] Image[] enemy2EffectSprites;
    [SerializeField] Image[] enemy3EffectSprites;
    [SerializeField] Image[] enemy4EffectSprites;
    [SerializeField] Image[] playableCharactersSprites;
    [SerializeField] Slider[] enemyHealthBars;
    //[SerializeField] Slider[] targetHealthBars;
    [SerializeField] TMP_Text[] enemyHealthTexts;
    [SerializeField] Slider[] characterHealthBars;
    [SerializeField] Slider[] characterSkillBars;
    [SerializeField] TMP_Text[] characterHealthTexts;
    [SerializeField] TMP_Text[] characterSkillTexts;
    [SerializeField] TMP_Text[] actions;
    [SerializeField] TMP_Text dynamicDescriptionText;
    [SerializeField] GameObject actionDescriptionMenu;
    [SerializeField] GameObject dynamicDescription;
    [SerializeField] TMP_Text actionDescriptionText;
    [SerializeField] TMP_Text characterDescriptionText;
    public TMP_Text battleFpsText;

    public List<FriendlyCharacter> playableCharacters = new List<FriendlyCharacter>();
    public List<EnemyCharacter> allEnemyCharacters = new List<EnemyCharacter>();
    [NonSerialized] public List<int> currentPartyCharacters = new List<int>();

    List<FriendlyCharacter> playableCharacterList = new List<FriendlyCharacter>();
    List<EnemyCharacter> enemyCharacterList = new List<EnemyCharacter>();

    List<int> enemySpriteIndexes = new List<int>();

    const int maxCharactersInBattle = 5, iconsPerPlayable = 6;
    const int maxEnemiesInBattle = 4, iconsPerEnemy = 5;
    const int guardSpBoost = 20, skillCheckSliderWidth = 500;
    const float defaultSkillCheckTime = 1.75f;

    [SerializeField] GameObject[] animationObjects;
    public int[,] randomEncounterEnemyIndexes = { { 0, 1, 2, 3 }, { 0, 1, 2, 3 }, { 0, 1, 2, 3 }, { 0, 1, 2, 3 } }; //1st act, 2nd act, 3rd act, underground

    Color orange = new Color(0.976f, 0.612f, 0.007f);
    int currentPlayable, currentEnemy;
    int currentRow, currentColumn, maxCurrentRow, currentPage, maxCurrentPage;
    int chosenAction, chosenSubaction, chosenTarget, currentMoveInTurn = 0, currentTurn = 0;
    int chosenSubactionPage;
    int currentPhase = 0;
    bool acceptsInput = false, enemyIsMoving = false, battleFinished = false, saveGameAfterBattle = false;
    bool skillCheckGoingRight, skillCheckAcceptsInput;
    int playablesKnockedOut = 0, enemiesKnockedOut = 0, uiIndexOffset = 0;
    int skillPerformance;
    int playerMovesThisTurn;
    float skillCheckTime = defaultSkillCheckTime, defaultBlueAreaScale, defaultGreenAreaScale;
    Vector3 defaultTextScale;
    Image[,] allEffectSprites = new Image[maxCharactersInBattle, iconsPerPlayable];
    Image[,] allEnemyEffectSprites = new Image[maxEnemiesInBattle, iconsPerEnemy];

    [SerializeField] AudioClip[] battleMusic;
    [SerializeField] AudioClip navigationScrollSound;
    [SerializeField] AudioClip navigationCancelSound;
    [SerializeField] AudioClip navigationAcceptSound;
    [SerializeField] AudioClip[] skillSounds;
    [SerializeField] AudioClip altBurzynskiMusic;
    [SerializeField] AudioClip[] midFightVoiceLines;
    AudioSource musicSource, sfxSource;
    public AudioMixerGroup musicMixerGroup, sfxMixerGroup;
    private void Awake()
    {
        background.gameObject.SetActive(false);
        instance = this;
        battleCanvas.enabled = false;
        InitializeFriendlyCharacters();
        InitializeEnemyCharacters();
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.outputAudioMixerGroup = musicMixerGroup;
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.outputAudioMixerGroup = sfxMixerGroup;
        defaultTextScale = characterSkillTexts[currentPlayable].transform.localScale;
        defaultBlueAreaScale = skillCheckBlueArea.transform.localScale.x;
        defaultGreenAreaScale = skillCheckGreenArea.transform.localScale.x;
        for (int i = 0; i < iconsPerPlayable; i++)
            allEffectSprites[0, i] = playerEffectSprites[i];
        for (int i = 0; i < iconsPerPlayable; i++)
            allEffectSprites[1, i] = nextEffectSprites[i];
        for (int i = 0; i < iconsPerPlayable; i++)
            allEffectSprites[2, i] = next1EffectSprites[i];
        for (int i = 0; i < iconsPerPlayable; i++)
            allEffectSprites[3, i] = next2EffectSprites[i];
        for (int i = 0; i < iconsPerPlayable; i++)
            allEffectSprites[4, i] = next3EffectSprites[i];

        for (int i = 0; i < iconsPerEnemy; i++)
            allEnemyEffectSprites[0, i] = enemy1EffectSprites[i];
        for (int i = 0; i < iconsPerEnemy; i++)
            allEnemyEffectSprites[1, i] = enemy2EffectSprites[i];
        for (int i = 0; i < iconsPerEnemy; i++)
            allEnemyEffectSprites[2, i] = enemy3EffectSprites[i];
        for (int i = 0; i < iconsPerEnemy; i++)
            allEnemyEffectSprites[3, i] = enemy4EffectSprites[i];
    }

    private void Update()
    {
        if (battleCanvas.enabled)
        {
            if (skillCheckAcceptsInput)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    skillCheckAcceptsInput = false;
                }
            }
            if (acceptsInput)
            {
                HandleInput();
            }
            else if (enemyIsMoving)
            {
                HandleEnemysMove();
                enemyIsMoving = false;
            }
            GameManager.instance.CountFPS();
            HandleIndependentInput();
        }
    }

    void HandleIndependentInput()
    {
        if (Input.GetKey(KeyCode.Tab) && playableCharacterList.Count > 1)
        {
            nextCharacters.SetActive(true);
            characterDescriptionText.transform.parent.gameObject.SetActive(false);
            actionDescriptionText.transform.parent.gameObject.SetActive(false);
            //characterDescriptionText.gameObject.SetActive(false);
            //actionDescriptionText.gameObject.SetActive(false);

            //characterDescriptionText.text = playableCharacterList[Mathf.Min(currentPlayable, playableCharacterList.Count-1)].AbilityDescription;
        }
        else
        {
            nextCharacters.SetActive(false);
            //characterDescriptionText.gameObject.SetActive(true);
            //actionDescriptionText.gameObject.SetActive(true);
            characterDescriptionText.transform.parent.gameObject.SetActive(true);
            actionDescriptionText.transform.parent.gameObject.SetActive(true);
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Return))
        {
            sfxSource.clip = navigationAcceptSound;
            sfxSource.loop = false;
            sfxSource.Play();
            switch (currentColumn)
            {
                case 0: //just selected an action - entering subactions
                    currentColumn++;
                    chosenAction = currentRow;
                    currentPage = 0;
                    switch (chosenAction)
                    { //what action has been selected
                        case 0: //skill
                            PrintPageOfSkills();
                            break;
                        case 1: //item
                            PrintPageOfItems();
                            break;
                        default: //guard or run
                            PrintYesNo();
                            break;
                    }
                    ResetCurrentRow();
                    break;
                case 1: //just selected a subaction - entering targets (most likely)
                    chosenSubaction = currentRow;
                    chosenSubactionPage = currentPage;
                    currentPage = 0;
                    switch (chosenAction)
                    { //what action has been selected
                        case 0: //skill
                            if ((playableCharacterList[currentPlayable].skillSet[chosenSubactionPage * actions.Length + chosenSubaction].Cost > playableCharacterList[currentPlayable].Skill &&
                                playableCharacterList[currentPlayable].skillSet[chosenSubactionPage * actions.Length + chosenSubaction].Cost > 1) ||
                                ((int)(playableCharacterList[currentPlayable].skillSet[chosenSubactionPage * actions.Length + chosenSubaction].Cost * playableCharacterList[currentPlayable].MaxSkill) > playableCharacterList[currentPlayable].Skill &&
                                playableCharacterList[currentPlayable].skillSet[chosenSubactionPage * actions.Length + chosenSubaction].Cost <= 1))
                            { //too expensive to use
                                StartCoroutine(TooLittleSkillToUse());
                            }
                            else if (playableCharacterList[currentPlayable].skillSet[chosenSubactionPage * actions.Length + chosenSubaction].TargetIsFriendly)
                            {
                                if (playableCharacterList[currentPlayable].skillSet[chosenSubactionPage * actions.Length + chosenSubaction].MultipleTargets)
                                { //every ally is a target
                                    ClearActions();
                                    maxCurrentRow = 1;
                                    maxCurrentPage = 1;
                                    actions[0].text = "Wszyscy";
                                    currentColumn++;
                                }
                                else if (playableCharacterList[currentPlayable].skillSet[chosenSubactionPage * actions.Length + chosenSubaction].TargetIsRandom)
                                {
                                    ClearActions();
                                    maxCurrentRow = 1;
                                    maxCurrentPage = 1;
                                    actions[0].text = "Losowo";
                                    currentColumn++;
                                }
                                else
                                { //a singular alive ally is a target
                                    PrintPageOfAllies(true);
                                    currentColumn++;
                                }
                            }
                            else if (playableCharacterList[currentPlayable].skillSet[chosenSubactionPage * actions.Length + chosenSubaction].TargetIsSelf)
                            { //target is self
                                ClearActions();
                                maxCurrentRow = 1;
                                maxCurrentPage = 1;
                                actions[0].text = playableCharacterList[currentPlayable].NominativeName;
                                currentColumn++;
                            }
                            else
                            { //targets are alive enemies
                                if (playableCharacterList[currentPlayable].skillSet[chosenSubactionPage * actions.Length + chosenSubaction].MultipleTargets)
                                { //every enemy is a target
                                    ClearActions();
                                    maxCurrentRow = 1;
                                    maxCurrentPage = 1;
                                    actions[0].text = "Wszyscy";
                                }
                                else if (playableCharacterList[currentPlayable].skillSet[chosenSubactionPage * actions.Length + chosenSubaction].TargetIsRandom)
                                { //random enemy is a target
                                    ClearActions();
                                    maxCurrentRow = 1;
                                    maxCurrentPage = 1;
                                    actions[0].text = "Losowo";
                                }
                                else
                                { //one enemy is a target
                                    PrintPageOfEnemies();
                                }
                                currentColumn++;
                            }
                            break;
                        case 1: //item
                            if (Inventory.Instance.items[chosenSubactionPage * actions.Length + chosenSubaction].Amount > 0)
                            {
                                if (chosenSubaction == 2) //resurrects
                                {
                                    PrintPageOfAllies(false);
                                }
                                else
                                {
                                    PrintPageOfAllies(true);
                                }
                                currentColumn++;
                            }
                            else
                            {
                                currentPage = chosenSubactionPage;
                                actionDescriptionText.text = Inventory.Instance.items[currentPage * actions.Length + currentRow].Description;
                            }
                            break;
                        case 2: //guard
                            if (actions[chosenSubaction].text == "PotwierdŸ") //accepted to guard
                            {
                                HandlePlayersMove();
                                maxCurrentRow = 4;
                            }
                            else //didn't guard
                            {
                                currentColumn--;
                                maxCurrentRow = 4;
                            }
                            break;
                        case 3: //run
                            if (actions[chosenSubaction].text == "PotwierdŸ") //accepted to run
                            {
                                StartCoroutine(FinishBattle(false, true));
                                //HandleBattleEnd(false);
                            }
                            else //didn't run
                            {
                                currentColumn--;
                                maxCurrentRow = 4;
                            }
                            break;
                    }
                    ResetCurrentRow();
                    break;
                case 2: //just selected a target
                    chosenTarget = enemyCharacterList.FindIndex(x => x.NominativeName.Equals(actions[currentRow].text));
                    if (chosenTarget == -1)
                    { //if not an enemy, then an ally (or all enemies which does not matter)
                        chosenTarget = playableCharacterList.FindIndex(x => x.NominativeName.Equals(actions[currentRow].text));
                    }

                    currentColumn++;
                    HandlePlayersMove();
                    maxCurrentRow = 4;
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Escape))
        {
            sfxSource.clip = navigationCancelSound;
            sfxSource.loop = false;
            sfxSource.Play();
            ResetCurrentRow();
            switch (currentColumn)
            {
                case 1: //backed out of subactions
                    PrintPageOfActions();
                    switch (currentRow)
                    {
                        case 0: //skill
                            actionDescriptionText.text = "U¿yj umiejêtnoœci";
                            break;
                        case 1: //item
                            actionDescriptionText.text = "U¿yj przedmiotu";
                            break;
                        case 2: //guard
                            actionDescriptionText.text = "Otrzymuj mniej obra¿eñ, wiêcej leczenia i " + guardSpBoost + "% SP";
                            break;
                        case 3: //run
                            actionDescriptionText.text = "Ucieknij z walki";
                            break;
                    }
                    break;
                case 2: //backed out of targets
                    currentPage = chosenSubactionPage;
                    switch (chosenAction)
                    {
                        case 0: //skill
                            PrintPageOfSkills();
                            break;
                        case 1: //item
                            PrintPageOfItems();
                            break;
                    }
                    break;
            }
            currentColumn = Mathf.Max(currentColumn - 1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            sfxSource.clip = navigationScrollSound;
            sfxSource.loop = false;
            sfxSource.Play();
            actions[currentRow].color = Color.white;
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                currentRow = (currentRow + 1) % maxCurrentRow;
            }
            else
            {
                currentRow = (currentRow - 1 < 0) ? (maxCurrentRow - 1) : (currentRow - 1);
            }
            actions[currentRow].color = orange;
            switch (currentColumn)
            {
                case 0: //actions
                    switch (currentRow)
                    {
                        case 0: //skill
                            actionDescriptionText.text = "U¿yj umiejêtnoœci";
                            break;
                        case 1: //item
                            actionDescriptionText.text = "U¿yj przedmiotu";
                            break;
                        case 2: //guard
                            actionDescriptionText.text = "Otrzymuj mniej obra¿eñ, wiêcej leczenia i " + guardSpBoost + "% SP";
                            break;
                        case 3: //run
                            actionDescriptionText.text = "Ucieknij z walki";
                            break;
                    }
                    break;
                case 1: //subactions
                    switch (chosenAction)
                    {
                        case 0: //skill
                            actionDescriptionText.text = playableCharacterList[currentPlayable].NominativeName + " " +
                            playableCharacterList[currentPlayable].skillSet[currentPage * actions.Length + currentRow].SkillDescription;
                            break;
                        case 1: //item
                            actionDescriptionText.text = Inventory.Instance.items[currentPage * actions.Length + currentRow].Description;
                            break;
                        default:
                            actionDescriptionText.text = "";
                            break;
                    }
                    break;
                case 2: //targets
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {
            sfxSource.clip = navigationScrollSound;
            sfxSource.loop = false;
            sfxSource.Play();
            ResetCurrentRow();
            if (Input.GetKeyDown(KeyCode.Q))
            {
                currentPage = Mathf.Max(currentPage - 1, 0);
            }
            else
            {
                currentPage = Mathf.Min(currentPage + 1, maxCurrentPage - 1);
            }
            switch (currentColumn)
            {
                case 1: //subactions
                    if (chosenAction == 0) //skill
                    {
                        PrintPageOfSkills();
                    }
                    else if (chosenAction == 1) //item
                    {
                        PrintPageOfItems();
                    }
                    break;
                case 2: //targets
                    if (chosenAction == 0) //skill
                    {
                        if (playableCharacterList[currentPlayable].skillSet[chosenSubactionPage * actions.Length + chosenSubaction].TargetIsRandom || playableCharacterList[currentPlayable].skillSet[chosenSubactionPage * actions.Length + chosenSubaction].MultipleTargets)
                        {

                        }
                        else if (playableCharacterList[currentPlayable].skillSet[chosenSubactionPage * actions.Length + chosenSubaction].TargetIsFriendly)
                        {
                            PrintPageOfAllies(true);
                        }
                        else
                        {
                            PrintPageOfEnemies();
                        }
                    }
                    else //item
                    {
                        if (Inventory.Instance.items[chosenSubactionPage * actions.Length + chosenSubaction].Resurrects)
                        {
                            PrintPageOfAllies(false);
                        }
                        else
                        {
                            PrintPageOfAllies(true);
                        }
                    }
                    break;
            }
        }
    }

    void RidUIofColor()
    {
        for (int i = 0; i < actions.Length; i++) //make everything white
        {
            actions[i].color = Color.white;
        }
    }

    void PrintPageOfActions()
    {
        actions[0].text = "Umiejêtnoœæ";
        actions[1].text = "Przedmiot";
        actions[2].text = "Garda";
        actions[3].text = "Ucieczka";
        maxCurrentRow = actions.Length;
        maxCurrentPage = 1;
    }
    void PrintPageOfSkills()
    {
        ClearActions();
        maxCurrentPage = (playableCharacterList[currentPlayable].UnlockedSkills - 1) / actions.Length + 1;
        maxCurrentRow = Mathf.Min(playableCharacterList[currentPlayable].UnlockedSkills - currentPage * actions.Length, actions.Length);
        if (playableCharacterList[currentPlayable].skillSet.Count == 1) //rogos vs franek override
        {
            maxCurrentPage = 1;
            maxCurrentRow = 1;
        }
        for (int i = 0; i < actions.Length; i++)
        {
            if (currentPage * actions.Length + i < playableCharacterList[currentPlayable].UnlockedSkills)
            {
                float cost = 0;
                if (playableCharacterList[currentPlayable].skillSet[currentPage * actions.Length + i].Cost > 1)
                {
                    cost = playableCharacterList[currentPlayable].skillSet[currentPage * actions.Length + i].Cost;
                }
                else
                {
                    cost = (int)(playableCharacterList[currentPlayable].skillSet[currentPage * actions.Length + i].Cost * playableCharacterList[currentPlayable].MaxSkill);
                }
                actions[i].text = playableCharacterList[currentPlayable].skillSet[currentPage * actions.Length + i].Name + " (" + cost + ")";
            }
            else
            {
                actions[i].text = "";
            }
        }
        actionDescriptionText.text = playableCharacterList[currentPlayable].NominativeName + " " + playableCharacterList[currentPlayable].skillSet[currentPage * actions.Length].SkillDescription;
    }

    void ResetCurrentRow()
    {
        actions[currentRow].color = Color.white;
        currentRow = 0;
        actions[currentRow].color = orange;
    }

    void PrintPageOfItems()
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].text = Inventory.Instance.items[currentPage * actions.Length + i].Name + " ("
                + Inventory.Instance.items[currentPage * actions.Length + i].Amount + ")"; ;
        }
        maxCurrentRow = actions.Length;
        maxCurrentPage = ShopManager.instance.level + 1;
        actionDescriptionText.text = Inventory.Instance.items[currentPage * actions.Length].Description;
    }
    void PrintYesNo()
    {
        ClearActions();
        actions[0].text = "PotwierdŸ";
        actions[1].text = "Cofnij";
        maxCurrentRow = 2;
        maxCurrentPage = 1;
    }
    void ClearActions()
    {
        foreach (var action in actions)
        {
            action.text = "";
        }
    }
    void UpdateHealthBarsAndIcons()
    {
        for (int i = 0; i < playableCharacterList.Count; i++)
        { //showing status of players
            int index = (i + currentPlayable - uiIndexOffset) % playableCharacterList.Count;
            if (index < 0) index = playableCharacterList.Count + index;
            characterHealthBars[i].value = (float)(playableCharacterList[index].Health / (float)playableCharacterList[index].MaxHealth);
            characterSkillBars[i].value = (float)(playableCharacterList[index].Skill / (float)playableCharacterList[index].MaxSkill);
            characterHealthTexts[i].text = playableCharacterList[index].Health + " / " + playableCharacterList[index].MaxHealth;
            characterSkillTexts[i].text = playableCharacterList[index].Skill + " / " + playableCharacterList[index].MaxSkill;
        }
        for (int i = 0; i < enemyCharacterList.Count; i++)
        { //showing status of enemies
            enemyHealthBars[i].value = (float)(enemyCharacterList[i].Health / (float)enemyCharacterList[i].MaxHealth);
            enemyHealthTexts[i].text = enemyCharacterList[i].Health + " / " + enemyCharacterList[i].MaxHealth;
            for (int j = 0; j < enemyCharacterList[i].StatusTimers.Length; j++)
            {
                int type = 10 - 2 * j;
                if (enemyCharacterList[i].StatusTimers[j] < 0)
                {
                    type = 1;
                }
                else if (enemyCharacterList[i].StatusTimers[j] > 0)
                {
                    type = 0;
                }
                allEnemyEffectSprites[i, j].sprite = effectSprites[2 * j + 2 + type];
            }
        }

        for (int j = 0; j < playableCharacterList.Count; j++)
        {
            int index = (j + currentPlayable - uiIndexOffset) % playableCharacterList.Count;
            if (index < 0) index = playableCharacterList.Count + index;
            if (playableCharacterList[index].IsGuarding)
            {
                allEffectSprites[j, 0].sprite = effectSprites[0];
            }
            else if (playableCharacterList[index].KnockedOut)
            {
                allEffectSprites[j, 0].sprite = effectSprites[1];
            }
            else
            {
                allEffectSprites[j, 0].sprite = effectSprites[12];
            }
            for (int i = 0; i < playableCharacterList[index].StatusTimers.Length; i++)
            {
                int type = 10 - 2 * i;
                if (playableCharacterList[index].StatusTimers[i] < 0)
                {
                    type = 1;
                }
                else if (playableCharacterList[index].StatusTimers[i] > 0)
                {
                    type = 0;
                }

                allEffectSprites[j, i + 1].sprite = effectSprites[2 * i + 2 + type];
            }
        }
    }

    void PrintPageOfAllies(bool alive)
    {
        ClearActions();
        int skipped = 0, toSkip = currentPage * actions.Length, textIndex = 0;
        for (int i = 0; i < playableCharacterList.Count; i++)
        {
            if (playableCharacterList[i].KnockedOut != alive)
            {
                if (skipped < toSkip)
                {
                    skipped++;
                }
                else if (textIndex < actions.Length)
                {
                    actions[textIndex++].text = playableCharacterList[i].NominativeName;
                }
            }
        }
        maxCurrentRow = textIndex;
        maxCurrentPage = (playableCharacterList.Count - 1) / actions.Length + 1;
    }

    void PrintPageOfEnemies()
    {
        ClearActions();
        int skipped = 0, toSkip = currentPage * actions.Length, textIndex = 0;
        for (int i = 0; i < enemyCharacterList.Count; i++)
        {
            if (!enemyCharacterList[i].KnockedOut)
            {
                if (skipped < toSkip)
                {
                    skipped++;
                }
                else
                {
                    actions[textIndex++].text = enemyCharacterList[i].NominativeName;
                }
            }
        }
        maxCurrentRow = textIndex;
        maxCurrentPage = toSkip / actions.Length + 1;
    }
    void HandlePlayersMove()
    {
        uiIndexOffset = 0;
        acceptsInput = false;
        if (chosenAction == 2)
        { //guard
            dynamicDescription.SetActive(true);
            actionDescriptionMenu.SetActive(false);
            dynamicDescriptionText.text = playableCharacterList[currentPlayable].NominativeName + " broni siê!";
            playableCharacterList[currentPlayable].StartGuard();
            animationObjects[animationObjects.Length - 1].GetComponent<Animator>().SetInteger("animation", 2);
            StartCoroutine(disableAnimation(animationObjects.Length - 1));
            UpdateHealthBarsAndIcons();
            FinishPlayersMove();
        }
        else if (chosenAction == 1)
        { //item
            dynamicDescription.SetActive(true);
            actionDescriptionMenu.SetActive(false);
            dynamicDescriptionText.text = Inventory.Instance.items[chosenSubactionPage * actions.Length + chosenSubaction].Use(playableCharacterList[currentPlayable], playableCharacterList[chosenTarget]);
            animationObjects[animationObjects.Length - 1].GetComponent<Animator>().SetInteger("animation", 5);
            StartCoroutine(disableAnimation(animationObjects.Length - 1));
            UpdateHealthBarsAndIcons();
            FinishPlayersMove();
        }
        else // skill
        {
            int skillIndex = chosenSubactionPage * actions.Length + chosenSubaction;
            HandleSkillCheck(currentPlayable, skillIndex);
            onSkillCheckFinished.AddListener(PerformPlayersMove);
        }
    }

    void PerformPlayersMove()
    {
        onSkillCheckFinished.RemoveAllListeners();
        int skillIndex = chosenSubactionPage * actions.Length + chosenSubaction;
        float cost = playableCharacterList[currentPlayable].skillSet[skillIndex].Cost;
        dynamicDescription.SetActive(true);
        actionDescriptionMenu.SetActive(false);
        if (cost > 1 || cost == 0)
        {
            playableCharacterList[currentPlayable].DepleteSkill((int)cost);
        }
        else
        {
            playableCharacterList[currentPlayable].DepleteSkill(cost);
        }
        onMoveFinished.AddListener(FinishPlayersMove);
        if (playableCharacterList[currentPlayable].skillSet[skillIndex].TargetIsFriendly || playableCharacterList[currentPlayable].skillSet[skillIndex].TargetIsSelf)
        {
            if (playableCharacterList[currentPlayable].skillSet[skillIndex].MultipleTargets)
            { //skill targets every ally
                StartCoroutine(FriendlyExecuteSkillOnEveryone(playableCharacterList[currentPlayable], skillIndex, playableCharacterList.Cast<Character>().ToList(), true));
            }
            else if (playableCharacterList[currentPlayable].skillSet[skillIndex].Repetitions > 1)
            { //skill targets random allies multiple times
                StartCoroutine(FriendlyExecuteSkillMultipleTimes(playableCharacterList[currentPlayable], skillIndex, playableCharacterList.Cast<Character>().ToList()));
            }
            else
            { //skill targets one ally
                FriendlyExecuteSkill(playableCharacterList[currentPlayable], skillIndex, playableCharacterList[chosenTarget], true);
            }
        }
        else
        {
            if (playableCharacterList[currentPlayable].skillSet[skillIndex].MultipleTargets)
            { //skill targets every enemy
                StartCoroutine(FriendlyExecuteSkillOnEveryone(playableCharacterList[currentPlayable], skillIndex, enemyCharacterList.Cast<Character>().ToList(), false));
            }
            else if (playableCharacterList[currentPlayable].skillSet[skillIndex].Repetitions > 1)
            { //skill targets random enemies multiple times
                StartCoroutine(FriendlyExecuteSkillMultipleTimes(playableCharacterList[currentPlayable], skillIndex, enemyCharacterList.Cast<Character>().ToList()));
            }
            else
            { //skill targets one enemy
                if (chosenTarget != -1)
                {
                    FriendlyExecuteSkill(playableCharacterList[currentPlayable], skillIndex, enemyCharacterList[chosenTarget], false);
                }
                else
                {
                    FriendlyExecuteSkill(playableCharacterList[currentPlayable], skillIndex, enemyCharacterList[ChooseRandomTarget(enemyCharacterList.Cast<Character>().ToList())], false);
                }
            }
        }
    }


    void FindAvailableToMove()
    {
        while (currentPlayable < playableCharacterList.Count && (playableCharacterList[currentPlayable].KnockedOut || playableCharacterList[currentPlayable].Turns <= 0))
        { //find next playable that can move or go beyond the list
            Debug.Log(currentPlayable + " " + playableCharacterList[currentPlayable].NominativeName + " ma ruchow: " + playableCharacterList[currentPlayable].Turns);
            if (playableCharacterList[currentPlayable].Turns <= 0)
            {
                playableCharacterList[currentPlayable].HandlePersistentStatusEffects();
                playableCharacterList[currentPlayable].HandleTimers();
            }
            currentPlayable++;
            uiIndexOffset++;
            if (currentPlayable < playableCharacterList.Count) RotatePlayables();
        }
        while (currentEnemy < enemyCharacterList.Count && enemyCharacterList[currentEnemy].KnockedOut)
        {//find next enemy that can move or go beyond the list
            currentEnemy++;
        }
        if (currentPlayable == playableCharacterList.Count && currentEnemy == enemyCharacterList.Count)
        {
            currentPlayable = 0;
            currentEnemy = 0;
            currentTurn++;
            FindAvailableToMove();
        }
    }

    void DecideNextMove()
    {
        CountKnockedOut();
        if (playablesKnockedOut == playableCharacterList.Count && !battleFinished)
        { //player lost
            StartCoroutine(FinishBattle(false, false));
            //HandleBattleEnd(false);
        }
        else if (enemiesKnockedOut == enemyCharacterList.Count && !battleFinished)
        { //enemy lost
            StartCoroutine(FinishBattle(true, false));
            //HandleBattleEnd(true);
        }
        if (currentPlayable == playableCharacterList.Count ||
            (currentEnemy < enemyCharacterList.Count && playableCharacterList[currentPlayable].Speed < enemyCharacterList[currentEnemy].Speed))
        { //enemy moves
            StartCoroutine(AllowEnemyToMove());
        }
        if (currentEnemy == enemyCharacterList.Count ||
            (currentPlayable < playableCharacterList.Count && playableCharacterList[currentPlayable].Speed > enemyCharacterList[currentEnemy].Speed))
        { //player moves
            StartCoroutine(AllowPlayerToMove(true));
        }
    }

    void HandleEnemysMove()
    {
        enemyCharacterList[currentEnemy].HandlePersistentStatusEffects();
        enemyCharacterList[currentEnemy].HandleTimers();
        Debug.Log("Handling " + enemyCharacterList[currentEnemy].NominativeName + " move");
        if (enemyCharacterList[currentEnemy].Turns == 0)
        {
            EnemyIsParalyzed(enemyCharacterList[currentEnemy]);
            return;
        }
        PerformEnemysMove(enemyCharacterList[currentEnemy]);
    }

    void EnemyIsParalyzed(Character source)
    {
        dynamicDescriptionText.text = source.NominativeName + " nie mo¿e siê ruszyæ!";
        FinishEnemysMove();
    }

    void PerformEnemysMove(EnemyCharacter source)
    {
        int randSkill = UnityEngine.Random.Range(0, source.skillSet.Count);
        int randTarget;
        onMoveFinished.AddListener(FinishEnemysMove);
        if (source.skillSet[randSkill].TargetIsFriendly)
        { //targets are alive enemies
            if (source.skillSet[randSkill].MultipleTargets)
            { //skill targets all enemies
                StartCoroutine(EnemyExecuteSkillOnEveryone(source, randSkill, enemyCharacterList.Cast<Character>().ToList()));
            }
            else
            { //skill targets a single enemy
                randTarget = ChooseRandomTarget(enemyCharacterList.Cast<Character>().ToList());
                if (randTarget == -1)
                {
                    StartCoroutine(FinishBattle(false, false));
                    //HandleBattleEnd(false);
                    return;
                }
                EnemyExecuteSkill(source, randSkill, enemyCharacterList[randTarget]);
            }
        }
        else if (source.skillSet[randSkill].TargetIsSelf)
        {
            EnemyExecuteSkill(source, randSkill, source);
        }
        else
        { //targets are playables
            randTarget = ChooseRandomTarget(playableCharacterList.Cast<Character>().ToList());
            if (randTarget == -1)
            {
                StartCoroutine(FinishBattle(false, false));
                //HandleBattleEnd(false);
                return;
            }
            if (source.skillSet[randSkill].MultipleTargets)
            { //skill targets every playable
                StartCoroutine(EnemyExecuteSkillOnEveryone(source, randSkill, playableCharacterList.Cast<Character>().ToList()));
            }
            else if (source.skillSet[randSkill].Repetitions > 1)
            { //skill targets random playables multiple times
                StartCoroutine(EnemyExecuteSkillMultipleTimes(source, randSkill, playableCharacterList.Cast<Character>().ToList()));
            }
            else
            { //skill targets one playable
                EnemyExecuteSkill(source, randSkill, playableCharacterList[randTarget]);
            }
        }
    }

    public void InitiateBattle(int[] playables, int[] enemies, int backgroundId, bool saveGameAfterBattle)
    {
        background.gameObject.SetActive(true);
        background.sprite = backgroundPhotos[backgroundId];
        enemySpriteIndexes.AddRange(enemies);
        this.saveGameAfterBattle = saveGameAfterBattle;
        musicSource.clip = battleMusic[enemies[0]];
        musicSource.loop = true;
        musicSource.Play();
        skillCheckSlider.gameObject.SetActive(false);
        actionDescriptionMenu.SetActive(true);
        dynamicDescription.SetActive(false);
        acceptsInput = true;
        enemyIsMoving = false;
        currentEnemy = 0; currentPlayable = 0; currentRow = 0; currentColumn = 0; currentPage = 0;
        currentPhase = 0; currentTurn = 0; uiIndexOffset = 0; skillPerformance = 0;
        skillCheckTime = defaultSkillCheckTime - 0.25f * GameManager.instance.difficulty;
        battleFinished = false;
        battleCanvas.enabled = true;
        GameManager.instance.inGameCanvas.enabled = false;
        returnPosition = player.transform.position;
        player.transform.position = battlePosition;
        player.SetActive(false);
        StoryManager.instance.DisableAllNPCs();
        RidUIofColor();
        for (int i = 0; i < characterNames.Length; i++) //clear everything up
        {
            characterNames[i].text = "";
            characterHealthBars[i].gameObject.SetActive(false);
            characterSkillBars[i].gameObject.SetActive(false);
            for (int j = 0; j < iconsPerPlayable; j++)
            {
                allEffectSprites[i, j].sprite = effectSprites[12];
            }
        }

        foreach (Image sprite in playableCharactersSprites)
        {
            sprite.sprite = effectSprites[12];
        }
        for (int i = 0; i < enemyNames.Length; i++)
        {
            enemyHealthBars[i].gameObject.SetActive(false);
            enemyNames[i].text = "";
            for (int j = 0; j < iconsPerEnemy; j++)
            {
                allEnemyEffectSprites[i, j].sprite = effectSprites[12];
            }
        }
        PrintPageOfActions();
        actionDescriptionText.text = "U¿yj umiejêtnoœci";
        actions[0].color = orange;

        for (int i = 0; i < playables.Length; i++) //show what's necessary
        {
            playableCharacters[playables[i]].Reset();
            playableCharacterList.Add(playableCharacters[playables[i]]);
            characterNames[i].text = playableCharacterList[i].NominativeName;
            characterHealthBars[i].gameObject.SetActive(true);
            characterSkillBars[i].gameObject.SetActive(true);
            playableCharactersSprites[i].sprite = DialogManager.instance.speakerSprites[playableCharacterList[i].SpriteIndex];
        }
        for (int i = 0; i < enemies.Length; i++)
        {
            allEnemyCharacters[enemies[i]].Reset();
            enemyCharacterList.Add(allEnemyCharacters[enemies[i]]);
            enemySprites[enemies[i]].SetActive(true);
            enemyNames[i].text = enemyCharacterList[i].NominativeName;
            enemyHealthBars[i].gameObject.SetActive(true);
        }
        UpdateHealthBarsAndIcons();
        characterDescriptionText.text = playableCharacterList[0].AbilityDescription;
        playerMovesThisTurn = playableCharacterList[0].DefaultTurns;
    }

    void HandleBattleEnd(bool playerWon, bool playerEscaped)
    {
        battleFinished = true;
        acceptsInput = false;
        List<string> gameInfoLines = new List<string>();
        if (playerWon)
        {
            int xpEarned = 0, moneyEarned = 0;
            for (int i = 0; i < enemyCharacterList.Count; i++)
            {
                xpEarned += enemyCharacterList[i].XPDropped;
                moneyEarned += enemyCharacterList[i].MoneyDropped;
            }
            gameInfoLines.Add("Zdobywasz " + xpEarned + " doœwiadczenia!");
            for (int i = 0; i < playableCharacterList.Count; i++)
            {
                gameInfoLines.AddRange(playableCharacterList[i].HandleLevel(xpEarned));
            }
            Inventory.Instance.Money += moneyEarned;
            gameInfoLines.Add("Zarabiasz " + moneyEarned + " PLN!");
            Debug.Log("Earned " + moneyEarned + " money. Now you have " + Inventory.Instance.Money + " money");

            DialogManager.instance.StartGameInfo(gameInfoLines.ToArray());
        }
        else
        {

        }
        DialogManager.instance.onGameInfoEnd.AddListener(() => StartCoroutine(FinishBattle(playerWon, playerEscaped)));
    }

    IEnumerator FinishBattle(bool playerWon, bool playerEscaped)
    {
        battleFinished = true;
        acceptsInput = false;
        List<string> gameInfoLines = new List<string>();

        yield return new WaitForSeconds(2);
        musicSource.Stop();
        foreach (var sprite in enemySprites)
        {
            sprite.SetActive(false);
        }

        background.gameObject.SetActive(false);
        player.SetActive(true);
        player.transform.position = returnPosition;
        StoryManager.instance.HandleAllNPCs();
        battleCanvas.enabled = false;
        if (playerWon)
        {
            int xpEarned = 0, moneyEarned = 0;
            for (int i = 0; i < enemyCharacterList.Count; i++)
            {
                xpEarned += enemyCharacterList[i].XPDropped;
                moneyEarned += enemyCharacterList[i].MoneyDropped;
            }
            gameInfoLines.Add("Zdobywasz " + xpEarned + " punktów doœwiadczenia!\n" + "Zarabiasz " + moneyEarned + " PLN!");
            for (int i = 0; i < playableCharacterList.Count; i++)
            {
                gameInfoLines.AddRange(playableCharacterList[i].HandleLevel(xpEarned));
            }
            Inventory.Instance.Money += moneyEarned;
            Debug.Log("Earned " + moneyEarned + " money. Now you have " + Inventory.Instance.Money + " money");

            DialogManager.instance.StartGameInfo(gameInfoLines.ToArray());
            if (saveGameAfterBattle)
            {
                GameManager.instance.SaveGame();
            }
        }
        else if (playerEscaped)
        {
            gameInfoLines.Add("ciota");
            DialogManager.instance.StartGameInfo(gameInfoLines.ToArray());
        }
        else
        {
            gameInfoLines.Add("gg wracasz do lobby");
            DialogManager.instance.StartGameInfo(gameInfoLines.ToArray());
        }
        playableCharacterList.Clear();
        enemyCharacterList.Clear();
        enemySpriteIndexes.Clear();
        StopAllCoroutines();
        DialogManager.instance.onGameInfoEnd.AddListener(() => {
            GameManager.instance.inGameCanvas.enabled = true;
            if (playerWon)
            {
                onBattleWon.Invoke();
            }
            else if (playerEscaped)
            {
                //onBattleLost.Invoke();
            }
            else
            {
                //onBattleLost.Invoke();
                SceneManager.LoadScene("start");
            }
        });
    }

    void InitializeFriendlyCharacters()
    {
        FriendlyCharacter character = new Rogos();
        playableCharacters.Add(character);
        currentPartyCharacters.Add(0);

        character = new Welenc();
        playableCharacters.Add(character);

        character = new Stasiak();
        playableCharacters.Add(character);

        character = new Maja();
        playableCharacters.Add(character);

        character = new Burzynski();
        playableCharacters.Add(character);

        character = new Lora();
        playableCharacters.Add(character);

        character = new Franek();
        playableCharacters.Add(character);
    }

    void InitializeEnemyCharacters()
    {
        EnemyCharacter character = new MiddleFingerKid();
        allEnemyCharacters.Add(character);

        character = new IndianKid();
        allEnemyCharacters.Add(character);

        character = new AngryGirl();
        allEnemyCharacters.Add(character);

        character = new OffendedKid();
        allEnemyCharacters.Add(character);

        character = new EnemySwietlik();
        allEnemyCharacters.Add(character);

        allEnemyCharacters.Add(character); //5, added twice to not change ids of enemies below

        character = new Monitoring();
        allEnemyCharacters.Add(character);

        character = new Camera1();
        allEnemyCharacters.Add(character);

        character = new Camera2();
        allEnemyCharacters.Add(character);

        character = new Aniela();
        allEnemyCharacters.Add(character);

        character = new StrongAniela(); //10
        allEnemyCharacters.Add(character);

        character = new Nina();
        allEnemyCharacters.Add(character);

        character = new Generator1();
        allEnemyCharacters.Add(character);

        character = new Generator2();
        allEnemyCharacters.Add(character);

        character = new Generator3();
        allEnemyCharacters.Add(character);

        character = new Server(); //15
        allEnemyCharacters.Add(character);

        character = new Peter();
        allEnemyCharacters.Add(character);

        character = new MainBuiler();
        allEnemyCharacters.Add(character);

        character = new Builder();
        allEnemyCharacters.Add(character);

        character = new BuilderB();
        allEnemyCharacters.Add(character);

        character = new BuilderC(); //20
        allEnemyCharacters.Add(character);

        character = new Skeleton1();
        allEnemyCharacters.Add(character);

        character = new Skeleton();
        allEnemyCharacters.Add(character);

        character = new EnemyBurzynski();
        allEnemyCharacters.Add(character);

        character = new EnemyFranek();
        allEnemyCharacters.Add(character);
    }

    void RotatePlayables()
    {
        if (playableCharacterList.Count == 1)
        {
            UpdateHealthBarsAndIcons();
            return;
        }
        for (int i = 0; i < playableCharacterList.Count; i++)
        {
            int index = (i + currentPlayable) % playableCharacterList.Count;
            characterNames[i].text = playableCharacterList[index].NominativeName;
            characterHealthBars[i].value = (float)(playableCharacterList[index].Health / (float)playableCharacterList[index].MaxHealth);
            characterSkillBars[i].value = (float)(playableCharacterList[index].Skill / (float)playableCharacterList[index].MaxSkill);
            characterHealthTexts[i].text = playableCharacterList[index].Health + " / " + playableCharacterList[index].MaxHealth;
            characterSkillTexts[i].text = playableCharacterList[index].Skill + " / " + playableCharacterList[index].MaxSkill;
            playableCharactersSprites[i].sprite = DialogManager.instance.speakerSprites[playableCharacterList[index].SpriteIndex];
            if (playableCharacterList[index].IsGuarding)
            {
                allEffectSprites[i, 0].sprite = effectSprites[0];
            }
            else if (playableCharacterList[index].KnockedOut)
            {
                allEffectSprites[i, 0].sprite = effectSprites[1];
            }
            else
            {
                allEffectSprites[i, 0].sprite = effectSprites[12];
            }

            for (int j = 0; j < 5; j++)
            {
                if (playableCharacterList[index].StatusTimers[j] == 0)
                {
                    allEffectSprites[i, j + 1].sprite = effectSprites[12];
                }
                else if (playableCharacterList[index].StatusTimers[j] < 0)
                {
                    allEffectSprites[i, j + 1].sprite = effectSprites[3 + j * 2];
                }
                else
                {
                    allEffectSprites[i, j + 1].sprite = effectSprites[2 + j * 2];
                }
            }
        }
        characterDescriptionText.text = playableCharacterList[currentPlayable].AbilityDescription;
    }


    IEnumerator TooLittleSkillToUse()
    {
        characterSkillTexts[0].color = Color.red;
        characterSkillTexts[0].transform.localScale = Vector3.one;

        yield return new WaitForSeconds(0.2f);

        characterSkillTexts[0].color = Color.white;
        characterSkillTexts[0].transform.localScale = defaultTextScale;
    }


    IEnumerator AllowPlayerToMove(bool rotatePlayables)
    {
        yield return new WaitForSeconds(4);
        foreach (var obj in animationObjects)
        {
            obj.GetComponent<Animator>().SetInteger("animation", 0);
        }
        if (rotatePlayables)
        {
            playableCharacterList[currentPlayable].HandlePersistentStatusEffects();
            playableCharacterList[currentPlayable].HandleTimers();
            RotatePlayables();
            Debug.Log("Now " + playableCharacterList[currentPlayable].NominativeName + " will move");
            playerMovesThisTurn = playableCharacterList[currentPlayable].Turns;
        }
        dynamicDescription.SetActive(false);
        actionDescriptionMenu.SetActive(true);
        acceptsInput = true;
        currentColumn = 0;
        RidUIofColor();
        actions[currentRow = 0].color = orange;
        actionDescriptionText.text = "U¿yj umiejêtnoœci";
        PrintPageOfActions();
        HandlePhases();
    }

    IEnumerator AllowEnemyToMove()
    {
        yield return new WaitForSeconds(5);
        foreach (var obj in animationObjects)
        {
            obj.GetComponent<Animator>().SetInteger("animation", 0);
        }
        enemyIsMoving = true;
        RidUIofColor();
    }

    IEnumerator FriendlyExecuteSkillOnEveryone(FriendlyCharacter source, int skill, List<Character> targets, bool targetIsFriendly)
    {
        sfxSource.clip = skillSounds[source.skillSet[skill].SkillSoundId];
        sfxSource.loop = false;
        sfxSource.Play();
        for (int i = 0; i < targets.Count; i++)
        {
            if (!targets[i].KnockedOut)
            {
                dynamicDescriptionText.text = source.skillSet[skill].execute(source, targets[i], skillPerformance);
                UpdateHealthBarsAndIcons();
                if (targetIsFriendly)
                {
                    animationObjects[animationObjects.Length - 1].GetComponent<Animator>().SetInteger("animation", source.skillSet[skill].AnimationId);
                    StartCoroutine(disableAnimation(animationObjects.Length - 1));
                }
                else
                {
                    animationObjects[i].GetComponent<Animator>().SetInteger("animation", source.skillSet[skill].AnimationId);
                    StartCoroutine(disableAnimation(i));
                }
                yield return new WaitForSeconds(1.5f / targets.Count);
            }
        }
        if (source is Swietlik)
        {
            ((Swietlik)source).ResetBetrayal();
        }
        else if (source is Welenc)
        {
            ((Welenc)source).IncreaseAttackMultiplier();
        }
        else if (source is Rogos)
        {
            if (source.skillSet[skill].Name == "Rozdanie kebabów")
            {
                ((ZahirTrip)source.skillSet[skill]).SetToMission();
            }
        }
            onMoveFinished.Invoke();
    }

    IEnumerator EnemyExecuteSkillOnEveryone(EnemyCharacter source, int skill, List<Character> targets)
    {
        sfxSource.clip = skillSounds[source.skillSet[skill].SkillSoundId];
        sfxSource.loop = false;
        sfxSource.Play();
        animationObjects[0].GetComponent<Animator>().SetInteger("animation", source.skillSet[skill].AnimationId);
        StartCoroutine(disableAnimation(0));
        for (int i = 0; i < targets.Count; i++)
        {
            if (!targets[i].KnockedOut)
            {
                dynamicDescriptionText.text = source.skillSet[skill].execute(source, targets[i]);
                UpdateHealthBarsAndIcons();
                yield return new WaitForSeconds(1.5f / targets.Count);
            }
        }
        onMoveFinished.Invoke();
    }

    IEnumerator FriendlyExecuteSkillMultipleTimes(FriendlyCharacter source, int skill, List<Character> targets)
    {
        sfxSource.clip = skillSounds[source.skillSet[skill].SkillSoundId];
        sfxSource.loop = false;
        sfxSource.Play();
        for (int i = 0; i < source.skillSet[skill].Repetitions; i++)
        {
            int target = ChooseRandomTarget(targets);
            if (target == -1)
            {
                break;
            }
            dynamicDescriptionText.text = source.skillSet[skill].execute(source, targets[target], skillPerformance);
            animationObjects[target].GetComponent<Animator>().SetInteger("animation", source.skillSet[skill].AnimationId);
            StartCoroutine(disableAnimation(target));
            UpdateHealthBarsAndIcons();
            yield return new WaitForSeconds(1.5f / source.skillSet[skill].Repetitions);
        }
        if (source is Swietlik)
        {
            ((Swietlik)source).ResetBetrayal();
        }
        else if (source is Welenc)
        {
            ((Welenc)source).IncreaseAttackMultiplier();
        }
        else if (source is Rogos)
        {
            if (source.skillSet[skill].Name == "Ostrza³ padami")
            {
                ((ControllerBarrage)source.skillSet[skill]).SetToUseless();
            }
        }
        onMoveFinished.Invoke();
    }

    IEnumerator EnemyExecuteSkillMultipleTimes(EnemyCharacter source, int skill, List<Character> targets)
    {
        sfxSource.clip = skillSounds[source.skillSet[skill].SkillSoundId];
        sfxSource.loop = false;
        sfxSource.Play();
        for (int i = 0; i < source.skillSet[skill].Repetitions; i++)
        {
            int target = ChooseRandomTarget(targets);
            if (target == -1)
            {
                break;
            }
            dynamicDescriptionText.text = source.skillSet[skill].execute(source, targets[target]);
            animationObjects[animationObjects.Length - 1].GetComponent<Animator>().SetInteger("animation", source.skillSet[skill].AnimationId);
            StartCoroutine(disableAnimation(animationObjects.Length - 1));
            UpdateHealthBarsAndIcons();
            yield return new WaitForSeconds(1.5f / source.skillSet[skill].Repetitions);
        }
        onMoveFinished.Invoke();
    }

    void FriendlyExecuteSkill(FriendlyCharacter source, int skill, Character target, bool targetIsFriendly)
    {
        sfxSource.clip = skillSounds[source.skillSet[skill].SkillSoundId];
        sfxSource.loop = false;
        sfxSource.Play();
        dynamicDescriptionText.text = source.skillSet[skill].execute(source, target, skillPerformance);
        if (chosenTarget == -1)
        {
            chosenTarget = enemyCharacterList.FindIndex(x => x.Equals(target));
        }
        int animationObject = chosenTarget;
        if (targetIsFriendly)
        {
            animationObject = 0;
        }
        animationObjects[animationObject].GetComponent<Animator>().SetInteger("animation", source.skillSet[skill].AnimationId);
        StartCoroutine(disableAnimation(animationObject));
        UpdateHealthBarsAndIcons();
        onMoveFinished.Invoke();
    }

    void EnemyExecuteSkill(EnemyCharacter source, int skill, Character target)
    {
        sfxSource.clip = skillSounds[source.skillSet[skill].SkillSoundId];
        sfxSource.loop = false;
        sfxSource.Play();
        dynamicDescriptionText.text = source.skillSet[skill].execute(source, target);
        animationObjects[animationObjects.Length - 1].GetComponent<Animator>().SetInteger("animation", source.skillSet[skill].AnimationId);
        StartCoroutine(disableAnimation(animationObjects.Length - 1));
        UpdateHealthBarsAndIcons();
        onMoveFinished.Invoke();
    }

    void FinishPlayersMove()
    {
        onMoveFinished.RemoveAllListeners();
        //if (currentMoveInTurn < playableCharacterList[currentPlayable].Turns - 1)
        CountKnockedOut();
        if (currentMoveInTurn < playerMovesThisTurn - 1 && playableCharacterList[currentPlayable].Turns > 0 && enemiesKnockedOut != enemyCharacterList.Count && playablesKnockedOut != playableCharacterList.Count)
        {
            currentMoveInTurn++;
            StartCoroutine(AllowPlayerToMove(false));
        }
        else
        {
            currentPlayable++;
            uiIndexOffset++;
            currentMoveInTurn = 0;
            FindAvailableToMove();
            DecideNextMove();
        }
    }

    void FinishEnemysMove()
    {
        onMoveFinished.RemoveAllListeners();
        CountKnockedOut();
        if (currentMoveInTurn < enemyCharacterList[currentEnemy].Turns - 1 && playableCharacterList[currentEnemy].Turns > 0 && enemiesKnockedOut != enemyCharacterList.Count && playablesKnockedOut != playableCharacterList.Count)
        {
            currentMoveInTurn++;
            StartCoroutine(AllowEnemyToMove());
        }
        else
        {
            currentMoveInTurn = 0;
            currentEnemy++;
            FindAvailableToMove();
            DecideNextMove();
        }
    }

    int ChooseRandomTarget(List<Character> targets)
    {
        int knockedOut = 0, result = -1;
        foreach (var target in targets)
        {
            if (target.KnockedOut)
            {
                knockedOut++;
            }
        }
        if (knockedOut == targets.Count)
        {
            return result;
        }
        while (result == -1)
        {
            result = UnityEngine.Random.Range(0, targets.Count);
            if (targets[result].KnockedOut)
            {
                result = -1;
            }
        }
        return result;
    }

    void AddEnemyCharactersToBattle(int[] enemies)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemySpriteIndexes.Add(enemies[i]);
            allEnemyCharacters[enemies[i]].Reset();
            enemyCharacterList.Add(allEnemyCharacters[enemies[i]]);
            enemySprites[enemies[i]].SetActive(true);
            enemyNames[enemyCharacterList.Count - 1].text = allEnemyCharacters[enemies[i]].NominativeName;
            enemyHealthBars[enemyCharacterList.Count - 1].gameObject.SetActive(true);
        }
    }

    void HandleSkillCheck(int currPlayable, int skillIndex)
    {
        int greenPosition = (int)UnityEngine.Random.Range(-skillCheckSliderWidth * (1 - skillCheckGreenArea.transform.localScale.x) / 2, skillCheckSliderWidth * (1 - skillCheckGreenArea.transform.localScale.x) / 2);
        int bluePosition = (int)UnityEngine.Random.Range(-skillCheckSliderWidth * (1 - skillCheckBlueArea.transform.localScale.x) / 2, skillCheckSliderWidth * (1 - skillCheckBlueArea.transform.localScale.x) / 2);
        Vector2 newPos = skillCheckGreenArea.transform.localPosition;
        Vector3 newScale = skillCheckBlueArea.transform.localScale;
        newPos.x = greenPosition;
        newScale.x = defaultGreenAreaScale * playableCharacterList[currPlayable].skillSet[skillIndex].AccuracyMultiplier * playableCharacterList[currPlayable].Accuracy;
        skillCheckGreenArea.transform.localPosition = newPos;
        skillCheckGreenArea.transform.localScale = newScale;
        newPos.x = bluePosition;
        newScale.x = defaultBlueAreaScale * playableCharacterList[currPlayable].skillSet[skillIndex].AccuracyMultiplier * playableCharacterList[currPlayable].Accuracy;
        skillCheckBlueArea.transform.localPosition = newPos;
        skillCheckBlueArea.transform.localScale = newScale;
        skillCheckSlider.value = 0;
        skillCheckSlider.gameObject.SetActive(true);
        skillCheckGoingRight = true;
        skillCheckAcceptsInput = true;
        StartCoroutine(PerformSkillCheck(greenPosition, bluePosition));
    }

    IEnumerator PerformSkillCheck(int greenPos, int bluePos)
    {
        for (int i = 0; i < 2 * skillCheckSlider.maxValue; i++)
        {
            if (skillCheckAcceptsInput)
            {
                if (skillCheckGoingRight)
                {
                    skillCheckSlider.value++;
                    if (skillCheckSlider.value == skillCheckSlider.maxValue)
                    {
                        skillCheckGoingRight = false;
                    }
                }
                else
                {
                    skillCheckSlider.value--;
                }
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(skillCheckTime / (2 * skillCheckSlider.maxValue));
        }
        yield return new WaitForSeconds(1);
        skillCheckSlider.gameObject.SetActive(false);
        skillCheckAcceptsInput = false;
        float value = -skillCheckSliderWidth / 2 + skillCheckSliderWidth * skillCheckSlider.value / skillCheckSlider.maxValue;
        //Debug.Log("value: " + value);
        //Debug.Log("green: " + (greenPos - skillCheckSliderWidth * skillCheckGreenArea.transform.localScale.x / 2) + " - " + (greenPos + skillCheckSliderWidth * skillCheckGreenArea.transform.localScale.x / 2));
        //Debug.Log("blue: " + (bluePos - skillCheckSliderWidth * skillCheckBlueArea.transform.localScale.x / 2) + " - " + (bluePos + skillCheckSliderWidth * skillCheckBlueArea.transform.localScale.x / 2));
        if (value >= greenPos - skillCheckSliderWidth * skillCheckGreenArea.transform.localScale.x / 2
            && value <= greenPos + skillCheckSliderWidth * skillCheckGreenArea.transform.localScale.x / 2)
        { //hit green
            skillPerformance = 2;
        }
        else if (value >= bluePos - skillCheckSliderWidth * skillCheckBlueArea.transform.localScale.x / 2
            && value <= bluePos + skillCheckSliderWidth * skillCheckBlueArea.transform.localScale.x / 2)
        { //hit blue
            skillPerformance = 1;
        }
        else
        {
            skillPerformance = 0;
        }
        onSkillCheckFinished.Invoke();
    }

    void HandlePhases()
    {
        Debug.Log("handling phases");
        switch (enemyCharacterList[0].NominativeName)
        {
            case "Monitoring":
                if ((float)enemyCharacterList[0].Health / enemyCharacterList[0].MaxHealth <= 0.75f && currentPhase == 0)
                {
                    currentPhase++;
                    int[] enemies = { 7 };
                    AddEnemyCharactersToBattle(enemies);
                    UpdateHealthBarsAndIcons();
                    DecideNextMove();
                }
                if ((float)enemyCharacterList[0].Health / enemyCharacterList[0].MaxHealth <= 0.5f && currentPhase == 1)
                {
                    currentPhase++;
                    int[] enemies = { 8 };
                    AddEnemyCharactersToBattle(enemies);
                    UpdateHealthBarsAndIcons();
                    DecideNextMove();
                }
                break;
            case "Burzyñski":
                if ((float)enemyCharacterList[0].Health / enemyCharacterList[0].MaxHealth <= 0.85f && currentPhase == 0)
                {
                    currentPhase++;
                    string[] lines = {
                        "MASZ JU¯ DOŒÆ?!",
                        "...",
                        "Co z wolnym po Nowym Roku?",
                        "O co chodzi³o z tym koncertem? GDZIE JEST LORA I GDZIE BOMBA?",
                        "..." };
                    int[] speakerIndexes = { 0,4,0,0,4 };
                    acceptsInput = false;
                    DialogManager.instance.StartDialogue(lines, speakerIndexes, midFightVoiceLines);
                    DialogManager.instance.onDialogueEnd.AddListener(() => {
                        acceptsInput = true;
                    });
                    
                }
                if (currentPhase > 0 && currentPhase <= 3)
                {
                    currentPhase++;
                }
                if (currentPhase == 4)
                {
                    currentPhase++;
                    string[] lines = {
                        "KTO ZA TYM STOI I GDZIE JEST?!",
                        "ODPOWIEDZ WRESZCIE!!!",
                        "OD KIEDY TO PLANOWALIŒCIE?!",
                        "G³upcy, wszystko w imiê wy¿szego celu. W imiê wy¿szej racji!",
                        "Jakiego celu? Jakiej racji?!",
                        "GDZIE...",
                        "JEST...",
                        "LORA?!",
                        "Zamknij siê wreszcie" };
                    int[] speakerIndexes = { 0, 0, 0, 4, 0, 0, 0, 0, 4 };
                    acceptsInput = false;
                    DialogManager.instance.StartDialogue(lines, speakerIndexes, midFightVoiceLines);
                    DialogManager.instance.onDialogueEnd.AddListener(() => {
                        acceptsInput = true;
                    });
                }
                if (currentPhase > 4 && currentPhase <= 6)
                {
                    currentPhase++;
                }
                if ((float)enemyCharacterList[0].Health / enemyCharacterList[0].MaxHealth <= 0.6f && currentPhase == 7)
                {
                    currentPhase++;
                    musicSource.clip = altBurzynskiMusic;
                    musicSource.loop = true;
                    musicSource.Play();
                    string[] lines = {
                        "Ach! Poddaj siê wreszcie!",
                        "Oboje wiemy, ¿e nic ju¿ nie zrobicie!",
                        "Mylisz siê, wszystko jest na najlepszej drodze. W imiê wy¿szych racji!",
                        "Przestañ siê oszukiwaæ, Kamil! Przypomnij sobie, ile razy oszukiwa³eœ te¿ mnie, kiedy byliœmy razem! Tyle obietnic, tyle k³amstw!",
                        "Jakich k³amstw, Maju? Zawsze chcia³em dla Ciebie jak najlepiej. Kocha³em Ciê, to ty nie potrafi³aœ tego odwzajemniæ! Nigdy Ciê nie oszuka³em" };
                    int[] speakerIndexes = { 3, 3, 4, 3, 4 };
                    acceptsInput = false;
                    DialogManager.instance.StartDialogue(lines, speakerIndexes, midFightVoiceLines);
                    DialogManager.instance.onDialogueEnd.AddListener(() => {
                        acceptsInput = true;
                    });
                }
                if (currentPhase > 7 && currentPhase <= 9)
                {
                    currentPhase++;
                }
                if (currentPhase == 10)
                {
                    currentPhase++;
                    string[] lines = {
                        "Ka¿de Twoje s³owo, kiedy byliœmy razem to k³amstwo. Nigdy nie zale¿a³o Ci na mnie, mia³eœ w g³owie tylko siebie i swoje dobro",
                        "To samo jest teraz w roli przewodnicz¹cego. Nie potrafisz byæ nawet przez chwilê ani powa¿ny, ani szczery",
                        "A Twoja dziecinnoœæ? Myœlisz, ¿e ten breloczek z dinozaurem dodaje Ci fajnoœci? Jak g³upia by³am...",
                        "Przestañ, przestañ!" };
                    int[] speakerIndexes = { 3, 3, 3, 4 };
                    acceptsInput = false;
                    DialogManager.instance.StartDialogue(lines, speakerIndexes, midFightVoiceLines);
                    DialogManager.instance.onDialogueEnd.AddListener(() => {
                        acceptsInput = true;
                    });
                }
                if (currentPhase > 10 && currentPhase <= 12)
                {
                    currentPhase++;
                }
                if (currentPhase == 13)
                {
                    currentPhase++;
                    string[] lines = {
                        "Tylko tyle pamiêtasz z naszego bycia razem? Nie pamiêtasz ¿adnych dobrych wspólnych chwil?",
                        "Mo¿e i pamiêtam, ale nie by³o ich wiele",
                        "Jak to niewiele? A wspólne gotowanie? Ogl¹danie Króla Lwa 2?",
                        "Idioto, nie ogl¹da³am z Tob¹ Króla Lwa 2. Ogl¹daliœmy wtedy Auta",
                        "Serio? A, faktycznie. Ale widzisz, nie by³o tak Ÿle. Na pewno pamiêtasz wiêcej fajnych chwil",
                        "I jeszcze wiêcej z³ych"
                    };
                    int[] speakerIndexes = { 4, 3, 4, 3, 4, 3 };
                    acceptsInput = false;
                    DialogManager.instance.StartDialogue(lines, speakerIndexes, midFightVoiceLines);
                    DialogManager.instance.onDialogueEnd.AddListener(() => {
                        acceptsInput = true;
                    });
                }
                break;
            case "Franek":
                if ((float)enemyCharacterList[0].Health / enemyCharacterList[0].MaxHealth <= 0.5f && currentPhase == 0)
                {
                    currentPhase++;
                    enemyCharacterList[0].Health = (int)(0.87221f * enemyCharacterList[0].MaxHealth);
                    enemyCharacterList[0].DefaultDefense = 99999;
                    enemyCharacterList[0].Defense = 99999;

                    playableCharacterList[0].skillSet.Clear();
                    TheEnd theEnd = new TheEnd();
                    playableCharacterList[0].skillSet.Add(theEnd);

                    currentPhase++;
                    string[] lines = {
                        "Pora to zakoñczyæ" };
                    int[] speakerIndexes = { 0 };
                    acceptsInput = false;
                    DialogManager.instance.StartDialogue(lines, speakerIndexes, midFightVoiceLines);
                    DialogManager.instance.onDialogueEnd.AddListener(() => {
                        acceptsInput = true;
                        UpdateHealthBarsAndIcons();
                    });
                }
                break;
        }
    }

    void CountKnockedOut()
    {
        playablesKnockedOut = 0; enemiesKnockedOut = 0;
        foreach (Character character in playableCharacterList)
        {
            if (character.KnockedOut)
            {
                playablesKnockedOut++;
            }
        }
        for (int i = 0; i < enemyCharacterList.Count; i++)
        {
            if (enemyCharacterList[i].KnockedOut)
            {
                enemiesKnockedOut++;
                enemySprites[enemySpriteIndexes[i]].SetActive(false);
                //enemySprites[i].SetActive(false);
            }
        }
    }

    IEnumerator disableAnimation(int index)
    {
        yield return new WaitForSeconds(0.5f);
        animationObjects[index].GetComponent<Animator>().SetInteger("animation", 0);
    }
}
