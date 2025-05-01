using UnityEngine;

public class RaidUIManager : MonoBehaviour
{
    public Raid[] allRaids;
    public GameObject raidUIPrefab;
    public Transform raidUIParent;

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
}
