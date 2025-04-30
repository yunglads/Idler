using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillUIHandler : MonoBehaviour
{
    public TextMeshProUGUI skillNameText;
    public TextMeshProUGUI levelText;
    public Slider xpSlider;
    public Button toggleButton;
    public TextMeshProUGUI toggleButtonText;
    //public TextMeshProUGUI itemText;

    private SkillBehavior skillBehavior;
    private Skill skill;

    public void Setup(Skill skill, SkillBehavior behavior)
    {
        this.skill = skill;
        this.skillBehavior = behavior;
        skillNameText.text = skill.skillName;

        toggleButton.onClick.AddListener(() =>
        {
            skillBehavior.ToggleActive();
            UpdateToggleText();
        });
    }

    void Update()
    {
        float xp = SkillManager.Instance.GetXP(skill);
        int level = SkillManager.Instance.GetLevel(skill);

        levelText.text = $"Lvl {level}";
        xpSlider.value = xp % 100f / 100f;

        double amount = InventoryManager.Instance.GetAmount(skill.outputItem);
        //itemText.text = $"{skill.outputItem.itemName}: {amount:0}";

        UpdateToggleText();
    }

    void UpdateToggleText()
    {
        toggleButtonText.text = skillBehavior.isActive ? "Stop" : "Start";
    }
}

