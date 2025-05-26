using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using UnityEngine.EventSystems;

public class CraftingRecipeUI : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text recipeNameText;
    public Button craftButton;
    public Image resultIcon;
    public TMP_Text resultAmountText;
    public Transform ingredientPanel;
    public TMP_Text ingredientName;
    public GameObject ingredientIconPrefab;

    private CraftingRecipe recipe;

    private float lastClickTime = 0f;
    private const float doubleClickThreshold = 0.3f;
    public ItemPopup itemPopup;

    private void Awake()
    {
        itemPopup = FindAnyObjectByType<ItemPopup>();
    }

    public void Setup(CraftingRecipe newRecipe)
    {
        recipe = newRecipe;
        recipeNameText.text = recipe.recipeName;
        resultIcon.sprite = recipe.result.icon;

        StringBuilder sb = new StringBuilder();

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
            int owned = InventoryManager.Instance.GetAmount(ingredient.item);
            int required = ingredient.amount;

            GameObject iconObj = Instantiate(ingredientIconPrefab, ingredientPanel);
            Image iconImage = iconObj.GetComponent<Image>();
            TextMeshProUGUI[] texts = iconObj.GetComponentsInChildren<TextMeshProUGUI>();

            iconImage.sprite = ingredient.item.icon;

            if (texts.Length > 0)
            {
                string color = owned >= required ? "#00FF00" : "#FFFFFF"; // green if enough, white if not
                texts[0].text = $"<color={color}>{ingredient.amount}</color>";
            }

            if (texts.Length > 1)
                texts[1].text = ingredient.item.itemName;
        }

        craftButton.onClick.RemoveAllListeners();
        craftButton.onClick.AddListener(() =>
        {
            CraftingManager.Instance.Craft(recipe);
            RefreshUI();
        });
    }

    public void RefreshUI()
    {
        if (recipe != null)
            Setup(recipe);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (recipe.result == null) return;

        if (Time.time - lastClickTime < doubleClickThreshold)
        {
            if (recipe.result != null)
            {
                itemPopup.Show(recipe.result);
            }
        }

        lastClickTime = Time.time;
    }
}


