using UnityEngine;

public enum ItemType
{
    Resource,
    Tool,
    Armor,
    Weapon,
    Consumable
}

[CreateAssetMenu(menuName = "IdleGame/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    [TextArea] public string itemDesc;
    public Sprite icon;
    public ItemType itemType;
    public bool isStackable = true;
    public int maxStack = 99;
}

