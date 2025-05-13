using System.Collections.Generic;
using UnityEngine;

public enum EquipmentSlot { Tool, Armor, Accessory, Weapon, Helmet, Backpack, Melee }

[CreateAssetMenu(menuName = "IdleGame/Equipment Item")]
public class EquipmentItem : Item
{
    public EquipmentSlot slot;
    public List<SkillType> relevantSkills;

    [Header("Bonuses")]
    public float gatherSpeedMultiplier = 1f;
    public float xpGainMultiplier = 1f;
    public int bonusDamage = 0;
    public int bonusDefense = 0;
}

