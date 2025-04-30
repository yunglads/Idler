using UnityEngine;

public enum SkillType { Mining, Logging }

[CreateAssetMenu(fileName = "New Skill", menuName = "IdleGame/Skill")]

public class Skill : ScriptableObject
{
    public string skillName;
    public SkillType skillType;
    public Item outputItem;
    public float minInterval = 2f;
    public float maxInterval = 5f;
    //public float gatherInterval = Random.Range(2f, 5f);
    public int baseXPPerGather = 5;
    public bool removalSkill = false;
}
