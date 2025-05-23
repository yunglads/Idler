using UnityEngine;
using UnityEditor;
using System.IO;

public class ItemImporter : EditorWindow
{
    [MenuItem("Tools/Import Items from CSV")]
    public static void ImportItemsFromCSV()
    {
        string csvPath = "Assets/Data/Items.csv";
        string savePath = "Assets/Scripts/ScriptableObjects/Items";

        if (!File.Exists(csvPath))
        {
            Debug.LogError("CSV file not found at: " + csvPath);
            return;
        }

        if (!AssetDatabase.IsValidFolder(savePath))
        {
            AssetDatabase.CreateFolder("Assets", "Items");
        }

        string[] lines = File.ReadAllLines(csvPath);

        for (int i = 1; i < lines.Length; i++) // skip header
        {
            string[] values = lines[i].Split(',');

            if (values.Length < 6)
            {
                Debug.LogWarning($"Invalid line at {i + 1}: {lines[i]}");
                continue;
            }

            string itemName = values[0];
            string itemDesc = values[1];
            string iconPath = $"Assets/Art/{values[2]}.png";
            string itemTypeStr = values[3];
            bool isStackable = bool.Parse(values[4]);
            int maxStack = int.Parse(values[5]);

            Sprite icon = AssetDatabase.LoadAssetAtPath<Sprite>(iconPath);
            if (icon == null)
            {
                Debug.LogWarning($"Icon not found at path: {iconPath}");
            }

            if (!System.Enum.TryParse(itemTypeStr, out ItemType itemType))
            {
                Debug.LogWarning($"Invalid ItemType: {itemTypeStr} at line {i + 1}");
                continue;
            }

            Item item = ScriptableObject.CreateInstance<Item>();
            item.itemName = itemName;
            item.itemDesc = itemDesc;
            item.icon = icon;
            item.itemType = itemType;
            item.isStackable = isStackable;
            item.maxStack = maxStack;

            string assetName = $"{itemName.Replace(" ", " ")}.asset";
            string assetPath = Path.Combine(savePath, assetName);

            AssetDatabase.CreateAsset(item, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Item import complete.");
    }
}

