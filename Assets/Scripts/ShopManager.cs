using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public GameObject player;
    public Canvas shopCanvas;
    public TMP_Text[] categories;
    public TMP_Text[] items;
    public TMP_Text pageText;
    public TMP_Text moneyText;
    public TMP_Text upgradeText;
    public TMP_Text shopLevelText;
    public TMP_Text paymentText;
    public TMP_Text itemDescriptionText;
    public TMP_Text controlsText;
    public Canvas warningCanvas;
    public TMP_Text warningText;


    [SerializeField] AudioMixer mixer;
    AudioSource musicSource, sfxSource;
    public AudioMixerGroup musicMixerGroup, sfxMixerGroup;
    [SerializeField] AudioClip shopMusic;

    Color orange = new Color(0.976f, 0.612f, 0.007f);
    int currentColumn, currentRow, amountToBuy, maxAmountToBuy, currentPage;
    public int level;
    readonly int[] upgradeCosts = { 500, 2000, 5000 };
    const string defaultControlsText = "W/S - nawigacja, A/D - iloœæ, Z/X - kategoria, Q/E - strona\r\nENTER - zakup, ESC - wyjœcie",
                controlsTextAddOn = ", LSHIFT - ulepszenie";
    const int maxLevel = 3;
    readonly int[] storyRequirementsForUpgrades = { 2, 3, 4};
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.outputAudioMixerGroup = musicMixerGroup;
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.outputAudioMixerGroup = sfxMixerGroup;
        shopCanvas.enabled = false;
        warningCanvas.enabled = false;
        level = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (shopCanvas.enabled)
        {
            if (!warningCanvas.enabled)
            {
                HandleInput();
            }
            else
            {
                HandleWarningInput();
            }
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitShop();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            items[currentRow].color = Color.white;
            currentRow = (currentRow - 1 < 0) ? 3 : (currentRow - 1);
            items[currentRow].color = orange;
            amountToBuy = 0;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            items[currentRow].color = Color.white;
            currentRow = (currentRow + 1) % 4;
            items[currentRow].color = orange;
            amountToBuy = 0;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            amountToBuy = (amountToBuy - 1 < 0) ? 0 : (amountToBuy - 1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            amountToBuy = (amountToBuy + 1 > maxAmountToBuy) ? maxAmountToBuy : (amountToBuy + 1);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            HandlePurchase();
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            HandleUpgrade();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            amountToBuy = 0;
            categories[currentColumn].color = Color.white;
            currentColumn = 0;
            categories[currentColumn].color = Color.red;
            PrintItems();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            amountToBuy = 0;
            categories[currentColumn].color = Color.white;
            currentColumn = 1;
            categories[currentColumn].color = Color.red;
            PrintEquipment();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            currentPage = (currentPage - 1 < 0) ? currentPage : currentPage - 1;
            pageText.text = "Strona: " + (currentPage + 1) + "/" + (ShopManager.instance.level + 1);
            if (currentColumn == 0)
            {
                PrintItems();
            }
            else
            {
                PrintEquipment();
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            currentPage = (currentPage + 1 > ShopManager.instance.level) ? currentPage : currentPage + 1;
            pageText.text = "Strona: " + (currentPage + 1) + "/" + (ShopManager.instance.level + 1);
            if (currentColumn == 0)
            {
                PrintItems();
            }
            else
            {
                PrintEquipment();
            }
        }
        if (Input.anyKey)
        {
            if (currentColumn == 0)
            {
                maxAmountToBuy = Inventory.Instance.maxOwnedItems - Inventory.Instance.items[currentPage * 4 + currentRow].Amount;
                paymentText.text = amountToBuy + " x " + Inventory.Instance.items[currentPage * 4 + currentRow].Cost + "PLN = " +
                    amountToBuy * Inventory.Instance.items[currentPage * 4 + currentRow].Cost + "PLN";
                itemDescriptionText.text = Inventory.Instance.items[currentPage * 4 + currentRow].Description + ". Masz: " + Inventory.Instance.items[currentPage * 4 + currentRow].Amount;
            }
            else
            {
                maxAmountToBuy = Inventory.Instance.maxOwnedItems - Inventory.Instance.wearables[currentPage * 4 + currentRow].Amount;
                paymentText.text = amountToBuy + " x " + Inventory.Instance.wearables[currentPage * 4 + currentRow].Cost + "PLN = " +
                    amountToBuy * Inventory.Instance.wearables[currentPage * 4 + currentRow].Cost + "PLN";
                itemDescriptionText.text = Inventory.Instance.wearables[currentPage * 4 + currentRow].Description + ". Masz: " + Inventory.Instance.wearables[currentPage * 4 + currentRow].Amount;
            }
        }
    }

    void EnableWarningCanvas()
    {
        warningCanvas.enabled = true;
        warningText.text = "Tej operacji nie mo¿na cofn¹æ, a sporo kosztuje.\r\nCzy chcesz kontynuowaæ?";
    }

    void HandleWarningInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            warningCanvas.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            warningCanvas.enabled = false;
            PerformUpgrade();
            Inventory.Instance.Money -= upgradeCosts[level++];
        }
    }

    void PrintItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].text = Inventory.Instance.items[currentPage * 4 + i].Name + "\r\n(koszt: " + Inventory.Instance.items[currentPage * 4 + i].Cost + "PLN)";
        }
    }

    void PrintEquipment()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].text = Inventory.Instance.wearables[currentPage * 4 + i].Name + "\r\n(koszt: " + Inventory.Instance.wearables[currentPage * 4 + i].Cost + "PLN)";
        }
    }

    void HandlePurchase()
    {
        if (currentColumn == 0)
        {
            if (Inventory.Instance.Money >= amountToBuy * Inventory.Instance.items[currentPage * 4 + currentRow].Cost)
            {
                Inventory.Instance.Money -= amountToBuy * Inventory.Instance.items[currentPage * 4 + currentRow].Cost;
                Inventory.Instance.items[currentPage * 4 + currentRow].Add(amountToBuy);
            }
            else
            {
                StartCoroutine(CantAfford());
            }
        }
        else
        {
            if (Inventory.Instance.Money >= amountToBuy * Inventory.Instance.wearables[currentPage * 4 + currentRow].Cost)
            {
                Inventory.Instance.Money -= amountToBuy * Inventory.Instance.wearables[currentPage * 4 + currentRow].Cost;
                Inventory.Instance.wearables[currentPage * 4 + currentRow].Add(amountToBuy);
            }
            else
            {
                StartCoroutine(CantAfford());
            }
        }
        moneyText.text = "Kasa: " + Inventory.Instance.Money + " PLN";
    }

    void HandleUpgrade()
    {
        if (level < maxLevel && StoryManager.instance.currentMainQuest >= storyRequirementsForUpgrades[level])
        {
            if (Inventory.Instance.Money >= upgradeCosts[level])
            {
                EnableWarningCanvas();
            }
            else
            {
                StartCoroutine(CantAfford());
            }
        }
    }

    public void PerformUpgrade()
    {
        moneyText.text = "Kasa: " + Inventory.Instance.Money + " PLN";
        pageText.text = "Strona: " + (currentPage + 1) + "/" + (ShopManager.instance.level + 1);
        controlsText.text = defaultControlsText;
        if (level < maxLevel && StoryManager.instance.currentMainQuest >= storyRequirementsForUpgrades[level])
        {
            upgradeText.text = "Ulepszenie: " + upgradeCosts[level] + " PLN";
            controlsText.text += controlsTextAddOn;
        }
        else if (level < maxLevel && StoryManager.instance.currentMainQuest < storyRequirementsForUpgrades[level])
        {
            upgradeText.text = "Kontynuuj fabu³ê, aby móc ulepszyæ";
        }
        else
        {
            upgradeText.text = "Osi¹gniêto maksymalny poziom";
        }
        shopLevelText.text = "Poziom: " + (level + 1) + " / " + (maxLevel + 1);
        if (currentColumn == 0)
        {
            PrintItems();
        }
        else
        {
            PrintEquipment();
        }
    }

    public void SetUpShop()
    {
        GameManager.instance.inGameCanvas.enabled = false;
        ShopManager.instance.shopCanvas.enabled = true;
        musicSource.clip = shopMusic;
        musicSource.loop = true;
        musicSource.Play();
        player.SetActive(false);
        PrintItems();
        moneyText.text = "Kasa: " + Inventory.Instance.Money + " PLN";
        shopLevelText.text = "Poziom: " + (level + 1) + " / " + (maxLevel + 1);
        amountToBuy = 0;
        items[currentRow].color = Color.white;
        currentRow = 0;
        currentPage = 0;
        items[currentRow].color = orange;
        paymentText.text = "0 x " + Inventory.Instance.items[currentRow].Cost + "PLN = 0 PLN";
        currentColumn = 0;
        categories[0].color = Color.red;
        categories[1].color = Color.white;
        controlsText.text = defaultControlsText;
        if (level < maxLevel && StoryManager.instance.currentMainQuest >= storyRequirementsForUpgrades[level])
        {
            upgradeText.text = "Ulepszenie: " + upgradeCosts[level] + " PLN";
            controlsText.text += controlsTextAddOn;
        }
        else if (level < maxLevel && StoryManager.instance.currentMainQuest < storyRequirementsForUpgrades[level])
        {
            upgradeText.text = "Kontynuuj fabu³ê, aby móc ulepszyæ";
        }
        else
        {
            upgradeText.text = "Osi¹gniêto maksymalny poziom";
        }
    }

    void ExitShop()
    {
        GameManager.instance.inGameCanvas.enabled = true;
        ShopManager.instance.shopCanvas.enabled = false;
        player.SetActive(true);
        musicSource.Stop();
        shopCanvas.enabled = false;
    }

    IEnumerator CantAfford()
    {
        moneyText.color = Color.red;
        moneyText.transform.localScale = new Vector3(1.25f, 1.25f);

        yield return new WaitForSeconds(0.2f);

        moneyText.color = Color.white;
        moneyText.transform.localScale = Vector3.one;
    }
}
