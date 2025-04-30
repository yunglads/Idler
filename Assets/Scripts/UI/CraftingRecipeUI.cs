using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingRecipeUI : MonoBehaviour
{
    public TextMeshProUGUI recipeNameText;
    public Button craftButton;
    public Image resultIcon;
    public Transform ingredientPanel;
    public GameObject ingredientIconPrefab;

    private CraftingRecipe recipe;

    //private void Update()
    //{
    //    if (CraftingManager.Instance.CanCraft(recipe) == false)
    //    {
    //        craftButton.enabled = false;
    //    }
    //    else
    //    {
    //        craftButton.enabled = true;
    //    }
    //}

    public void Setup(CraftingRecipe newRecipe)
    {
        recipe = newRecipe;
        recipeNameText.text = recipe.recipeName;
        resultIcon.sprite = recipe.result.icon;

        // Clear old icons
        foreach (Transform child in ingredientPanel)
            Destroy(child.gameObject);

        // Add new ingredient icons
        foreach (var ingredient in recipe.ingredients)
        {
            GameObject iconObj = Instantiate(ingredientIconPrefab, ingredientPanel);
            Image iconImage = iconObj.GetComponent<Image>();
            TextMeshProUGUI countText = iconObj.GetComponentInChildren<TextMeshProUGUI>();

            iconImage.sprite = ingredient.item.icon;
            countText.text = ingredient.amount.ToString();
        }

        craftButton.onClick.RemoveAllListeners();
        craftButton.onClick.AddListener(() =>
        {
            CraftingManager.Instance.Craft(recipe);
        });
    }
}


