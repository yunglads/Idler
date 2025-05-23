#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CraftingRecipeImporter : EditorWindow
{
    private TextAsset csvFile;

    [MenuItem("Tools/Import Crafting Recipes from CSV")]
    public static void ShowWindow()
    {
        GetWindow<CraftingRecipeImporter>("Crafting Recipe Importer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Import Crafting Recipes", EditorStyles.boldLabel);
        csvFile = (TextAsset)EditorGUILayout.ObjectField("CSV File", csvFile, typeof(TextAsset), false);

        if (GUILayout.Button("Import Recipes"))
        {
            if (csvFile != null)
                ImportFromCSV(csvFile);
            else
                Debug.LogError("CSV file not assigned!");
        }
    }

    private void ImportFromCSV(TextAsset file)
    {
        string[] lines = file.text.Split('\n');

        string recipeFolder = "Assets/Scripts/ScriptableObjects/Recipes";
        if (!AssetDatabase.IsValidFolder(recipeFolder))
            AssetDatabase.CreateFolder("Assets/Scripts/ScriptableObjects", "Recipes");

        for (int i = 1; i < lines.Length; i++) // skip header
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] parts = lines[i].Split(',');

            if (parts.Length < 4)
            {
                Debug.LogWarning($"Invalid row at line {i + 1}: {lines[i]}");
                continue;
            }

            string recipeName = parts[0].Trim();
            string ingredientsStr = parts[1].Trim();
            string resultName = parts[2].Trim();
            int.TryParse(parts[3], out int resultAmount);

            List<CraftingRecipe.Ingredient> ingredients = ParseIngredients(ingredientsStr);

            Item resultItem = FindItemByName(resultName);
            if (resultItem == null)
            {
                Debug.LogWarning($"Result item not found: {resultName}");
                continue;
            }

            CraftingRecipe recipe = ScriptableObject.CreateInstance<CraftingRecipe>();
            recipe.recipeName = recipeName;
            recipe.ingredients = ingredients;
            recipe.result = resultItem;
            recipe.resultAmount = resultAmount;

            string assetPath = $"{recipeFolder}/{recipeName}.asset";
            AssetDatabase.CreateAsset(recipe, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Crafting recipes imported successfully.");
    }

    private List<CraftingRecipe.Ingredient> ParseIngredients(string input)
    {
        List<CraftingRecipe.Ingredient> list = new();

        string[] ingredientPairs = input.Split(';');
        foreach (string pair in ingredientPairs)
        {
            string[] data = pair.Split(':');
            if (data.Length != 2) continue;

            string itemName = data[0].Trim();
            int.TryParse(data[1], out int amount);

            Item item = FindItemByName(itemName);
            if (item == null)
            {
                Debug.LogWarning($"Ingredient item not found: {itemName}");
                continue;
            }

            CraftingRecipe.Ingredient ingredient = new()
            {
                item = item,
                amount = amount
            };

            list.Add(ingredient);
        }

        return list;
    }

    private Item FindItemByName(string itemName)
    {
        string[] guids = AssetDatabase.FindAssets($"{itemName} t:Item");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Item item = AssetDatabase.LoadAssetAtPath<Item>(path);
            if (item != null && item.itemName == itemName)
            {
                return item;
            }
        }

        return null;
    }
}
#endif


