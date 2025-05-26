using UnityEngine;

public class UseItem : MonoBehaviour
{
    ItemPopup popup;

    public void UseConsumable()
    {
        if (popup != null && popup.currentItem != null)
        {
            popup.currentItem.bonusHealth += PlayerStats.Instance.health;
        }
    }
}
