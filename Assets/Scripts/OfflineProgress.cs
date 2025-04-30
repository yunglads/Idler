using System;
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
            //ResourceManager.Instance.AddAmount("Wood", offlineWood);
        }
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetString("LastPlayed", DateTime.Now.ToString());
    }
}
