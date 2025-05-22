using UnityEngine;
using UnityEditor;

public class ItemBatchEditor : EditorWindow
{
    private Sprite fallbackIcon;
    private string folderPath = "Assets/Scripts/ScriptableObjects/Items";

    // Field controls
    private bool editName = false;
    private string newName;

    private bool editDesc = false;
    private string newDesc;

    private bool editIcon = false;

    private bool editType = false;
    private ItemType newItemType;

    private bool editStackable = false;
    private bool newStackable;

    private bool editMaxStack = false;
    private int newMaxStack = 99;

    [MenuItem("Tools/Batch Edit Items")]
    public static void ShowWindow()
    {
        GetWindow<ItemBatchEditor>("Batch Edit Items");
    }

    private void OnGUI()
    {
        GUILayout.Label("Batch Edit Items", EditorStyles.boldLabel);

        folderPath = EditorGUILayout.TextField("Items Folder Path", folderPath);

        // Name
        editName = EditorGUILayout.Toggle("Edit Name", editName);
        if (editName)
            newName = EditorGUILayout.TextField("New Name", newName);

        // Description
        editDesc = EditorGUILayout.Toggle("Edit Description", editDesc);
        if (editDesc)
            newDesc = EditorGUILayout.TextArea(newDesc, GUILayout.Height(40));

        // Icon
        editIcon = EditorGUILayout.Toggle("Edit Icon (only if missing)", editIcon);
        if (editIcon)
            fallbackIcon = (Sprite)EditorGUILayout.ObjectField("Fallback Icon", fallbackIcon, typeof(Sprite), false);

        // Type
        editType = EditorGUILayout.Toggle("Edit Item Type", editType);
        if (editType)
            newItemType = (ItemType)EditorGUILayout.EnumPopup("New Item Type", newItemType);

        // Stackable
        editStackable = EditorGUILayout.Toggle("Edit Stackable", editStackable);
        if (editStackable)
            newStackable = EditorGUILayout.Toggle("New Stackable Value", newStackable);

        // Max Stack
        editMaxStack = EditorGUILayout.Toggle("Edit Max Stack", editMaxStack);
        if (editMaxStack)
            newMaxStack = EditorGUILayout.IntField("New Max Stack", newMaxStack);

        EditorGUILayout.Space(10);

        if (GUILayout.Button("Apply Changes to All Items"))
        {
            ApplyBatchEdit();
        }
    }

    private void ApplyBatchEdit()
    {
        // Get selected assets
        Object[] selectedObjects = Selection.GetFiltered(typeof(Item), SelectionMode.Assets);
        Item[] selectedItems = System.Array.ConvertAll(selectedObjects, item => item as Item);

        // Fallback to folder search if no valid Item assets are selected
        if (selectedItems.Length == 0)
        {
            string[] guids = AssetDatabase.FindAssets("t:Item", new[] { folderPath });
            selectedItems = new Item[guids.Length];
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                selectedItems[i] = AssetDatabase.LoadAssetAtPath<Item>(path);
            }
        }

        int editedCount = 0;

        foreach (Item item in selectedItems)
        {
            if (item == null) continue;

            bool dirty = false;

            if (editName && !string.IsNullOrEmpty(newName))
            {
                item.itemName = newName;
                dirty = true;
            }

            if (editDesc && !string.IsNullOrEmpty(newDesc))
            {
                item.itemDesc = newDesc;
                dirty = true;
            }

            if (editIcon && item.icon == null && fallbackIcon != null)
            {
                item.icon = fallbackIcon;
                dirty = true;
            }

            if (editType)
            {
                item.itemType = newItemType;
                dirty = true;
            }

            if (editStackable)
            {
                item.isStackable = newStackable;
                dirty = true;
            }

            if (editMaxStack)
            {
                item.maxStack = newMaxStack;
                dirty = true;
            }

            if (dirty)
            {
                EditorUtility.SetDirty(item);
                editedCount++;
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"Batch edit applied to {editedCount} items.");
    }
}


