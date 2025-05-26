using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour, IDropHandler
{
    public static InventoryManager Instance;
    
    private List<InventoryItem> items = new();
    public List<InventoryItem> GetAllItems() => items;

    public static event Action OnInventoryChanged;

    public int maxInventorySize = 20;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }   

    public bool AddItem(Item item, int count = 1)
    {
        if (item == null || count <= 0)
            return false;  // invalid input

        int currentCount = GetTotalItemStacks();

        // If no space for new stacks and no space in existing stacks, refuse add
        if (currentCount >= maxInventorySize && !HasStackSpaceFor(item))
        {
            Debug.Log("Inventory full!");
            return false;
        }

        if (item.isStackable)
        {
            // Fill existing stacks first
            foreach (var stack in items)
            {
                if (stack.item == item && !stack.IsFull)
                {
                    int spaceLeft = item.maxStack - stack.count;
                    int toAdd = Mathf.Min(spaceLeft, count);
                    stack.count += toAdd;
                    count -= toAdd;
                    if (count <= 0)
                        return true; // done adding all items
                }
            }
        }

        // Create new stacks for remaining items
        while (count > 0 && items.Count < maxInventorySize)
        {
            int toAdd = item.isStackable ? Mathf.Min(item.maxStack, count) : 1;
            items.Add(new InventoryItem(item, toAdd));
            count -= toAdd;
        }

        if (count > 0)
        {
            Debug.LogWarning("Not enough inventory space to add all items.");
            return false;
        }

        InventoryUIManager.Instance.Refresh();
        OnInventoryChanged?.Invoke();
        return true;
    }

    public void RemoveItem(Item item, int count = 1)
    {
        for (int i = items.Count - 1; i >= 0 && count > 0; i--)
        {
            if (items[i].item == item)
            {
                if (items[i].count > count)
                {
                    items[i].count -= count;
                    return;
                }
                else
                {
                    count -= items[i].count;
                    items.RemoveAt(i);
                }
            }
        }
        InventoryUIManager.Instance.Refresh();
        OnInventoryChanged?.Invoke();
    }

    public int GetAmount(Item item)
    {
        int total = 0;
        foreach (var invItem in items)
        {
            if (invItem.item == item)
                total += invItem.count;
        }
        return total;
    }

    public bool HasItem(Item item, int count)
    {
        return GetAmount(item) >= count;
    }

    private int GetTotalItemStacks()
    {
        return items.Count;
    }

    private bool HasStackSpaceFor(Item item)
    {
        if (!item.isStackable)
            return false;

        foreach (var invItem in items)
        {
            if (invItem.item == item && invItem.count < item.maxStack)
                return true;
        }
        return false;
    }

    public void UpgradeInventorySize(int amount)
    {
        maxInventorySize += amount;
        // Clamp if needed, e.g. max limit
        maxInventorySize = Mathf.Clamp(maxInventorySize, 1, 100); // example max

        InventoryUIManager.Instance.Refresh();
        OnInventoryChanged?.Invoke();
    }

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
        }
    }
}

