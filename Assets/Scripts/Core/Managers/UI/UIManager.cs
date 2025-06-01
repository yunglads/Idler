using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject[] uiPanels;
    public GameObject worldCanvas;
    public Transform hideoutCamTarget;

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
            if (go.name == "Skills")
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
            if (go.name == "CraftingScrollView")
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
            if (go.name == "Raids")
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
            if (go.name == "FightUI")
            {
                CloseAllUI();
                go.SetActive(true);
            }
        }
    }
    
    public void OpenHideoutUI()
    {
        if (!CameraController.Instance.isMoving)
        {
            CameraController.Instance.MoveToTarget(hideoutCamTarget);

            foreach (var go in uiPanels)
            {
                CloseAllUI();
            }

            worldCanvas.SetActive(true);
        }
    }

    public void OpenRestAreaUI()
    {
        if (!CameraController.Instance.isMoving)
        {
            foreach (var go in uiPanels)
            {
                if (go.name == "RestAreaCraftingScrollView")
                {
                    CloseAllUI();
                    go.SetActive(true);
                }
            }
        }
    }

    public void OpenKitchenUI()
    {
        if (!CameraController.Instance.isMoving)
        {
            foreach (var go in uiPanels)
            {
                if (go.name == "KitchenCraftingScrollView")
                {
                    CloseAllUI();
                    go.SetActive(true);
                }
            }
        }
    }

    public void OpenWeaponsUI()
    {
        if (!CameraController.Instance.isMoving)
        {
            foreach (var go in uiPanels)
            {
                if (go.name == "WeaponsCraftingScrollView")
                {
                    CloseAllUI();
                    go.SetActive(true);
                }
            }
        }
    }
}
