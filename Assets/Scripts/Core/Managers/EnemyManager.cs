using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyStats> enemyTypes = new List<EnemyStats>();

    public EnemyStats GetRandomEnemy()
    {
        float totalChance = 0f;

        foreach (var enemy in enemyTypes)
        {
            totalChance += enemy.spawnChance;

            if (enemy.health <= 0)
            {
                enemy.health = enemy.maxHealth;
            }
        }

        float roll = Random.Range(0f, totalChance);
        float cumulative = 0f;

        foreach (var enemy in enemyTypes)
        {
            cumulative += enemy.spawnChance;
            if (roll <= cumulative)
            {
                return enemy;
            }
        }

        // Fallback (shouldn't happen if data is correct)
        return enemyTypes[0];
    }
}
