using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler //IDropHandler
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
        icon.enabled = false;
        item = null;
        count = 0;
        countText.text = "";
    }

    //public void OnDrop(PointerEventData eventData)
    //{
    //    EquipmentSlotUI dragged = DragHandler.Instance.equipDragSource;
    //    if (dragged != null && dragged.item is EquipmentItem equip)
    //    {
    //        InventoryManager.Instance.AddItem(equip);
    //        dragged.Clear();
    //        DragHandler.Instance.EndDrag();
    //        InventoryUIManager.Instance.Refresh();

    //        //if (equip.slot == slotType)
    //        //{
    //        //    EquipmentManager.Instance.Equip(equip);
    //        //    icon.sprite = equip.icon;
    //        //    icon.enabled = true;

    //        //    InventoryManager.Instance.AddItem(equip);
    //        //    dragged.Clear();
    //        //}
    //    }
    //}
}

