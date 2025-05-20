using UnityEngine;

[System.Serializable]
public class GatherOutput
{
    public Item item;
    public int minAmount = 1;
    public int maxAmount = 10;
    public int setAmount;
    [Range(0f, 1f)] public float dropChance = 1f;
}
