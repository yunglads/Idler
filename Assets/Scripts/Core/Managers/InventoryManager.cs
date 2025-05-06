using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour, IDropHandler
{
    public static InventoryManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private Dictionary<Item, int> items = new();

    public void AddItem(Item item, int count = 1)
    {
        if (!items.ContainsKey(item))
            items[item] = 0;
        items[item] += count;
    }

    public void RemoveItem(Item item, int count = 1)
    {
        if (!items.ContainsKey(item)) return;
        items[item] -= count;
        if (items[item] <= 0)
            items.Remove(item);
    }

    public int GetAmount(Item item)
    {
        return items.TryGetValue(item, out int val) ? val : 0;
        //If error: check folder ScriptableObject>Skills and make sure skill has output item
    }

    public bool HasItem(Item item, int count) =>
    items.ContainsKey(item) && items[item] >= count;

    public Dictionary<Item, int> GetAllItems() => items;

    public void OnDrop(PointerEventData eventData)
    {
        EquipmentSlotUI dragged = DragHandler.Instance.equipDragSource;
        if (dragged != null && dragged.item is EquipmentItem equip)
        {
            AddItem(equip);
            EquipmentManager.Instance.Unequip(equip);
            dragged.Clear();
            DragHandler.Instance.EndDrag();
            InventoryUIManager.Instance.Refresh();

            //if (equip.slot == slotType)
            //{
            //    EquipmentManager.Instance.Equip(equip);
            //    icon.sprite = equip.icon;
            //    icon.enabled = true;

            //    InventoryManager.Instance.AddItem(equip);
            //    dragged.Clear();
            //}
        }
    }
}

