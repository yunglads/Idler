[System.Serializable]
public class InventoryItem
{
    public Item item;
    public int count;

    public InventoryItem(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }

    public bool IsFull => item.isStackable && count >= item.maxStack;
}
