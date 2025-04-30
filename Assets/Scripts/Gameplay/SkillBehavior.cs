using UnityEngine;

public class SkillBehavior : MonoBehaviour
{
    public Skill skill;
    private float timer;
    public bool isActive = false;
    //public bool resourceRemovalSkill = false;

    void Update()
    {
        if (!isActive) return;

        float gatherInterval = Random.Range(skill.minInterval, skill.maxInterval);
        float speedMult = EquipmentManager.Instance.GetGatherSpeedMultiplier(skill.skillType);
        float interval = gatherInterval / speedMult;

        timer += Time.deltaTime;

        if (timer >= interval && !skill.removalSkill)
        {
            timer = 0;
            InventoryManager.Instance.AddItem(skill.outputItem, 1);
            float xpBonus = EquipmentManager.Instance.GetXPBonusMultiplier(skill.skillType);
            SkillManager.Instance.AddXP(skill, skill.baseXPPerGather * xpBonus);
            InventoryUIManager.Instance.Refresh();

            print(skill + " current xp: " + xpBonus.ToString());
        }

        if (timer >= interval && skill.removalSkill)
        {
            timer = 0;
            InventoryManager.Instance.RemoveItem(skill.outputItem, 1);
            SkillManager.Instance.AddXP(skill, skill.baseXPPerGather);
            InventoryUIManager.Instance.Refresh();
        }

        if (skill.removalSkill && InventoryManager.Instance.GetAmount(skill.outputItem) < 1)
        {
            ToggleActive();
            print("Not enough resource");
        }
    }

    public void ToggleActive()
    {
        isActive = !isActive;
    }
}
