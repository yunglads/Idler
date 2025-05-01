using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] uiPanels;

    private void Start()
    {
        OpenSkillsUI();
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
}
