using System.Collections.Generic;
using UnityEngine;

public class DebugInventory : MonoBehaviour
{
    public Item[] items;

    //public List<EquipmentSlot> equipmentSlots;

    void Start()
    {
        foreach (var item in items)
        {
            InventoryManager.Instance.AddItem(item, 1);
            Debug.Log("Added item: " + item.name);
        }
    }
}
