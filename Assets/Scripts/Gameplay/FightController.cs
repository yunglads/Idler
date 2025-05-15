using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FightController : MonoBehaviour
{
    public static FightController Instance;

    public EnemyManager enemyManager;
    public PlayerStats player;
    public EnemyStats currentEnemy;

    public RaidBehavior[] raids;
    public RaidBehavior behavior;

    public int d20Roll;
    public int damageDealt;
    public string attackResult;
    public Button closeUIButton;

    private int totalEncounters;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);       
    }

    private void Update()
    {
        if (raids == null || raids.Length == 0)
        {
            raids = FindObjectsByType<RaidBehavior>(FindObjectsSortMode.None);
            print("looking for raids");
        }
    }

    private void SimulateAttack(CombatantStats attacker, CombatantStats defender, string attackerName, string defenderName)
    {
        // Evasion check
        if (Random.value < defender.dodgeChance)
        {
            //Debug.Log($"{defenderName} dodged the attack from {attackerName}!");
            attackResult = $"{defenderName} dodged the attack from {attackerName}!";
            return;
        }

        d20Roll = Random.Range(1, 20);

        // Base damage
        float baseDamage = Mathf.Max(0, attacker.damage + d20Roll - defender.defense);

        // Variance
        float variance = Random.Range(1f - attacker.damageVariance, 1f + attacker.damageVariance);
        float variedDamage = baseDamage * variance;

        // Critical hit check
        bool isCrit = Random.value < attacker.critChance;
        float finalDamage = isCrit ? variedDamage * attacker.critMultiplier : variedDamage;

        // Round and apply damage
        damageDealt = Mathf.RoundToInt(finalDamage);
        defender.health -= damageDealt;

        // Log result
        string critText = isCrit ? " (Critical!)" : "";
        //Debug.Log($"{attackerName} hits {defenderName} for {damageDealt} damage{critText}. {defenderName} health: {defender.health}");
        attackResult = $"{attackerName} hits {defenderName} for {damageDealt} damage{critText}. {defenderName} health: {defender.health}";
    }

    public void StartRaid()
    {
        closeUIButton.gameObject.SetActive(false);

        //EnemyStats baseEnemy = enemyManager.GetRandomEnemy();
        //currentEnemy = baseEnemy;

        foreach (RaidBehavior raid in raids)
        {
            if (raid.isActive)
            {
                behavior = raid;
            }
        }

        totalEncounters = Random.Range(behavior.raid.minEncounters, behavior.raid.maxEncounters);
        print("Total Encounters: " + totalEncounters);

        StartCoroutine(FightLoop());
    }

    public IEnumerator FightLoop(float interval = 2f)
    {
        while (totalEncounters > 0)
        {
            EnemyStats baseEnemy = enemyManager.GetRandomEnemy();
            currentEnemy = baseEnemy;

            while (player.health > 0 && currentEnemy.health > 0)
            {
                yield return new WaitForSeconds(interval);

                // Player attacks
                SimulateAttack(player, currentEnemy, "Player", currentEnemy.enemyName);
                if (currentEnemy.health <= 0)
                {
                    currentEnemy.health = 0;
                    //behavior.raidSuccessful = true;
                    totalEncounters--;
                    Debug.Log("Enemy defeated!");
                    print("Remaining Encounters: " + totalEncounters);
                    break; // Stop the loop
                }

                yield return new WaitForSeconds(interval);

                // Enemy attacks
                SimulateAttack(currentEnemy, player, currentEnemy.enemyName, "Player");
                if (player.health <= 0)
                {
                    player.health = 0;
                    behavior.raidSuccessful = false;
                    totalEncounters = 0;
                    Debug.Log("Player defeated!");
                    break; // Stop the loop
                }

                yield return new WaitForSeconds(interval);
            }

            //totalEncounters--;

            if (totalEncounters > 0)
            {
                float waitTime = Random.Range(3, 10);
                Debug.Log($"Waiting {waitTime:F1}s until next encounter...");
                yield return new WaitForSeconds(waitTime);
            }
        }

        behavior.raidSuccessful = true;
        closeUIButton.gameObject.SetActive(true);
        Debug.Log("Raid complete!");
    }
}
