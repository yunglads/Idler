using UnityEngine;

public class SkillUIManager : MonoBehaviour
{
    public Skill[] allSkills;
    public GameObject skillUIPrefab;
    public Transform skillUIParent;

    void Start()
    {
        foreach (Skill skill in allSkills)
        {
            GameObject skillGO = new GameObject(skill.skillName);
            SkillBehavior behavior = skillGO.AddComponent<SkillBehavior>();
            behavior.skill = skill;

            GameObject ui = Instantiate(skillUIPrefab, skillUIParent);
            SkillUIHandler handler = ui.GetComponent<SkillUIHandler>();
            handler.Setup(skill, behavior);
        }
    }
}

