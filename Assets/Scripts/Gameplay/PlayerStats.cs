using TMPro;
using UnityEngine;

public class PlayerStats : CombatantStats
{
    public static PlayerStats Instance;

    public Sprite playerIcon;
    private int maxHealth = 100;
    public int hunger = 100;
    private int maxHunger = 100;
    public int thirst = 100;
    private int maxThirst = 100;

    public TMP_Text healthText;
    public TMP_Text hungerText;
    public TMP_Text thirstText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        healthText.text = health + "/" + maxHealth;
        hungerText.text = hunger + "/" + maxHunger;
        thirstText.text = thirst + "/" + maxThirst;
    }
}
