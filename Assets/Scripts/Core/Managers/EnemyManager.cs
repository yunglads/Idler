using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyStats> enemyTypes = new List<EnemyStats>();

    public EnemyStats GetRandomEnemy()
    {
        //int index = Random.Range(0, enemyTypes.Count);
        //return enemyTypes[index]; // Optionally clone if you want a fresh instance

        float totalChance = 0f;

        foreach (var enemy in enemyTypes)
        {
            totalChance += enemy.spawnChance;
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
