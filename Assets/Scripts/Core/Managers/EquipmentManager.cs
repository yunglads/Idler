using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private Dictionary<EquipmentSlot, EquipmentItem> equipped = new();

    public void Equip(EquipmentItem item)
    {
        equipped[item.slot] = item;
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

    public float GetSurvivabilityMultiplier()
    {
        float bonus = 1f;
        foreach (var item in equipped.Values)
        {
            if (item != null)
            {
                bonus *= item.survivabilityMultiplier;
            }
        }
        return bonus;
    }
}

