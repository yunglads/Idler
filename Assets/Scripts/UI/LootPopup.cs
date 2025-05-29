using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LootPopup : MonoBehaviour
{
    public GameObject popupUI;
    public GameObject failPopupUI;
    public GameObject lootUIPrefab;
    public Transform lootPanel;
    //public TMP_Text raidResultText;

    private void Awake()
    {
        popupUI.SetActive(false);
        failPopupUI.SetActive(false);
    }

    public void ShowFailure()
    {
        //raidResultText.text = "Raid Failed";
        failPopupUI.SetActive(true);
    }

    public void ShowLoot(List<GatherOutput> lootList)
    {
        //raidResultText.text = "Raid Sucessful";
        popupUI.SetActive(true);

        // Clear old icons
        foreach (Transform child in lootPanel)
            Destroy(child.gameObject);

        foreach (GatherOutput item in lootList)
        {
            GameObject iconObj = Instantiate(lootUIPrefab, lootPanel);
            Image iconImage = iconObj.GetComponent<Image>();
            TMP_Text iconText = iconObj.GetComponentInChildren<TMP_Text>();

            iconImage.sprite = item.item.icon;
            iconText.text = item.setAmount.ToString();
        } 
    }

    public void Hide()
    {
        popupUI.SetActive(false);
        failPopupUI.SetActive(false);
    }
}

