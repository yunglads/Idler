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

        if (behavior != null && !behavior.isActive) 
        {
            closeUIButton.gameObject.SetActive(true);
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

        EnemyStats baseEnemy = enemyManager.GetRandomEnemy();

        currentEnemy = baseEnemy;

        foreach (RaidBehavior raid in raids)
        {
            if (raid.isActive)
            {
                behavior = raid;
            }
        }

        StartCoroutine(FightLoop());
    }

    public IEnumerator FightLoop(float interval = 3f)
    {
        while (player.health > 0 && currentEnemy.health > 0)
        {
            yield return new WaitForSeconds(interval);

            // Player attacks
            SimulateAttack(player, currentEnemy, "Player", currentEnemy.enemyName);
            if (currentEnemy.health <= 0)
            {
                currentEnemy.health = 0;
                behavior.raidSuccessful = true;
                Debug.Log("Enemy defeated!");
                yield break; // Stop the loop
            }

            yield return new WaitForSeconds(interval);

            // Enemy attacks
            SimulateAttack(currentEnemy, player, currentEnemy.enemyName, "Player");
            if (player.health <= 0)
            {
                player.health = 0;
                behavior.raidSuccessful = false;
                Debug.Log("Player defeated!");
                yield break; // Stop the loop
            }

            yield return new WaitForSeconds(interval);
        }
    }
}
