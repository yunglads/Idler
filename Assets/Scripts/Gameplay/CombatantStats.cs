using UnityEngine;

public class CombatantStats : MonoBehaviour
{
    public int damage;
    public int defense;
    public int health;

    [Range(0f, 1f)] public float critChance = 0.1f;   // 10% crit by default
    public float critMultiplier = 2f;                 // Double damage on crit

    [Range(0f, 1f)] public float dodgeChance = 0.05f; // 5% chance to dodge
    public float damageVariance = 0.1f;               // ±10% variance
}

