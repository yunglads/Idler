using System;
using Unity.VisualScripting;
using UnityEngine;

public class OfflineProgress : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.HasKey("LastPlayed"))
        {
            string lastPlayed = PlayerPrefs.GetString("LastPlayed");
            DateTime lastTime = DateTime.Parse(lastPlayed);
            TimeSpan offlineTime = DateTime.Now - lastTime;

            double offlineWood = offlineTime.TotalSeconds / 2; // e.g., 1 wood every 2 seconds
            //InventoryManager.Instance.AddItem(item, offlineWood.ConvertTo<int>());
        }
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetString("LastPlayed", DateTime.Now.ToString());
    }
}
