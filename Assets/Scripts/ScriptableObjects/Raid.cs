using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Raid", menuName = "IdleGame/Raid")]
public class Raid : ScriptableObject
{
    public string raidName;
    public List<GatherOutput> outputItems;
    //public List<EnemyStats> enemies;
    public float minInterval = 5f;
    public float maxInterval = 10f;
    //public int baseXPPerGather = 5;
    public int minEncounters = 1;
    public int maxEncounters = 2;
}
