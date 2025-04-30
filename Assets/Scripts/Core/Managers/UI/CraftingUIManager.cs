using System.Collections.Generic;
using UnityEngine;

public class CraftingUIManager : MonoBehaviour
{
    public List<CraftingRecipe> availableRecipes;
    public GameObject recipeUIPrefab;
    public Transform recipeListParent;

    void Start()
    {
        foreach (var recipe in availableRecipes)
        {
            var go = Instantiate(recipeUIPrefab, recipeListParent);
            var recipeUI = go.GetComponent<CraftingRecipeUI>();
            recipeUI.Setup(recipe);
        }
    }
}

