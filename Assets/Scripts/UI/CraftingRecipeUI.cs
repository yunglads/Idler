using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingRecipeUI : MonoBehaviour
{
    public TMP_Text recipeNameText;
    public Button craftButton;
    public Image resultIcon;
    public TMP_Text resultAmountText;
    public Transform ingredientPanel;
    public TMP_Text ingredientName;
    public GameObject ingredientIconPrefab;

    private CraftingRecipe recipe;

    public void Setup(CraftingRecipe newRecipe)
    {
        recipe = newRecipe;
        recipeNameText.text = recipe.recipeName;
        resultIcon.sprite = recipe.result.icon;

        if (recipe.resultAmount > 1)
        {
            resultAmountText.text = recipe.resultAmount.ToString();
        }
        
        // Clear old icons
        foreach (Transform child in ingredientPanel)
            Destroy(child.gameObject);

        // Add new ingredient icons
        foreach (var ingredient in recipe.ingredients)
        {
            GameObject iconObj = Instantiate(ingredientIconPrefab, ingredientPanel);
            Image iconImage = iconObj.GetComponent<Image>();
            TextMeshProUGUI[] texts = iconObj.GetComponentsInChildren<TextMeshProUGUI>();

            iconImage.sprite = ingredient.item.icon;

            if (texts.Length > 0 )
                texts[0].text = ingredient.amount.ToString();

            if (texts.Length > 1)
                texts[1].text = ingredient.item.itemName;
        }

        craftButton.onClick.RemoveAllListeners();
        craftButton.onClick.AddListener(() =>
        {
            CraftingManager.Instance.Craft(recipe);
        });
    }
}


