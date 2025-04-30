using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;

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

    public float GetXP(Skill skill) => skillXP.GetValueOrDefault(skill, 0);
    public int GetLevel(Skill skill) => skillLevel.GetValueOrDefault(skill, 0);
}
