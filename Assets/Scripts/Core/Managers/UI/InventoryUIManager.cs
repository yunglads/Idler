using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform inventoryGrid;

    public CraftingUIManager[] craftingUIManagers;

    public static InventoryUIManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        craftingUIManagers = FindObjectsByType<CraftingUIManager>(FindObjectsSortMode.None);
        Refresh();
    }

    public void Refresh()
    {
        foreach (Transform child in inventoryGrid)
            Destroy(child.gameObject);

        List<InventoryItem> allItems = InventoryManager.Instance.GetAllItems();

        int maxSlots = InventoryManager.Instance.maxInventorySize;

        for (int i = 0; i < maxSlots; i++)
        {
            GameObject go = Instantiate(slotPrefab, inventoryGrid);
            InventorySlotUI ui = go.GetComponent<InventorySlotUI>();

            if (i < allItems.Count)
            {
                ui.Setup(allItems[i]);
            }
            else
            {
                ui.Setup(null); // Empty slot
            }
        }

        foreach (var manager in craftingUIManagers)
            manager.RefreshAllRecipes();
    }
}

