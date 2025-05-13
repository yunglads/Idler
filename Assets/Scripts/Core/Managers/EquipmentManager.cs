using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;

    public EquipmentSlotUI[] slots;
    public List<EquipmentItem> equipmentItems;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        foreach (var slot in slots)
        {
            slot.Clear();
        }
    }

    private Dictionary<EquipmentSlot, EquipmentItem> equipped = new();

    public void Equip(EquipmentItem item)
    {
        equipped[item.slot] = item;

        equipmentItems.Add(item);

        ApplyBonus(item);
    }

    public void Unequip(EquipmentItem item)
    {
        PlayerStats.Instance.damage -= item.bonusDamage;
        PlayerStats.Instance.defense -= item.bonusDefense;

        equipped[item.slot] = null;

        equipmentItems.Remove(item);
    }

    public void RemoveAllEquipment()
    {
        foreach (var equipedItem in new List<EquipmentItem>(equipmentItems))
        {
            if (equipedItem.slot != EquipmentSlot.Tool)
            {
                RemoveBonus(equipedItem);
                equipped[equipedItem.slot] = null;
                equipmentItems.Remove(equipedItem);
            }
        }

        foreach (var slot in slots)
        {
            if (slot.slotType != EquipmentSlot.Tool)
            {
                slot.Clear();
            }
        }

        print("Operation finished");
    }

    public EquipmentItem GetEquippedItem(EquipmentSlot slot)
    {
        return equipped.ContainsKey(slot) ? equipped[slot] : null;
    }

    public float GetXPBonusMultiplier(SkillType skill)
    {
        float bonus = 1f;
        foreach (var item in equipped.Values)
        {
            if (item !=  null && item.relevantSkills.Contains(skill)) 
            {
                bonus *= item.xpGainMultiplier;
            }
        }        
        return bonus;
    }

    public float GetGatherSpeedMultiplier(SkillType skill)
    {
        float bonus = 1f;
        foreach (var item in equipped.Values)
        {
            if (item != null && item.relevantSkills.Contains(skill))
            {
                bonus *= item.xpGainMultiplier;
            }
        }
        return bonus;
    }

    public void ApplyBonus(EquipmentItem item)
    {
        PlayerStats.Instance.damage += item.bonusDamage;
        PlayerStats.Instance.defense += item.bonusDefense;
    }

    public void RemoveBonus(EquipmentItem item)
    {
        PlayerStats.Instance.damage -= item.bonusDamage;
        PlayerStats.Instance.defense -= item.bonusDefense;
    }
}

