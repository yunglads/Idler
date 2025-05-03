using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Mathematics;

public class SkillUIHandler : MonoBehaviour
{
    public TextMeshProUGUI skillNameText;
    public TextMeshProUGUI levelText;
    public Slider timerSlider;
    public Button toggleButton;
    public TextMeshProUGUI toggleButtonText;
    public Image skillIcon;
    //public TextMeshProUGUI itemText;

    private SkillBehavior skillBehavior;
    private Skill skill;

    public void Setup(Skill skill, SkillBehavior behavior)
    {
        this.skill = skill;
        this.skillBehavior = behavior;
        skillNameText.text = skill.skillName;
        skillIcon.sprite = skill.skillIcon;

        if (!behavior.isActive)
        {
            toggleButton.onClick.AddListener(() =>
            {
                OnClickStartSkill();
                UpdateToggleText();
            });
        } 
    }

    void Update()
    {
        int level = SkillManager.Instance.GetLevel(skill);
        levelText.text = $"Lvl {level}";

        float setTimer = skillBehavior.timer;
        float setInterval = skillBehavior.interval;
        if (setTimer > 0 && setInterval > 0) 
        {
            timerSlider.value = setTimer % setInterval / setInterval;
        }
        else
        {
            timerSlider.value = 0f;
        }
        
        print(setTimer + " : " +  setInterval);

        //int amount = InventoryManager.Instance.GetAmount(skill.outputItem);
        //itemText.text = $"{skill.outputItem.itemName}: {amount:0}";

        UpdateToggleText();
    }

    void UpdateToggleText()
    {
        toggleButtonText.text = skillBehavior.isActive ? "Stop" : "Start";
    }

    public void OnClickStartSkill()
    {
        skillBehavior.RequestStart();
    }
}

