using UnityEngine;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    private Dictionary<Item, double> resources = new();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddResource(Item resource, double amount)
    {
        if (!resources.ContainsKey(resource))
            resources[resource] = 0;
        resources[resource] += amount;
    }

    public void RemoveResource(Item resource, double amount)
    {
        if (!resources.ContainsKey(resource))
            resources[resource] = 0;
        resources[resource] -= amount;
    }

    public double GetAmount(Item resource)
    {
        return resources.TryGetValue(resource, out double val) ? val : 0;
    }
}
