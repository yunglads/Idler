using System.Collections;
using UnityEngine;

public class FightController : MonoBehaviour
{
    public static FightController Instance;

    public EnemyManager enemyManager;
    public PlayerStats player;
    public EnemyStats currentEnemy;

    public RaidBehavior[] raids;
    public RaidBehavior behavior;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);       
    }

    private void Update()
    {
        if(raids == null || raids.Length == 0)
        {
            raids = FindObjectsByType<RaidBehavior>(FindObjectsSortMode.None);
            print("looking for raids");
        }
    }

    public void Fight()
    {
        SimulateAttack(player, currentEnemy, "Player", currentEnemy.enemyName);
        if (currentEnemy.health <= 0)
        {
            behavior.raidSuccessful = true;
            
            Debug.Log("Enemy defeated!");
            return;
        }

        SimulateAttack(currentEnemy, player, currentEnemy.enemyName, "Player");
        if (player.health <= 0)
        {
            behavior.raidSuccessful = false;             
            Debug.Log("Player defeated!");
        }
    }

    private void SimulateAttack(CombatantStats attacker, CombatantStats defender, string attackerName, string defenderName)
    {
        // Evasion check
        if (Random.value < defender.dodgeChance)
        {
            Debug.Log($"{defenderName} dodged the attack from {attackerName}!");
            return;
        }

        int d20Roll = Random.Range(1, 20);

        // Base damage
        float baseDamage = Mathf.Max(0, attacker.damage + d20Roll - defender.defense);

        // Variance
        float variance = Random.Range(1f - attacker.damageVariance, 1f + attacker.damageVariance);
        float variedDamage = baseDamage * variance;

        // Critical hit check
        bool isCrit = Random.value < attacker.critChance;
        float finalDamage = isCrit ? variedDamage * attacker.critMultiplier : variedDamage;

        // Round and apply damage
        int damageDealt = Mathf.RoundToInt(finalDamage);
        defender.health -= damageDealt;

        // Log result
        string critText = isCrit ? " (Critical!)" : "";
        Debug.Log($"{attackerName} hits {defenderName} for {damageDealt} damage{critText}. {defenderName} health: {defender.health}");
    }

    public void StartRaid()
    {
        EnemyStats baseEnemy = enemyManager.GetRandomEnemy();

        currentEnemy = baseEnemy;

        //currentEnemy = new EnemyStats
        //{
        //    health = baseEnemy.health,
        //    damage = baseEnemy.damage,
        //    defense = baseEnemy.defense
        //};

        foreach (RaidBehavior raid in raids)
        {
            if (raid.isActive)
            {
                behavior = raid;
            }
        }

        StartCoroutine(FightLoop());
    }

    public IEnumerator FightLoop(float interval = 1f)
    {
        while (player.health > 0 && currentEnemy.health > 0)
        {
            Fight();
            yield return new WaitForSeconds(interval);
        }
    }
}
