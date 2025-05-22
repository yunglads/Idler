using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;
    private SkillBehavior activeSkill;

    [Tooltip("XP needed to reach Level 1")]
    public float baseXP = 100f; 
    [Tooltip("Controls level curve steepness")]
    public float exponent = 1.5f; 

    public Dictionary<Skill, float> skillXP = new();
    public Dictionary<Skill, int> skillLevel = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddXP(Skill skill, float amount)
    {
        if (!skillXP.ContainsKey(skill)) skillXP[skill] = 0;
        if (!skillLevel.ContainsKey(skill)) skillLevel[skill] = 0;

        skillXP[skill] += amount;
        float currentXP = skillXP[skill];
        int currentLevel = skillLevel[skill];

        // Check if the skill should level up
        while (currentXP >= GetXPForLevel(currentLevel + 1))
        {
            currentLevel++;
            Debug.Log($"{skill.name} leveled up to {currentLevel}!");
        }

        skillLevel[skill] = currentLevel;
    }

    public void StartSkill(SkillBehavior skillToStart)
    {
        if (activeSkill != null && activeSkill != skillToStart)
        {
            StopCurrentSkill();
        }

        activeSkill = skillToStart;
        activeSkill.StartSkill();
    }

    public void StopCurrentSkill()
    {
        if (activeSkill != null)
        {
            activeSkill.StopSkill();
            activeSkill = null;
        }
    }

    public SkillBehavior GetActiveSkill()
    {
        return activeSkill;
    }

    private float GetXPForLevel(int level)
    {
        return baseXP * Mathf.Pow(level, exponent);
    }

    //Optional functions for UI
    public float GetXPToNextLevel(Skill skill)
    {
        int currentLevel = GetLevel(skill);
        float xpForNext = GetXPForLevel(currentLevel + 1);
        return xpForNext - GetXP(skill);
    }

    public float GetXPPercentToNextLevel(Skill skill)
    {
        int level = GetLevel(skill);
        float currentXP = GetXP(skill);
        float prevXP = GetXPForLevel(level);
        float nextXP = GetXPForLevel(level + 1);

        return (currentXP - prevXP) / (nextXP - prevXP);
    }

    public float GetXP(Skill skill) => skillXP.GetValueOrDefault(skill, 0);
    public int GetLevel(Skill skill) => skillLevel.GetValueOrDefault(skill, 0);
}
