using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public bool CanCraft(CraftingRecipe recipe)
    {
        foreach (var ingredient in recipe.ingredients)
        {
            if (!InventoryManager.Instance.HasItem(ingredient.item, ingredient.amount))
                return false;
        }
        return true;
    }

    public void Craft(CraftingRecipe recipe)
    {
        if (!CanCraft(recipe)) return;

        foreach (var ingredient in recipe.ingredients)
        {
            InventoryManager.Instance.RemoveItem(ingredient.item, ingredient.amount);
        }

        InventoryManager.Instance.AddItem(recipe.result, recipe.resultAmount);
        InventoryUIManager.Instance.Refresh();
    }
}

