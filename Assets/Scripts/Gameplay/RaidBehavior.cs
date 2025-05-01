using UnityEngine;

public class RaidBehavior : MonoBehaviour
{
    public Raid raid;
    private float timer;
    public bool isActive = false;
    //public bool resourceRemovalSkill = false;

    void Update()
    {
        if (!isActive) return;

        float gatherInterval = Random.Range(raid.minInterval, raid.maxInterval);
        
        //float speedMult = EquipmentManager.Instance.GetGatherSpeedMultiplier(skill.skillType);
        //float interval = gatherInterval / speedMult;

        timer += Time.deltaTime;

        if (timer >= gatherInterval)
        {
            timer = 0;
            foreach (var lootItem in raid.outputItems)
            {
                if (Random.value <= lootItem.dropChance)
                {
                    int randomAmount = Random.Range(lootItem.minAmount, lootItem.maxAmount);
                    InventoryManager.Instance.AddItem(lootItem.item, randomAmount);
                }          
            }
            
            //float xpBonus = EquipmentManager.Instance.GetXPBonusMultiplier(raid.skillType);
            //SkillManager.Instance.AddXP(raid, raid.baseXPPerGather * xpBonus);
            InventoryUIManager.Instance.Refresh();
            ToggleActive();

            //print(raid + " current xp: " + xpBonus.ToString());
        }
    }

    public void ToggleActive()
    {
        isActive = !isActive;
    }
}
