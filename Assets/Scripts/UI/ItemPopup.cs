using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPopup : MonoBehaviour
{
    public GameObject panel;
    public Image iconImage;
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;
    public Button closeButton;
    public GameObject useItemButton;
    public Item currentItem;

    private void Awake()
    {
        closeButton.onClick.AddListener(() => panel.SetActive(false));
        panel.SetActive(false);
    }

    public void Show(Item item)
    {
        currentItem = item;
        iconImage.sprite = item.icon;
        itemNameText.text = item.itemName;
        itemDescriptionText.text = item.itemDesc;
        panel.SetActive(true);

        if (item != null && item.itemType == ItemType.Consumable) 
        {
            useItemButton.SetActive(true);
        }
        else
        {
            useItemButton.SetActive(false);
        }
    }

    public void UseConsumable()
    {
        if (currentItem != null)
        {
            PlayerStats.Instance.health += currentItem.bonusHealth;
            PlayerStats.Instance.hunger += currentItem.bonusFood;
            PlayerStats.Instance.thirst += currentItem.bonusWater;
            InventoryManager.Instance.RemoveItem(currentItem);
            InventoryUIManager.Instance.Refresh();
            panel.SetActive(false);
            print("item used");
        }
    }
}

