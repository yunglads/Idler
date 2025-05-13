using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Raid", menuName = "IdleGame/Raid")]

public class Raid : ScriptableObject
{
    public string raidName;
    //public SkillType skillType;
    public List<GatherOutput> outputItems;
    public float minInterval = 5f;
    public float maxInterval = 10f;
    public int baseXPPerGather = 5;
    public int minEncounters = 1;
    public int maxEncounters = 2;
    public float enemyHealth = 100f;
    public float enemyDamage = 1.0f;
    public float enemyDefense = 1.0f;
    //[Range(0f, 1f)] public float survivalRate = 1f;
    //public bool removalSkill = false;
}
