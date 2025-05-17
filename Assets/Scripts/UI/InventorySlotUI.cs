using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Image icon;
    public TextMeshProUGUI countText;
    public InventoryItem inventoryItem;

    private float lastClickTime = 0f;
    private const float doubleClickThreshold = 0.3f;
    public ItemPopup itemPopup;

    private void Awake()
    {
        itemPopup = FindAnyObjectByType<ItemPopup>();
    }

    public void Setup(InventoryItem newItem)
    {
        inventoryItem = newItem;

        if (inventoryItem?.item != null)
        {
            icon.sprite = inventoryItem.item.icon;
            icon.enabled = true;

            countText.text = inventoryItem.item.isStackable && inventoryItem.count > 1
                ? inventoryItem.count.ToString()
                : "";
        }
        else
        {
            icon.enabled = false;
            countText.text = "";
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (inventoryItem == null || inventoryItem.item == null) return;

        DragHandler.Instance.StartDrag(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        DragHandler.Instance.MoveDrag(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragHandler.Instance.EndDrag();
    }

    public void Clear()
    {
        inventoryItem = null;
        icon.enabled = false;
        countText.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (inventoryItem == null || inventoryItem.item == null) return;

        if (Time.time - lastClickTime < doubleClickThreshold)
        {
            if (inventoryItem?.item != null)
            {
                itemPopup.Show(inventoryItem.item);
            }
        }

        lastClickTime = Time.time;
    }
}


