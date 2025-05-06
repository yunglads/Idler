using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RaidBehavior : MonoBehaviour
{
    public Raid raid;
    private float timer;
    public bool isActive = false;
    private bool raidSuccessful = true;

    void Update()
    {
        if (!isActive) return;

        float gatherInterval = Random.Range(raid.minInterval, raid.maxInterval);

        timer += Time.deltaTime;

        if (Random.value <= raid.survivalRate)
        {
            raidSuccessful = true;
            
            print("Raid sucessful");
        }
        else
        {
            raidSuccessful = false;
            print("Raid failed");
        }

        if (timer >= gatherInterval && raidSuccessful)
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
            InventoryUIManager.Instance.Refresh();
            ToggleActive();
        }
        else if (timer >= gatherInterval && !raidSuccessful)
        {
            EquipmentManager.Instance.RemoveAllEquipment();
            timer = 0;
            ToggleActive();
        }
    }

    public void ToggleActive()
    {
        isActive = !isActive;
    }
}
