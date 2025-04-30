using TMPro;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    public Skill skill;
    public TMP_Text skillText;

    void Update()
    {
        if (skill != null && skillText != null)
        {
            int level = SkillManager.Instance.GetLevel(skill);
            skillText.text = skill.ToString() + ": " + level.ToString();
        }
    }
}
