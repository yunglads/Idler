using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class EquipmentSlotUI : MonoBehaviour, IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public EquipmentSlot slotType;
    public Image icon;
    public Item item;
    public TMP_Text labelText;

    void Start()
    {
        labelText.text = slotType.ToString();
    }

    public void Setup(Item newItem)
    {
        item = newItem;
        //count = newCount;
        icon.sprite = item.icon;
        //countText.text = item.isStackable ? count.ToString() : "";
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
        if (dragged != null && dragged.item is EquipmentItem equip)
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
}

