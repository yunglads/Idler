using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class EquipmentSlotUI : MonoBehaviour, IDropHandler
{
    public EquipmentSlot slotType;
    public Image icon;
    public TMP_Text labelText;

    void Start()
    {
        labelText.text = slotType.ToString();
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventorySlotUI dragged = DragHandler.Instance.dragSource;
        if (dragged != null && dragged.item is EquipmentItem equip)
        {
            if (equip.slot == slotType)
            {
                EquipmentManager.Instance.Equip(equip);
                icon.sprite = equip.icon;
                icon.enabled = true;

                InventoryManager.Instance.RemoveItem(equip);
                dragged.Clear();
            }
        }
    }
}

