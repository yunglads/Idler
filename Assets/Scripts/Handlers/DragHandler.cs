using UnityEngine;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour
{
    public static DragHandler Instance;

    public Image dragImage;
    public Canvas canvas;
    public InventorySlotUI dragSource;

    void Awake()
    {
        Instance = this;
        dragImage.enabled = false;
    }

    public void StartDrag(InventorySlotUI source)
    {
        dragSource = source;
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
        dragSource = null;
    }
}

