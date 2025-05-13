using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RaidUIHandler : MonoBehaviour
{
    public TextMeshProUGUI raidNameText;
    public TextMeshProUGUI raidTimeText;
    //public TextMeshProUGUI raidSurvivalRateText;
    public Transform lootPanel;
    public GameObject lootIconPrefab;
    //public Slider xpSlider;
    public Button toggleButton;
    public TextMeshProUGUI toggleButtonText;
    //public TextMeshProUGUI itemText;

    private RaidBehavior raidBehavior;
    private Raid raid;
    public UIManager uiManager;

    private void Start()
    {
        if (uiManager == null)
        {
            uiManager = FindFirstObjectByType<UIManager>();
        }   
    }

    public void Setup(Raid raid, RaidBehavior behavior)
    {
        this.raid = raid;
        this.raidBehavior = behavior;
        raidNameText.text = raid.raidName;

        // Add new ingredient icons
        foreach (var loots in raid.outputItems)
        {
            GameObject iconObj = Instantiate(lootIconPrefab, lootPanel);
            Image iconImage = iconObj.GetComponent<Image>();
            TextMeshProUGUI countText = iconObj.GetComponentInChildren<TextMeshProUGUI>();

            iconImage.sprite = loots.item.icon;
            countText.text = loots.minAmount.ToString() + " - " + loots.maxAmount.ToString();
        }

        toggleButton.onClick.AddListener(() =>
        {
            raidBehavior.ToggleActive();
            uiManager.OpenFightUI();
            FightController.Instance.StartRaid();
            UpdateToggleText();
        });
    }

    void Update()
    {
        //float xp = SkillManager.Instance.GetXP(raid);
        //int level = SkillManager.Instance.GetLevel(raid);

        raidTimeText.text = raid.minInterval + " - " + raid.maxInterval;
        //raidSurvivalRateText.text = raid.survivalRate * 100 + "%";
        //xpSlider.value = xp % 100f / 100f;

        //double amount = InventoryManager.Instance.GetAmount(raid.outputItem);
        //itemText.text = $"{skill.outputItem.itemName}: {amount:0}";

        UpdateToggleText();


        if (raidBehavior.isActive)
        {
            toggleButton.enabled = false;
            return;
        }
        else
        {
            toggleButton.enabled = true;
        }
    }

    void UpdateToggleText()
    {
        toggleButtonText.text = raidBehavior.isActive ? "Stop" : "Start";
    }
}
