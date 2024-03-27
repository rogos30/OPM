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
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;
using static UnityEngine.GraphicsBuffer;

public class BattleManager : MonoBehaviour
{
    Vector2 returnPosition, battlePosition = new Vector2(1000,0);
    [SerializeField] GameObject player;

    public UnityEvent onBattleWon, onBattleLost;
    public static BattleManager instance;
    [SerializeField] Canvas battleCanvas;
    [SerializeField] Slider skillCheckSlider;
    [SerializeField] GameObject skillCheckBlueArea;
    [SerializeField] GameObject skillCheckGreenArea;
    [SerializeField] GameObject[] enemySprites;
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
    [SerializeField] Slider[] enemyHealthBars;
    [SerializeField] TMP_Text[] enemyHealthTexts;
    [SerializeField] Slider[] characterHealthBars;
    [SerializeField] Slider[] characterSkillBars;
    [SerializeField] TMP_Text[] characterHealthTexts;
    [SerializeField] TMP_Text[] characterSkillTexts;
    [SerializeField] TMP_Text[] actions;
    [SerializeField] TMP_Text[] subactions;
    [SerializeField] TMP_Text[] targetNames;
    [SerializeField] TMP_Text descriptionText;
    public TMP_Text battleFpsText;

    public Skill[] skillTable = new Skill[100];
    public List<FriendlyCharacter> playableCharacters = new List<FriendlyCharacter>();
    public List<EnemyCharacter> enemyCharacters = new List<EnemyCharacter>();

    List<FriendlyCharacter> playableCharacterList = new List<FriendlyCharacter>();
    List<EnemyCharacter> enemyCharacterList = new List<EnemyCharacter>();

    const int maxCharactersInBattle = 5, iconsPerPlayable = 6;
    const int guardSpBoost = 30, skillCheckSliderWidth = 500;
    const float defaultSkillCheckTime = 1.75f;

    Color orange = new Color(0.976f, 0.612f, 0.007f);
    int currentPlayable, currentEnemy;
    int currentRow, currentColumn, maxCurrentRow, currentPage, maxCurrentPage;
    int chosenAction, chosenSubaction, chosenTarget, currentMoveInTurn = 0, currentTurn = 0;
    int chosenSubactionPage;
    int currentPhase = 0;
    bool acceptsInput = false, enemyIsMoving = false, handlingPhases = false, battleFinished = false;
    bool skillCheckGoingRight, skillCheckAcceptsInput;
    int playablesKnockedOut = 0, enemiesKnockedOut = 0, uiIndexOffset = 0;
    int skillPerformance;
    float skillCheckTime = defaultSkillCheckTime, defaultBlueAreaScale, defaultGreenAreaScale;
    Vector3 defaultTextScale;
    Image[,] allEffectSprites = new Image[maxCharactersInBattle, iconsPerPlayable];

