using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;
    private SkillBehavior activeSkill;

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
        skillXP[skill] += amount;

        // Optional: Leveling system
        int level = Mathf.FloorToInt(skillXP[skill] / 100);
        skillLevel[skill] = Mathf.Max(skillLevel.GetValueOrDefault(skill), level);
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

    public float GetXP(Skill skill) => skillXP.GetValueOrDefault(skill, 0);
    public int GetLevel(Skill skill) => skillLevel.GetValueOrDefault(skill, 0);
}
