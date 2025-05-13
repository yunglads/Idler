using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] uiPanels;

    private void Start()
    {
        OpenRaidUI();
        Invoke("OpenSkillsUI", .01f);
    }

    public void CloseAllUI()
    {
        foreach (var go in uiPanels)
        {
            go.SetActive(false);
        }
    }

    public void OpenSkillsUI()
    {
        foreach (var go in uiPanels)
        {
            if (go.name == "SkillsPanel")
            {
                CloseAllUI();
                go.SetActive(true);
            }
        }
    }

    public void OpenPlayerUI()
    {
        foreach (var go in uiPanels)
        {
            if (go.name == "PlayerPanel")
            {
                CloseAllUI();
                go.SetActive(true);
            }
        }
    }

    public void OpenCraftingUI()
    {
        foreach (var go in uiPanels)
        {
            if (go.name == "CraftingPanel")
            {
                CloseAllUI();
                go.SetActive(true);
            }
        }
    }

    public void OpenRaidUI()
    {
        foreach (var go in uiPanels)
        {
            if (go.name == "RaidsPanel")
            {
                CloseAllUI();
                go.SetActive(true);
            }
        }
    }

    public void OpenFightUI()
    {
        foreach (var go in uiPanels)
        {
            if (go.name == "FightsPanel")
            {
                CloseAllUI();
                go.SetActive(true);
            }
        }
    }
}
