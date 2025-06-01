using System.Collections.Generic;
using UnityEngine;

public class CraftingUIManager : MonoBehaviour
{
    public List<CraftingRecipe> availableRecipes;
    public GameObject recipeUIPrefab;
    public Transform recipeListParent;

    private List<CraftingRecipeUI> recipeUIs = new();

    void Start()
    {
        foreach (var recipe in availableRecipes)
        {
            var go = Instantiate(recipeUIPrefab, recipeListParent);
            var recipeUI = go.GetComponent<CraftingRecipeUI>();
            recipeUI.Setup(recipe);
            recipeUIs.Add(recipeUI);
        }

        print("subscribing to inventory change");
        InventoryManager.OnInventoryChanged += RefreshAllRecipes;
    }

    private void OnDestroy()
    {
        InventoryManager.OnInventoryChanged -= RefreshAllRecipes;
    }

    public void RefreshAllRecipes()
    {
        print("refreshing all recipes");

        foreach (var ui in recipeUIs)
        {
            ui.RefreshUI();
        }
    }

    public CraftingRecipeUI GetUIForRecipe(CraftingRecipe recipe)
    {
        foreach (var ui in recipeUIs)
        {
            if (ui.GetRecipe() == recipe)
            {
                return ui;
            }
        }
        return null;
    }

    public void RebuildRecipeList()
    {
        // Clear existing UI
        foreach (Transform child in recipeListParent)
        {
            Destroy(child.gameObject);
        }

        recipeUIs.Clear();

        // Recreate UI entries
        foreach (var recipe in availableRecipes)
        {
            var go = Instantiate(recipeUIPrefab, recipeListParent);
            var recipeUI = go.GetComponent<CraftingRecipeUI>();
            recipeUI.Setup(recipe);
            recipeUIs.Add(recipeUI);
        }

        RefreshAllRecipes();
    }
}

