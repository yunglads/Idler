using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class RaidBehavior : MonoBehaviour
{
    public Raid raid;
    private float timer;
    public bool isActive = false;
    public bool raidSuccessful = true;

    void Update()
    {
        if (!isActive) return;

        float gatherInterval = Random.Range(raid.minInterval, raid.maxInterval);

        timer += Time.deltaTime;

        if (isActive && timer >= gatherInterval && raidSuccessful)
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
        else if (isActive && !raidSuccessful)
        {
            EquipmentManager.Instance.RemoveAllEquipment();   
            timer = 0;
            raidSuccessful = true;
            ToggleActive();
        }
    }

    public void ToggleActive()
    {
        isActive = !isActive;
    }
}
