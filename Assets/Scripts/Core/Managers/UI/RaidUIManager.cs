using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaidUIManager : MonoBehaviour
{
    public Raid[] allRaids;
    public GameObject raidUIPrefab;
    public Transform raidUIParent;

    public static RaidUIManager Instance;

    public GameObject closeUIButton;
    public LootPopup lootPopup;

    void Start()
    {
        foreach (Raid raids in allRaids)
        {
            GameObject raidGO = new GameObject(raids.raidName);
            RaidBehavior behavior = raidGO.AddComponent<RaidBehavior>();
            behavior.raid = raids;

            GameObject ui = Instantiate(raidUIPrefab, raidUIParent);
            RaidUIHandler handler = ui.GetComponent<RaidUIHandler>();
            handler.Setup(raids, behavior);
        }
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowLootAndClose(List<GatherOutput> loot)
    {
        lootPopup?.ShowLoot(loot);
        closeUIButton?.gameObject.SetActive(true);
    }

    public void ShowFailureAndClose()
    {
        lootPopup?.ShowFailure();
        //FightUIHandler.Instance.fightPanel.SetActive(false);
        closeUIButton?.gameObject.SetActive(true);
    }
}
