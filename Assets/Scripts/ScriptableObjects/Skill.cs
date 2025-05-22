using UnityEngine;
using UnityEngine.UI;

public enum SkillType { Mining, Logging, Pistol, Focus }

[CreateAssetMenu(fileName = "New Skill", menuName = "IdleGame/Skill")]

public class Skill : ScriptableObject
{
    public string skillName;
    public SkillType skillType;
    public Item outputItem;
    public Sprite skillIcon;
    public float minInterval = 2f;
    public float maxInterval = 5f;
    public int baseXPPerGather = 5;
    public bool removalSkill = false;
}