using UnityEngine;

public class PlayerStats : CombatantStats
{
    public static PlayerStats Instance;

    public int maxHealth = 100;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PayForHeals()
    {
        health = maxHealth;
    }
}
