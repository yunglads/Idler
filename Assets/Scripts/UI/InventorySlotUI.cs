using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image icon;
    public TextMeshProUGUI countText;
    public Item item;
    public int count;

    public void Setup(Item newItem, int newCount)
    {
        item = newItem;
        count = newCount;
        icon.sprite = item.icon;
        countText.text = item.isStackable ? count.ToString() : "";
        icon.enabled = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
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
        item = null;
        count = 0;
        icon.enabled = false;
        countText.text = "";
    }
}

