using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class EquipmentSlotUI : MonoBehaviour, IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public EquipmentSlot slotType;
    public Image icon;
    public Item item;
    public TMP_Text labelText;

    private float lastClickTime = 0f;
    private const float doubleClickThreshold = 0.3f;
    public ItemPopup itemPopup;

    void Start()
    {
        labelText.text = slotType.ToString();
    }

    public void Setup(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
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
        icon.enabled = false;
        item = null;
        //count = 0; 
        //countText.text = "";
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventorySlotUI dragged = DragHandler.Instance.invDragSource;
        if (dragged != null && dragged.inventoryItem.item is EquipmentItem equip)
        {
            if (equip.slot == slotType)
            {
                EquipmentManager.Instance.Equip(equip);
                item = equip;
                icon.sprite = equip.icon;
                icon.enabled = true;

                InventoryManager.Instance.RemoveItem(equip);
                dragged.Clear();

                DragHandler.Instance.EndDrag();
                InventoryUIManager.Instance.Refresh();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.time - lastClickTime < doubleClickThreshold)
        {
            if (item != null)
            {
                itemPopup.Show(item);
            }
        }

        lastClickTime = Time.time;
    }
}

