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

    private void Awake()
    {
        closeButton.onClick.AddListener(() => panel.SetActive(false));
        panel.SetActive(false);
    }

    public void Show(Item item)
    {
        iconImage.sprite = item.icon;
        itemNameText.text = item.itemName;
        itemDescriptionText.text = item.itemDesc;
        panel.SetActive(true);
    }
}