    [SerializeField] AudioClip[] battleMusic;
    AudioSource musicSource, sfxSource;
    public AudioMixerGroup musicMixerGroup, sfxMixerGroup;
    private void Awake()
    {
        instance = this;
        battleCanvas.enabled = false;
        InitializeFriendlyCharacter();
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
        }
        else
        {
            nextCharacters.SetActive(false);
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Return))
        {
            switch (currentColumn)
            {
                case 0: //just selected an action - entering subactions
                    currentColumn++;
                    chosenAction = currentRow;
                    actions[chosenAction].color = Color.red;
                    subactions[currentRow = 0].color = orange;
                    currentPage = 0;
                    switch(chosenAction)
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
                    break;
                case 1: //just selected a subaction - entering targets (most likely)
                    chosenSubaction = currentRow;
                    chosenSubactionPage = currentPage;
                    subactions[chosenSubaction].color = Color.red;
                    targetNames[currentRow = 0].color = orange;
                    currentPage = 0;
                    switch (chosenAction)
                    { //what action has been selected
                        case 0: //skill
                            if ((playableCharacterList[currentPlayable].skillSet[chosenSubactionPage * subactions.Length + chosenSubaction].Cost > playableCharacterList[currentPlayable].Skill &&
                                playableCharacterList[currentPlayable].skillSet[chosenSubactionPage * subactions.Length + chosenSubaction].Cost > 1) ||
                                ((int)(playableCharacterList[currentPlayable].skillSet[chosenSubactionPage * subactions.Length + chosenSubaction].Cost * playableCharacterList[currentPlayable].MaxSkill) > playableCharacterList[currentPlayable].Skill &&
                                playableCharacterList[currentPlayable].skillSet[chosenSubactionPage * subactions.Length + chosenSubaction].Cost <= 1))
                            { //too expensive to use
                                StartCoroutine(TooLittleSkillToUse());
                            }
                            else if (playableCharacterList[currentPlayable].skillSet[chosenSubactionPage * subactions.Length + chosenSubaction].TargetIsFriendly)
                            {
                                if (playableCharacterList[currentPlayable].skillSet[chosenSubactionPage * subactions.Length + chosenSubaction].MultipleTargets)
                                { //every ally is a target
                                    maxCurrentRow = 1;
                                    maxCurrentPage = 1;
                                    targetNames[0].text = "Wszyscy";
                                    currentColumn++;
                                }
                                else
                                { //a singular alive ally is a target
                                    PrintPageOfAllies(true);
                                    currentColumn++;
                                }
                            }
                            else if (playableCharacterList[currentPlayable].skillSet[chosenSubactionPage * subactions.Length + chosenSubaction].TargetIsSelf)
                            { //target is self
                                maxCurrentRow = 1;
                                maxCurrentPage = 1;
                                targetNames[0].text = playableCharacterList[currentPlayable].NominativeName;
                                currentColumn++;
                            }
                            else
                            { //targets are alive enemies
                                if (playableCharacterList[currentPlayable].skillSet[chosenSubactionPage * subactions.Length + chosenSubaction].MultipleTargets)
                                { //every enemy is a target
                                    maxCurrentRow = 1;
                                    maxCurrentPage = 1;
                                    targetNames[0].text = "Wszyscy";
                                }
                                else if (playableCharacterList[currentPlayable].skillSet[chosenSubactionPage * subactions.Length + chosenSubaction].TargetIsRandom)
                                { //random enemy is a target
                                    maxCurrentRow = 1;
                                    maxCurrentPage = 1;
                                    targetNames[0].text = "Losowo";
                                }
                                else
                                { //one enemy is a target
                                    PrintPageOfEnemies();
                                }
                                currentColumn++;
                            }
                            break;
                        case 1: //item
                            if (Inventory.Instance.items[chosenSubactionPage * subactions.Length + chosenSubaction].Amount > 0)
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
                                subactions[chosenSubaction].color = Color.white;
                                subactions[currentRow].color = orange;
                                descriptionText.text = Inventory.Instance.items[currentPage * subactions.Length + currentRow].Description;
                            }
                            break;
                        case 2: //guard
                            if (subactions[chosenSubaction].text == "PotwierdŸ") //accepted to guard
                            {
                                PerformPlayersMove();
                                maxCurrentRow = 4;
                            }
                            else //didn't guard
                            {
                                ClearSubactions();
                                currentColumn--;
                                maxCurrentRow = 4;
                            }
                            break;
                        case 3: //run
                            if (subactions[chosenSubaction].text == "PotwierdŸ") //accepted to run
                            {
                                StartCoroutine(FinishBattle(false));
                            }
                            else //didn't run
                            {
                                ClearSubactions();
                                currentColumn--;
                                maxCurrentRow = 4;
                            }
                            break;
                    }
                    break;
                case 2: //just selected a target
                    chosenTarget = enemyCharacterList.FindIndex(x => x.NominativeName.Equals(targetNames[currentRow].text)) % targetNames.Length;
                    if (chosenTarget == -1)
                    { //if not an enemy, then an ally (or all enemies which does not matter)
                        chosenTarget = playableCharacterList.FindIndex(x => x.NominativeName.Equals(targetNames[currentRow].text)) % targetNames.Length;
                    }

                    targetNames[currentRow].color = Color.red;
                    currentColumn++;
                    PerformPlayersMove();
                    maxCurrentRow = 4;
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Escape))
        {
            switch (currentColumn)
            {
                case 1: //backed out of subactions
                    actions[currentRow = chosenAction].color = orange;
                    switch (currentRow)
                    {
                        case 0: //skill
                            descriptionText.text = "U¿yj umiejêtnoœci";
                            break;
                        case 1: //item
                            descriptionText.text = "U¿yj przedmiotu";
                            break;
                        case 2: //guard
                            descriptionText.text = "Otrzymuj mniej obra¿eñ, wiêcej leczenia i " + guardSpBoost + " SP";
                            break;
                        case 3: //run
                            descriptionText.text = "Ucieknij z walki";
                            break;
                    }
                    currentPage = 0;
                    ClearSubactions();
                    maxCurrentRow = 4;
                    break;
                case 2: //backed out of targets
                    subactions[currentRow = chosenSubaction].color = orange;
                    currentPage = chosenSubactionPage;
                    ClearTargets();
                    switch (chosenAction)
                    {
                        case 0: //skill
                            PrintPageOfSkills();
                            maxCurrentRow = Mathf.Min(playableCharacterList[currentPlayable].skillSet.Count, subactions.Length);
                            break;
                        case 1: //item
                            PrintPageOfItems();
                            maxCurrentRow = subactions.Length;
                            break;
                    }
                    break;
            }
            currentColumn = Mathf.Max(currentColumn - 1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            switch (currentColumn)
            {
                case 0: //actions
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
                    switch (currentRow)
                    {
                        case 0: //skill
                            descriptionText.text = "U¿yj umiejêtnoœci";
                            break;
                        case 1: //item
                            descriptionText.text = "U¿yj przedmiotu";
                            break;
                        case 2: //guard
                            descriptionText.text = "Otrzymuj mniej obra¿eñ, wiêcej leczenia i " + guardSpBoost + " SP";
                            break;
                        case 3: //run
                            descriptionText.text = "Ucieknij z walki";
                            break;
                    }
                    break;
                case 1: //subactions
                    subactions[currentRow].color = Color.white;
                    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                    {
                        currentRow = (currentRow + 1) % maxCurrentRow;
                    }
                    else
                    {
                        currentRow = (currentRow - 1 < 0) ? (maxCurrentRow - 1) : (currentRow - 1);
                    }
                    subactions[currentRow].color = orange;
                    switch(chosenAction)
                    {
                        case 0: //skill
                            descriptionText.text = playableCharacterList[currentPlayable].NominativeName + " " +
                                playableCharacterList[currentPlayable].skillSet[currentPage * subactions.Length + currentRow].SkillDescription;
                            break;
                        case 1: //item
                            descriptionText.text = Inventory.Instance.items[currentPage * subactions.Length + currentRow].Description;
                            break;
                        default:
                            descriptionText.text = "";
                            break;
                    }
                    break;
                case 2: //targets
                    targetNames[currentRow].color = Color.white;
                    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                    {
                        currentRow = (currentRow + 1) % maxCurrentRow;
                    }
                    else
                    {
                        currentRow = (currentRow - 1 < 0) ? (maxCurrentRow - 1) : (currentRow - 1);
                    }
                    targetNames[currentRow].color = orange;
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {
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
                case 2:
                    if (chosenAction == 0) //skill
                    {
                        if (playableCharacterList[currentPlayable].skillSet[chosenSubactionPage * subactions.Length + chosenSubaction].TargetIsFriendly)
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
                        if (Inventory.Instance.items[chosenSubactionPage * subactions.Length + chosenSubaction].Resurrects)
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
            subactions[i].color = Color.white;
            targetNames[i].color = Color.white;
        }
        for (int i = 0;i < characterNames.Length; i++)
        {
            characterNames[i].color = Color.white;
        }
    }
    void ClearTargets()
    {
        for (int i = 0; i < targetNames.Length; i++)
        {
            targetNames[i].color = Color.white;
            targetNames[i].text = "";
        }
    }
    void ClearSubactions()
    {
        for (int i = 0; i < subactions.Length; i++)
        {
            subactions[i].color = Color.white;
            subactions[i].text = "";
        }
    }
    void PrintPageOfSkills()
    {
        for (int i = 0; i < subactions.Length; i++)
        {
            if (currentPage * subactions.Length + i < playableCharacterList[currentPlayable].UnlockedSkills)
            {
                float cost = 0;
                if (playableCharacterList[currentPlayable].skillSet[currentPage * subactions.Length + i].Cost > 1)
                {
                    cost = playableCharacterList[currentPlayable].skillSet[currentPage * subactions.Length + i].Cost;
                }
                else
                {
                    cost = (int)(playableCharacterList[currentPlayable].skillSet[currentPage * subactions.Length + i].Cost * playableCharacterList[currentPlayable].MaxSkill);
                }
                subactions[i].text = playableCharacterList[currentPlayable].skillSet[currentPage * subactions.Length + i].Name + " (" + cost + ")";
            }
            else
            {
                subactions[i].text = "";
            }
        }
        maxCurrentPage = (playableCharacterList[currentPlayable].UnlockedSkills - 1) / subactions.Length + 1;
        maxCurrentRow = Mathf.Min(playableCharacterList[currentPlayable].UnlockedSkills - currentPage * subactions.Length, subactions.Length);
        descriptionText.text = playableCharacterList[currentPlayable].NominativeName + " " + playableCharacterList[currentPlayable].skillSet[currentPage * subactions.Length].SkillDescription;
    }

    void PrintPageOfItems()
    {
        for (int i = 0; i < subactions.Length; i++)
        {
            subactions[i].text = Inventory.Instance.items[currentPage * subactions.Length + i].Name + " ("
                + Inventory.Instance.items[currentPage * subactions.Length + i].Amount + ")"; ;
        }
        maxCurrentRow = subactions.Length;
        maxCurrentPage = ShopManager.instance.level + 1;
        descriptionText.text = Inventory.Instance.items[currentPage * subactions.Length].Description;
    }
    void PrintYesNo()
    {
        subactions[0].text = "PotwierdŸ";
        subactions[1].text = "Cofnij";
        maxCurrentRow = 2;
        maxCurrentPage = 1;
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
        foreach (var text in targetNames)
        {
            text.text = "";
        }
        currentRow = 0;
        int skipped = 0, toSkip = currentPage * targetNames.Length, textIndex = 0;
        for (int i = 0; i < playableCharacterList.Count; i++)
        {
            if (playableCharacterList[i].KnockedOut != alive)
            {
                if (skipped < toSkip)
                {
                    skipped++;
                }
                else if (textIndex < targetNames.Length)
                {
                    targetNames[textIndex++].text = playableCharacterList[i].NominativeName;
                }
            }
        }
        maxCurrentRow = textIndex;
        maxCurrentPage = (playableCharacterList.Count - 1) / targetNames.Length + 1;
    }

    void PrintPageOfEnemies()
    {
        int skipped = 0, toSkip = currentPage * targetNames.Length, textIndex = 0;
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
                    targetNames[textIndex++].text = enemyCharacterList[i].NominativeName;
                }
            }
        }
        maxCurrentRow = textIndex;
        maxCurrentPage = toSkip / targetNames.Length + 1;
    }
    void PerformPlayersMove()
    {
        uiIndexOffset = 0;
        acceptsInput = false;
        float delay = 0;
        if (actions[chosenAction].text == "Garda")
        {
            playableCharacterList[currentPlayable].IsGuarding = true;
            playableCharacterList[currentPlayable].Skill =
            Mathf.Min(playableCharacterList[currentPlayable].Skill + guardSpBoost, playableCharacterList[currentPlayable].MaxSkill);
            UpdateHealthBarsAndIcons();
        }
        else if (actions[chosenAction].text == "Przedmiot")
        {
            descriptionText.text = Inventory.Instance.items[chosenSubactionPage * subactions.Length + chosenSubaction].Use(playableCharacterList[currentPlayable], playableCharacterList[currentPage * targetNames.Length + chosenTarget]);
            UpdateHealthBarsAndIcons();
        }
        else // skill
        {
            delay = 3;
            int skillIndex = chosenSubactionPage * subactions.Length + chosenSubaction;
            HandleSkillCheck(currentPlayable, skillIndex);
            if (playableCharacterList[currentPlayable].skillSet[skillIndex].TargetIsFriendly || playableCharacterList[currentPlayable].skillSet[skillIndex].TargetIsSelf)
            {
                if (playableCharacterList[currentPlayable].skillSet[skillIndex].MultipleTargets)
                { //skill targets every ally
                    StartCoroutine(FriendlyExecuteSkillOnEveryone(playableCharacterList[currentPlayable], skillIndex, playableCharacterList.Cast<Character>().ToList()));
                }
                else
                { //skill targets one ally
                    StartCoroutine(FriendlyExecuteSkill(playableCharacterList[currentPlayable], skillIndex, playableCharacterList[currentPage * targetNames.Length + chosenTarget]));
                }
            }
            else
            {
                if (playableCharacterList[currentPlayable].skillSet[skillIndex].MultipleTargets)
                { //skill targets every enemy
                    StartCoroutine(FriendlyExecuteSkillOnEveryone(playableCharacterList[currentPlayable], skillIndex, enemyCharacterList.Cast<Character>().ToList()));
                }
                else if (playableCharacterList[currentPlayable].skillSet[skillIndex].Repetitions > 1)
                { //skill targets random enemies multiple times
                    StartCoroutine(FriendlyExecuteSkillMultipleTimes(playableCharacterList[currentPlayable], skillIndex, enemyCharacterList.Cast<Character>().ToList()));
                }
                else
                { //skill targets one enemy
                    StartCoroutine(FriendlyExecuteSkill(playableCharacterList[currentPlayable], skillIndex, enemyCharacterList[currentPage * targetNames.Length + chosenTarget]));
                }
            }
        }
        if (currentMoveInTurn < playableCharacterList[currentPlayable].Turns - 1)
        {
            currentMoveInTurn++;
            StartCoroutine(AllowPlayerToMove(false));
        }
        else
        {
            StartCoroutine(FinishPlayersMove(delay));
        }
    }

    void FindAvailableToMove()
    {
        while (currentPlayable < playableCharacterList.Count && playableCharacterList[currentPlayable].KnockedOut)
        { //find next playable that can move or go beyond the list
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
                enemySprites[i].SetActive(false);
            }
        }
        if (playablesKnockedOut == playableCharacterList.Count && !battleFinished)
        { //player lost
            StartCoroutine(FinishBattle(false));
        }
        else if (enemiesKnockedOut == enemyCharacterList.Count && !battleFinished)
        { //enemy lost
            StartCoroutine(FinishBattle(true));
        }
        else
        {
            HandlePhases();
        }
        if (!handlingPhases)
        {
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
    }

    void HandleEnemysMove()
    {
        enemyCharacterList[currentEnemy].HandleEffects();
        enemyCharacterList[currentEnemy].HandleTimers();
        int delay = -3;
        if (enemyCharacterList[currentEnemy].Turns == 0)
        {
            StartCoroutine(EnemyIsParalyzed(enemyCharacterList[currentEnemy]));
        }
        for (int i = 0; i < enemyCharacterList[currentEnemy].Turns; i++)
        {
            delay += 3;
            StartCoroutine(PerformEnemysMove(delay, enemyCharacterList[currentEnemy]));
        }
        currentEnemy++;
        FindAvailableToMove();
    }

    IEnumerator EnemyIsParalyzed(Character source)
    {
        yield return new WaitForSeconds(0);
        descriptionText.text = source.NominativeName + " nie mo¿e siê ruszyæ!";
        DecideNextMove();
    }

    IEnumerator PerformEnemysMove(int delay, EnemyCharacter source)
    {
        yield return new WaitForSeconds(delay);
        int randSkill = Random.Range(0, source.skillSet.Count);
        int randTarget;

        if (source.skillSet[randSkill].TargetIsFriendly)
        { //targets are alive enemies
            if (source.skillSet[randSkill].MultipleTargets)
            { //skill targets all enemies
                StartCoroutine(EnemyExecuteSkillOnEveryone(source, randSkill, enemyCharacterList.Cast<Character>().ToList()));
            }
            else
            { //skill targets a single enemy
                randTarget = ChooseRandomTarget(enemyCharacterList.Cast<Character>().ToList());
                descriptionText.text = source.skillSet[randSkill].execute(source, enemyCharacterList[randTarget]);
                UpdateHealthBarsAndIcons();
            }
        }
        else
        { //targets are playables
            randTarget = ChooseRandomTarget(playableCharacterList.Cast<Character>().ToList());
            if (randTarget == -1)
            {
                StartCoroutine(FinishBattle(false));
                yield break;
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
                descriptionText.text = source.skillSet[randSkill].execute(source, playableCharacterList[randTarget]);
                UpdateHealthBarsAndIcons();
            }
        }
        if (delay+3 == source.Turns * 3)
        {
            DecideNextMove();
        }
    }

    public void InitiateBattle(int[] playables, int[] enemies)
    {
        musicSource.clip = battleMusic[enemies[0]];
        musicSource.loop = true;
        musicSource.Play();
        skillCheckSlider.gameObject.SetActive(false);
        acceptsInput = true;
        enemyIsMoving = false;
        handlingPhases = false;
        currentEnemy = 0; currentPlayable = 0; currentRow = 0; currentColumn = 0; maxCurrentRow = 4; currentPage = 0; maxCurrentPage = 1;
        currentPhase = 0; currentTurn = 0; uiIndexOffset = 0; skillPerformance = 0;
        skillCheckTime = defaultSkillCheckTime - 0.25f * GameManager.instance.difficulty;
        battleFinished = false;
        battleCanvas.enabled = true;
        GameManager.instance.inGameCanvas.enabled = false;
        returnPosition = player.transform.position;
        player.transform.position = battlePosition;
        player.SetActive(false);
        RidUIofColor();
        for (int i=0; i < characterNames.Length; i++) //clear everything up
        {
            characterNames[i].text = "";
            characterHealthBars[i].gameObject.SetActive(false);
            characterSkillBars[i].gameObject.SetActive(false);
            for (int j = 0; j < iconsPerPlayable; j++)
            {
                allEffectSprites[i, j].sprite = effectSprites[12];
            }
        }
        for (int i=0; i < subactions.Length; i++)
        {
            subactions[i].text = "";
            targetNames[i].text = "";
            enemyHealthBars[i].gameObject.SetActive(false);
            enemyNames[i].text = "";
        }
        descriptionText.text = "U¿yj umiejêtnoœci";
        characterNames[0].color = Color.red;
        actions[0].color = orange;

        for (int i = 0; i < playables.Length; i++) //show what's necessary
        {
            playableCharacters[playables[i]].Reset();
            playableCharacterList.Add(playableCharacters[playables[i]]);
            characterNames[i].text = playableCharacterList[i].NominativeName;
            characterHealthBars[i].gameObject.SetActive(true);
            characterSkillBars[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < enemies.Length; i++)
        {
            enemyCharacters[enemies[i]].Reset();
            enemyCharacterList.Add(enemyCharacters[enemies[i]]);
            enemySprites[enemies[i]].SetActive(true);
            enemyNames[i].text = enemyCharacterList[i].NominativeName;
            enemyHealthBars[i].gameObject.SetActive(true);
        }
        UpdateHealthBarsAndIcons();
    }

    IEnumerator FinishBattle(bool playerWon)
    {
        battleFinished = true;
        acceptsInput = false;
        if (playerWon)
        {
            int xpEarned = 0, moneyEarned = 0;
            for (int i = 0; i < enemyCharacterList.Count; i++)
            {
                xpEarned += enemyCharacterList[i].XPDropped;
                moneyEarned += enemyCharacterList[i].MoneyDropped;
            }
            for (int i = 0; i < playableCharacterList.Count; i++)
            {
                playableCharacterList[i].HandleLevel(xpEarned);
            }
            Inventory.Instance.Money += moneyEarned;
            Debug.Log("Earned " + moneyEarned + " money. Now you have " + Inventory.Instance.Money + " money");
        }
        else
        {
            
        }
        yield return new WaitForSeconds(2);
        musicSource.Stop();
        foreach (var sprite in enemySprites)
        {
            sprite.SetActive(false);
        }
        playableCharacterList.Clear();
        enemyCharacterList.Clear();
        player.SetActive(true);
        player.transform.position = returnPosition;
        battleCanvas.enabled = false;
        GameManager.instance.inGameCanvas.enabled = true;
        StopAllCoroutines();
        if (playerWon)
        {
            onBattleWon.Invoke();
        }
        else
        {
            onBattleLost.Invoke();
        }
    }

    /*void InitializeSkills()
    {
        int i = 0;
        int[] statusEffects = new int[] { 0, 0, 0, 0, 0};
        Skill skill = new Skill("Atak", "wyprowadza zwyk³y cios", "wyprowadza zwyk³y cios", 0, 1, 1, 1, 0, false, false, false, false, statusEffects);
        skillTable[i++] = skill; //0

        statusEffects = new int[] { 0, 0, 0, 0, 0 };
        skill = new Skill("Podwójny atak", "wyprowadza 2 zwyk³e ciosy", "wyprowadza 2 zwyk³e ciosy", 20, 2, 1, 1, 0, false, false, true, false, statusEffects);
        skillTable[i++] = skill;

        statusEffects = new int[] { 0, 0, 0, 0, 0 };
        skill = new Skill("Potrójny atak", "wyprowadza 3 zwyk³e ciosy", "wyprowadza 3 zwyk³e ciosy", 35, 3, 1, 1, 0, false, false, true, false, statusEffects);
        skillTable[i++] = skill;

        statusEffects = new int[] { 0, 0, 0, 0, 0 };
        skill = new Skill("Leczenie", "odnawia czêœæ HP", "odnawia czêœæ HP", 30, 1, 1, 1, 160, true, false, false, false, statusEffects);
        skillTable[i++] = skill;

        statusEffects = new int[] { 0, 0, 0, 0, 0 };
        skill = new Skill("Bomba", "robi wielki zamach. Spora szansa na pud³o", "robi wielki zamach", 25, 1, 4f, 0.45f, 0, false, false, false, false, statusEffects);
        skillTable[i++] = skill;

        statusEffects = new int[] { 0, 0, 0, 0, 0 };
        skill = new Skill("Pierd", "pierdzi na przeciwników", "wypuszcza chmurê gazu", 40, 1, 1, 0.8f, 0, false, false, false, true, statusEffects);
        skillTable[i++] = skill; //5

        statusEffects = new int[] { 0, 0, 0, 0, -1};
        skill = new Skill("Parali¿", "próbuje sparali¿owaæ wroga. 40% szans na powodzenie", "parali¿uje", 30, 1, 0, 0.4f, 0, false, false, false, false, statusEffects);
        skillTable[i++] = skill;

        statusEffects = new int[] { 0, 0, 0, 0, 1};
        skill = new Skill("Cheaty", "zwiêksza sobie liczbê tur", "oszukuje grê i dodaje sobie ruch", 50, 1, 0, 2f, 0, false, true, false, false, statusEffects);
        skillTable[i++] = skill;

        statusEffects = new int[] { 0, 0, 0, -1, 0 };
        skill = new Skill("Grant's", "proponuje przeciwnikowi Grant'sa. Mo¿e otruæ", "otruwa Grant'sem w¹trobê", 40, 1, 0, 0.5f, 0, false, false, false, false, statusEffects);
        skillTable[i++] = skill;

        statusEffects = new int[] { 0, 0, 0, 0, 0 };
        skill = new Skill("Rzut padem", "rzuca padem jak bumerangiem. Zadaje obra¿enia 2 razy.", "rzuca padem", 70, 2, 1.5f, 1, 0, false, false, true, false, statusEffects);
        skillTable[i++] = skill;

        statusEffects = new int[] { 0, 0, 1, 0, 0 };
        skill = new Skill("Podbicie morale", "zwiêksza morale w dru¿ynie, zwiêkszaj¹c celnoœæ sojusznikom", "zwiêksza celnoœæ", 50, 1, 0, 1, 0, true, false, false, true, statusEffects);
        skillTable[i++] = skill; //10

        statusEffects = new int[] { 0, 0, 0, 0, 0 };
        skill = new Skill("Ostrza³ padami", "rzuca 4 padami jak bumerangami. Zadaje obra¿enia 8 razy.", "rzuca padami", 210, 8, 1.5f, 1, 0, false, false, true, false, statusEffects);
        skillTable[i++] = skill;

        statusEffects = new int[] { 0, 1, 0, 0, 0 };
        skill = new Skill("Postawa tanka", "zwiêksza swoj¹ obronê i leczy do 40% hp.", "wchodzi w postawê tanka", 65, 1, 0, 1, 0.4f, false, true, false, false, statusEffects);
        skillTable[i++] = skill;

        statusEffects = new int[] { 0, 0, 0, 0, 0 };
        skill = new Skill("Obietnica wyborcza", "spe³nia swoj¹ obietnicê wyborcz¹", "nie robi nic", 0, 1, 0, 1, 0, false, true, false, false, statusEffects);
        skillTable[i++] = skill;

        statusEffects = new int[] { 0, 0, 0, 0, 0 };
        skill = new Skill("Rozjazd", "wzywa swojego konia i rozje¿d¿a nim wszystkich wrogów", "traktuje wrogów jako tor jeŸdziecki", 50, 1, 1.2f, 1.2f, 0, false, false, false, true, statusEffects);
        skillTable[i++] = skill;

        statusEffects = new int[] { 0, 0, -1, 0, 0 };
        skill = new Skill("Podkrêtka", "zagrywa przeciwnikowi podkrêcon¹ pi³eczkê. Zadaje œrednie obra¿enia i obni¿a celnoœæ wroga.", "zagrywa podkrêcon¹ pi³eczkê", 60, 1, 0.4f, 0.8f, 0, false, false, false, false, statusEffects);
        skillTable[i++] = skill; //15

        statusEffects = new int[] { 0, 0, 0, 0, 0 };
        skill = new Skill("Debel", "gra z kompanem debla. Przywraca sobie i kompanowi 1000hp.", "gra z kompanem debla", 0.4f, 1, 0, 1, 1000, true, true, false, false, statusEffects);
        skillTable[i++] = skill;

        statusEffects = new int[] { 0, 0, 0, 0, 0 };
        skill = new Skill("Œcina", "œcina pi³eczkê w przeciwnika. Wysokie obra¿enia, ale ciê¿ko trafiæ.", "œcina pi³eczkê", 150, 1, 3, 0.4f, 0, false, false, false, false, statusEffects);
        skillTable[i++] = skill;

        statusEffects = new int[] { 0, 0, 0, 0, 0 };
        skill = new Skill("Rzut ¿ab¹", "rzuca ¿ab¹ w przeciwnika. Zadaje œrednie obra¿enia, ale ³atwo trafiæ", "rzuca ¿ab¹", 0.2f, 1, 1.25f, 1.4f, 0, false, false, false, false, statusEffects);
        skillTable[i++] = skill;

        statusEffects = new int[] { 0, 0, 0, 1, 0 };
        skill = new Skill("¯abia czapka", "zak³ada ¿abi¹ czapkê, lecz¹c 30% hp i nak³adaj¹c regeneracjê", "zak³ada ¿abi¹ czapkê", 120, 1, 0, 1, 0.3f, false, true, false, false, statusEffects);
        skillTable[i++] = skill;

        statusEffects = new int[] { 1, 1, 0, 0, 0 };
        skill = new Skill("Ukraiñska moc", "budzi w sobie moc Swiet³any i przekazuje j¹ sojusznikowi, zwiêkszaj¹c mu atak i obronê", "przekazuje moc Swiet³any", 0.35f, 1, 0, 0.8f, 0, true, false, false, false, statusEffects);
        skillTable[i++] = skill; //20

        statusEffects = new int[] { 0, 0, 0, 0, 0 };
        skill = new Skill("£ut szczêœcia", "leczy losowego kompana za ca³e jego hp", "odnawia ca³e zdrowie", 175, 1, 0, 0.75f, 1, true, false, true, false, statusEffects);
        skillTable[i++] = skill;

        statusEffects = new int[] { 0, 1, 0, 0, 0 };
        skill = new Skill("Kominiarka", "zak³ada kominiarkê, zwiêkszaj¹c sobie obronê", "zak³ada kominiarkê", 35, 1, 0, 1, 0, false, true, false, false, statusEffects);
        skillTable[i++] = skill;

        statusEffects = new int[] { 0, 0, 0, 0, 0 };
        skill = new Skill("Sza³ szabli", "wykonuje 3 mocne ciêcia w losowych przeciwników", "wykonuje 3 mocne ciêcia", 0.5f, 3, 1.75f, 0.9f, 0, false, false, true, false, statusEffects);
        skillTable[i++] = skill;

        statusEffects = new int[] { 0, 0, 0, 0, 0 };
        skill = new Skill("Wir szabli", "krêci siê z szabl¹ miêdzy przeciwnikami, ka¿demu zadaj¹c obra¿enia", "rozkrêca wir szabli", 0.5f, 1, 1.75f, 0.9f, 0, false, false, false, true, statusEffects);
        skillTable[i++] = skill;

        statusEffects = new int[] { 0, 0, 0, 0, -1 };
        skill = new Skill("Dinologia", "zanudza przeciwnika ciekawostkami o dinozaurach. Ciê¿ko trafiæ, ale parali¿uje.", "opowiada o dinozaurach", 0.35f, 1, 0, 0.5f, 0, false, false, false, false, statusEffects);
        skillTable[i++] = skill; //25

        statusEffects = new int[] { 0, 0, -1, 0, 0 };
        skill = new Skill("Zepsucie morale", "demotywuje rywali, zmniejszaj¹c im celnoœæ", "zmniejsza celnoœæ", 110, 1, 0, 0.75f, 0, false, false, false, true, statusEffects);
        skillTable[i++] = skill;

        statusEffects = new int[] { 0, 0, -1, 0, 0 };
        skill = new Skill("Podgl¹d", "przygl¹da siê rywalom, dekoncentruj¹c ich. Zmniejsza celnoœæ przeciwnikom.", "przygl¹da siê rywalom, dekoncentruj¹c ich. Zmniejsza celnoœæ", 110, 1, 0, 0.5f, 0, false, false, false, true, statusEffects);
        skillTable[i++] = skill;

        statusEffects = new int[] { -1, 0, 0, 0, 0 };
        skill = new Skill("Inwigilacja", "przegl¹da wrogów na wylot, os³abiaj¹c ich. Zmniejsza obra¿enia przeciwnikom.", "przegl¹da wrogów na wylot, os³abiaj¹c ich. Zmniejsza obra¿enia", 110, 1, 0, 0.5f, 0, false, false, false, true, statusEffects);
        skillTable[i++] = skill;

        statusEffects = new int[] { 1, 0, 0, 0, 0 };
        skill = new Skill("Po³¹czenie", "³¹czy siê z pozosta³ymi urz¹dzeniami. Zwiêksza obra¿enia dru¿ynie.", "³¹czy siê z pozosta³ymi urz¹dzeniami. Zwiêksza obra¿enia", 110, 1, 0, 0.5f, 0, false, false, false, true, statusEffects);
        skillTable[i++] = skill;
    }*/

    void InitializeFriendlyCharacter()
    {
        FriendlyCharacter character = new Rogos();
        playableCharacters.Add(character);
    }

    void InitializeEnemyCharacters()
    {                   //hp, hpIncrease, sp, attack, attackIncrease, defense, accuracy, turns, speed, goldDropped, xpDropped
        /*List<Skill> skills = new List<Skill> { skillTable[0], skillTable[0], skillTable[0], skillTable[0], skillTable[1] };
        EnemyCharacter character = new EnemyCharacter("Ch³opak z fakerem", 225, 30, 50, 15, 15, 0.85f, 1, 400, 20, 200, skills);
        enemyCharacters.Add(character); skills.Clear();

        skills = new List<Skill> { skillTable[0], skillTable[0], skillTable[0], skillTable[0], skillTable[4] };
        character = new EnemyCharacter("Hinduski dzieciak", 150, 30, 60, 20, 15, 0.9f, 1, 350, 25, 150, skills);
        enemyCharacters.Add(character); skills.Clear();

        skills = new List<Skill> { skillTable[0], skillTable[0], skillTable[0], skillTable[0], skillTable[3] };
        character = new EnemyCharacter("Wkurzona laska", 175, 25, 45, 10, 15, 0.95f, 1, 275, 20, 200, skills);
        enemyCharacters.Add(character); skills.Clear();

        skills = new List<Skill> { skillTable[0] };
        character = new EnemyCharacter("Dzieciak z fochem", 200, 20, 55, 15, 20, 0.85f, 1, 250, 15, 250, skills);
        enemyCharacters.Add(character); skills.Clear();

        skills = new List<Skill> { skillTable[0] };
        character = new EnemyCharacter("Œwietlik", 100, 125, 60, 25, 25, 90, 1, 400, 30, 500, skills);
        enemyCharacters.Add(character); skills.Clear();

        skills = new List<Skill> { skillTable[0], skillTable[0], skillTable[0], skillTable[1] };
        character = new EnemyCharacter("Welenc", 100, 250, 120, 30, 10, 85, 1, 300, 20, 300, skills);
        enemyCharacters.Add(character); skills.Clear();

        skills = new List<Skill> { skillTable[2], skillTable[2], skillTable[5], skillTable[27] };
        character = new EnemyCharacter("Monitoring", 30000, 5000, 500, 50, 60, 90, 1, 450, 2500, 1, skills);
        enemyCharacters.Add(character); skills.Clear();

        skills = new List<Skill> { skillTable[1], skillTable[1], skillTable[28]};
        character = new EnemyCharacter("Kamera 1", 15000, 2500, 400, 40, 70, 85, 1, 350, 0, 1, skills);
        enemyCharacters.Add(character); skills.Clear();

        skills = new List<Skill> { skillTable[1], skillTable[1], skillTable[2], skillTable[29] };
        character = new EnemyCharacter("Kamera 2", 13000, 2000, 500, 50, 65, 100, 1, 340, 0, 1, skills);
        enemyCharacters.Add(character); skills.Clear();*/

        EnemyCharacter character = new MiddleFingerKid();
        enemyCharacters.Add(character);

        character = new IndianKid();
        enemyCharacters.Add(character);

        character = new AngryGirl();
        enemyCharacters.Add(character);

        character = new OffendedKid();
        enemyCharacters.Add(character);

        character = new EnemySwietlik();
        enemyCharacters.Add(character);

        character = new Welenc();
        enemyCharacters.Add(character);
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
                    allEffectSprites[i, j+1].sprite = effectSprites[12];
                }
                else if (playableCharacterList[index].StatusTimers[j] < 0)
                {
                    allEffectSprites[i, j+1].sprite = effectSprites[3 + j * 2];
                }
                else
                {
                    allEffectSprites[i, j+1].sprite = effectSprites[2 + j * 2];
                }
            }
        }
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
        yield return new WaitForSeconds(5);
        if (rotatePlayables)
        {
            playableCharacterList[currentPlayable].HandleEffects();
            playableCharacterList[currentPlayable].HandleTimers();
            RotatePlayables();
        }
        acceptsInput = true;
        currentColumn = 0;
        ClearSubactions();
        ClearTargets();
        RidUIofColor();
        actions[currentRow = 0].color = orange;
        descriptionText.text = "U¿yj umiejêtnoœci";
    }

    IEnumerator AllowEnemyToMove()
    {
        yield return new WaitForSeconds(5);
        enemyIsMoving = true;
        ClearSubactions();
        ClearTargets();
        RidUIofColor();
    }

    IEnumerator FriendlyExecuteSkillOnEveryone(FriendlyCharacter source, int skill, List<Character> targets)
    {
        yield return new WaitForSeconds(2.5f); //friendly character - wait for skillcheck to end
        for (int i=0; i < targets.Count; i++)
        {
            if (!targets[i].KnockedOut) 
            {
                descriptionText.text = source.skillSet[skill].execute(source, targets[i], skillPerformance);
                if (i > 0)
                {
                    if (source.skillSet[skill].Cost > 1 || source.skillSet[skill].Cost == 0)
                    {
                        source.Skill += (int)source.skillSet[skill].Cost;
                    }
                    else
                    {
                        source.Skill = (int)(source.Skill / source.skillSet[skill].Cost);
                    }
                }
                UpdateHealthBarsAndIcons();
                yield return new WaitForSeconds(1.5f / targets.Count);
            }
        }
        if (source is Swietlik)
        {
            ((Swietlik)source).ResetBetrayal();
        }
    }

    IEnumerator EnemyExecuteSkillOnEveryone(EnemyCharacter source, int skill, List<Character> targets)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (!targets[i].KnockedOut)
            {
                descriptionText.text = source.skillSet[skill].execute(source, targets[i]);
                UpdateHealthBarsAndIcons();
                yield return new WaitForSeconds(1.5f / targets.Count);
            }
        }
    }

    IEnumerator FriendlyExecuteSkillMultipleTimes(FriendlyCharacter source, int skill, List<Character> targets)
    {
        yield return new WaitForSeconds(2.5f);
        for (int i=0; i < source.skillSet[skill].Repetitions; i++)
        {
            int target = ChooseRandomTarget(targets);
            descriptionText.text = source.skillSet[skill].execute(source, targets[target], skillPerformance);
            if (i > 0)
            {
                if (source.skillSet[skill].Cost > 1 || source.skillSet[skill].Cost == 0)
                {
                    source.Skill += (int)source.skillSet[skill].Cost;
                }
                else
                {
                    source.Skill += (int)(source.MaxSkill * source.skillSet[skill].Cost);
                }
            }
            UpdateHealthBarsAndIcons();
            yield return new WaitForSeconds(1.5f / source.skillSet[skill].Repetitions);
        }
        if (source is Swietlik)
        {
            ((Swietlik)source).ResetBetrayal();
        }
    }

    IEnumerator EnemyExecuteSkillMultipleTimes(EnemyCharacter source, int skill, List<Character> targets)
    {
        for (int i = 0; i < source.skillSet[skill].Repetitions; i++)
        {
            int target = ChooseRandomTarget(targets);
            descriptionText.text = source.skillSet[skill].execute(source, targets[target]);
            UpdateHealthBarsAndIcons();
            yield return new WaitForSeconds(1.5f / source.skillSet[skill].Repetitions);
        }
    }

    IEnumerator FriendlyExecuteSkill(FriendlyCharacter source, int skill, Character target)
    {
        yield return new WaitForSeconds(2.5f);
        descriptionText.text = source.skillSet[skill].execute(source, target, skillPerformance);
        UpdateHealthBarsAndIcons();
    }

    IEnumerator EnemyExecuteSkill(EnemyCharacter source, int skill, Character target)
    {
        yield return null;
        descriptionText.text = source.skillSet[skill].execute(source, target);
        UpdateHealthBarsAndIcons();
    }

    IEnumerator FinishPlayersMove(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentPlayable++;
        uiIndexOffset++;
        currentMoveInTurn = 0;
        FindAvailableToMove();
        DecideNextMove();
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
            result = Random.Range(0, targets.Count);
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
            enemyCharacters[enemies[i]].Reset();
            enemyCharacterList.Add(enemyCharacters[enemies[i]]);
            enemySprites[enemies[i]].SetActive(true);
            enemyNames[enemyCharacterList.Count-1].text = enemyCharacters[enemies[i]].NominativeName;
            enemyHealthBars[enemyCharacterList.Count - 1].gameObject.SetActive(true);
        }
    }

    void HandleSkillCheck(int currPlayable, int skillIndex)
    {
        int greenPosition = (int)Random.Range(-skillCheckSliderWidth * (1 - skillCheckGreenArea.transform.localScale.x) / 2, skillCheckSliderWidth * (1 - skillCheckGreenArea.transform.localScale.x) / 2);
        int bluePosition = (int)Random.Range(-skillCheckSliderWidth * (1 - skillCheckBlueArea.transform.localScale.x) / 2, skillCheckSliderWidth * (1 - skillCheckBlueArea.transform.localScale.x) / 2);
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
            yield return new WaitForSeconds(skillCheckTime / (2 * skillCheckSlider.maxValue));
        }
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
    }

    void HandlePhases()
    {
        handlingPhases = true;
        switch(enemyCharacterList[0].NominativeName)
        {
            /*case "Welenc":
                if ((float)enemyCharacterList[0].Health / enemyCharacterList[0].MaxHealth < 0.5f && currentPhase == 0)
                {
                    currentPhase++;
                    string[] lines = { "Dobra, kurwa, tak nie bedzie", "Duplikuje sie i w pizdzie was mam" };
                    int[] speakerIndexes = { 3, 3 };
                    DialogManager.instance.StartDialogue(lines, speakerIndexes);
                    DialogManager.instance.onDialogueEnd.AddListener(Chuj);
                }
                else
                {
                    handlingPhases = false;
                }
                break;*/
            case "Monitoring":
                if ((float)enemyCharacterList[0].Health / enemyCharacterList[0].MaxHealth < 0.67f && currentPhase == 0)
                {
                    currentPhase++;
                    int[] enemies = { 7 };
                    AddEnemyCharactersToBattle(enemies);
                    UpdateHealthBarsAndIcons();
                    handlingPhases = false;
                    DecideNextMove();
                }
                if ((float)enemyCharacterList[0].Health / enemyCharacterList[0].MaxHealth < 0.33f && currentPhase == 1)
                {
                    currentPhase++;
                    int[] enemies = { 8 };
                    AddEnemyCharactersToBattle(enemies);
                    UpdateHealthBarsAndIcons();
                    handlingPhases = false;
                    DecideNextMove();
                }
                else
                {
                    handlingPhases = false;
                }
                break;
            default:
                handlingPhases = false;
                break;
        }
    }

    void Chuj()
    {
        DialogManager.instance.onDialogueEnd.RemoveListener(Chuj);
        int[] enemies = { 2 };
        AddEnemyCharactersToBattle(enemies);
        UpdateHealthBarsAndIcons();
        handlingPhases = false;
        DecideNextMove();
    }
}
