using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform inventoryGrid;

    public static InventoryUIManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        foreach (Transform child in inventoryGrid)
            Destroy(child.gameObject);

        foreach (var pair in InventoryManager.Instance.GetAllItems())
        {
            GameObject go = Instantiate(slotPrefab, inventoryGrid);
            InventorySlotUI ui = go.GetComponent<InventorySlotUI>();
            ui.Setup(pair.Key, pair.Value);
        }
    }
}

