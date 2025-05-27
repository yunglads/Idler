using System.Security.Cryptography;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject menu;
    bool menuActive = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuActive)
            {
                menu.SetActive(false);
            }
            else
            {
                menu.SetActive(true);
            }

            menuActive = !menuActive;
        }
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
        menuActive = false;
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
