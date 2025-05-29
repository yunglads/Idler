using UnityEngine;

public class SkillBehavior : MonoBehaviour
{
    public Skill skill;
    public float timer;
    public float interval;
    public bool isActive = false;

    public bool generateInterval = false;

    void Update()
    {
        if (!isActive) return;

        if (!generateInterval)
        {
            GenerateGatherInterval();
        }

        timer += Time.deltaTime;

        if (timer >= interval && !skill.removalSkill)
        {
            timer = 0;
            InventoryManager.Instance.AddItem(skill.outputItem, 1);
            float xpBonus = EquipmentManager.Instance.GetXPBonusMultiplier(skill.skillType);
            SkillManager.Instance.AddXP(skill, skill.baseXPPerGather * xpBonus);

            if (!DragHandler.Instance.isDragging)
            {
                InventoryUIManager.Instance.Refresh();
            }
            

            generateInterval = false;

            print(skill + " current xp: " + skill.baseXPPerGather * xpBonus);
        }

        if (timer >= interval && skill.removalSkill)
        {
            timer = 0;
            InventoryManager.Instance.RemoveItem(skill.outputItem, 1);
            SkillManager.Instance.AddXP(skill, skill.baseXPPerGather);

            if (!DragHandler.Instance.isDragging)
            {
                InventoryUIManager.Instance.Refresh();
            }

            generateInterval = false;
        }

        if (skill.removalSkill && InventoryManager.Instance.GetAmount(skill.outputItem) < 1)
        {
            SkillManager.Instance.StopCurrentSkill();
            print("Not enough resource");
        }
    }

    public void ToggleActive()
    {
        isActive = !isActive;
    }

    public void RequestStart()
    {
        if (SkillManager.Instance.GetActiveSkill() == this)
        {
            SkillManager.Instance.StopCurrentSkill();
        }
        else if (SkillManager.Instance.GetActiveSkill() != this || SkillManager.Instance.GetActiveSkill() == this && !isActive)
        {
            SkillManager.Instance.StartSkill(this);
        }
    }

    public void StartSkill()
    {
        isActive = true;
        Debug.Log(skill.skillName + " started.");
        // Begin your coroutine or tick logic here
    }

    public void StopSkill()
    {
        isActive = false;
        Debug.Log(skill.skillName + " stopped.");
        // Stop coroutines/timers here
    }

    void GenerateGatherInterval()
    {
        float gatherInterval = Random.Range(skill.minInterval, skill.maxInterval);
        float speedMult = EquipmentManager.Instance.GetGatherSpeedMultiplier(skill.skillType);
        interval = gatherInterval / speedMult;

        generateInterval = true;
    }
}
