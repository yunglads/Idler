using UnityEngine;

public class DebugInventory : MonoBehaviour
{
    public Item testItem;
    public Item testItem1;

    void Start()
    {
        InventoryManager.Instance.AddItem(testItem, 1);
        Debug.Log("Added item: " + testItem.name);
        InventoryManager.Instance.AddItem(testItem1, 1);
        Debug.Log("Added item: " + testItem1.name);
    }
}
