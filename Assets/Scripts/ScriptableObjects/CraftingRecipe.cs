using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IdleGame/Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    [System.Serializable]
    public class Ingredient
    {
        public Item item;
        public int amount;
    }

    public string recipeName;
    public List<Ingredient> ingredients = new();
    public Item result;
    public int resultAmount = 1;
}

