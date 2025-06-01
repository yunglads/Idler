using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideoutController : MonoBehaviour
{
    public string roomName;
    public Transform cameraTarget;
    public GameObject buildRoomCraftPanel;
    public GameObject roomToActivate;
    public CraftingUIManager craftingManager;
    public List<CraftingRecipe> recipesToUnlock;

    public CraftingRecipe buildRecipe;

    private bool isBuilt = false;

    private void Update()
    {
        var ui = craftingManager.GetUIForRecipe(buildRecipe);
        if (ui != null && ui.gameObject.activeInHierarchy)
        {
            print("button found and active");

            ui.craftButton.onClick.RemoveAllListeners();
            ui.craftButton.onClick.AddListener(() => Build());

            //this.enabled = false; // Disable script after setting up
        }
    }

    public void OnBuildClicked()
    {
        if (!CameraController.Instance.isMoving)
        {  
            CameraController.Instance.MoveToTarget(cameraTarget);
            StartCoroutine(WaitForCameraAndShowPanel());
        }      
    }

    public void Build()
    {
        if (CraftingManager.Instance.CanCraft(buildRecipe))
        {
            CraftingManager.Instance.Craft(buildRecipe);

            roomToActivate.SetActive(true);

            craftingManager.availableRecipes.Remove(buildRecipe);

            foreach (var recipe in recipesToUnlock)
                craftingManager.availableRecipes.Add(recipe);

            craftingManager.RebuildRecipeList();

            //this.enabled = false;

            print("build successful");
        }
        
    }

    private void LoadState()
    {
        isBuilt = PlayerPrefs.GetInt($"HideoutRoom_{roomName}_Built", 0) == 1;
    }

    private IEnumerator WaitForCameraAndShowPanel()
    {
        // Wait until camera is done moving
        while (CameraController.Instance.isMoving)
            yield return null;

        // Then show the crafting panel
        buildRoomCraftPanel.SetActive(true);
    }
}
