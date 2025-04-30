using TMPro;
using UnityEngine;

public class ResourceUI : MonoBehaviour
{
    public Item resource;
    public TMP_Text resourceText;

    void Update()
    {
        if (resource != null && resourceText != null)
        {
            double amount = ResourceManager.Instance.GetAmount(resource);
            resourceText.text = resource.ToString() + ": " + amount.ToString();
        }
    }
}
