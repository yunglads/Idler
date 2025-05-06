using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour
{
    public static DragHandler Instance;

    public Image dragImage;
    public Canvas canvas;
    public InventorySlotUI invDragSource;
    public EquipmentSlotUI equipDragSource;

    void Awake()
    {
        Instance = this;
        dragImage.enabled = false;
    }

    public void StartDrag(InventorySlotUI source)
    {
        invDragSource = source;
        dragImage.sprite = source.icon.sprite;
        dragImage.enabled = true;
    }

    public void StartDrag(EquipmentSlotUI source)
    {
        equipDragSource = source;
        dragImage.sprite = source.icon.sprite;
        dragImage.enabled = true;
    }

    public void MoveDrag(Vector2 screenPosition)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            screenPosition, canvas.worldCamera,
            out Vector2 localPoint);

        dragImage.rectTransform.localPosition = localPoint;
    }

    public void EndDrag()
    {
        dragImage.enabled = false;
        invDragSource = null;
        equipDragSource = null;
    }
}

