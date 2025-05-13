using UnityEngine;

public class EnemyStats : CombatantStats
{
    public string enemyName;

    [Range(0f, 1f)]public float spawnChance = 0.5f;

    //private void Start()
    //{
    //    enemyName = gameObject.name;
    //}
}
