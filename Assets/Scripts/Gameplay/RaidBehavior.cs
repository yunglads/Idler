using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class RaidBehavior : MonoBehaviour
{
    public Raid raid;
    public bool isActive = false;
    public bool raidSuccessful = true;

    private Coroutine raidRoutine;

    public void ToggleActive()
    {
        isActive = !isActive;

        if (isActive && raidRoutine == null)
        {
            raidRoutine = StartCoroutine(RunRaid());
        }
    }

    private IEnumerator RunRaid()
    {
        float gatherInterval = Random.Range(raid.minInterval, raid.maxInterval);
        float elapsed = 0f;

        int encounterCount = Random.Range(raid.minEncounters, raid.maxEncounters + 1);
        List<float> fightTimestamps = new();

        float minGap = 3f;
        float availableTime = gatherInterval - 10f; // Leave some buffer at the end
        fightTimestamps.Clear();

        float lastTimestamp = 0f;

        for (int i = 0; i < encounterCount; i++)
        {
            float minStart = lastTimestamp + minGap;
            float maxStart = availableTime - (minGap * (encounterCount - i - 1));

            if (minStart > maxStart)
            {
                Debug.LogWarning("Not enough space to schedule all encounters with required spacing. Adjusting max encounters or gatherInterval may help.");
                break;
            }

            float nextTimestamp = Random.Range(minStart, maxStart);
            fightTimestamps.Add(nextTimestamp);
            lastTimestamp = nextTimestamp;
        }

        Debug.Log("Scheduled fight encounter timestamps:");
        for (int i = 0; i < fightTimestamps.Count; i++)
        {
            Debug.Log($"Encounter {i + 1}: {fightTimestamps[i]:F2} seconds");
        }

        int currentFightIndex = 0;

        while (elapsed < gatherInterval)
        {
            if (FightUIHandler.Instance != null)
                FightUIHandler.Instance.UpdateRaidTimer(elapsed);

            // Only run fight if it's time and no fight is already running
            if (currentFightIndex < fightTimestamps.Count && elapsed >= fightTimestamps[currentFightIndex])
            {
                Debug.Log($"Starting fight {currentFightIndex + 1} at {elapsed:F2}s (scheduled at {fightTimestamps[currentFightIndex]:F2}s)");

                yield return FightController.Instance.StartFight(this); // This yields until fight ends
                if (!raidSuccessful)
                {
                    HandleFailure();
                    yield break;
                }

                currentFightIndex++;
                continue; // Skip incrementing elapsed this frame
            }

            // Update UI timer (optional)
            FightUIHandler.Instance?.UpdateRaidTimer(elapsed);

            elapsed += Time.deltaTime;
            yield return null;
        }

        DistributeLoot();
    }

    private void HandleFailure()
    {
        EquipmentManager.Instance.RemoveAllEquipment();
        isActive = false;
        RaidUIManager.Instance.ShowFailureAndClose();
        raidRoutine = null;
    }

    public void DistributeLoot()
    {
        if (!raidSuccessful)
        {
            HandleFailure();
            return;
        }

        List<GatherOutput> grantedLoot = new();

        foreach (var lootItem in raid.outputItems)
        {
            if (Random.value <= lootItem.dropChance)
            {
                int randomAmount = Random.Range(lootItem.minAmount, lootItem.maxAmount + 1);
                lootItem.setAmount = randomAmount;
                InventoryManager.Instance.AddItem(lootItem.item, randomAmount);
                grantedLoot.Add(lootItem);
            }
        }

        InventoryUIManager.Instance.Refresh();
        isActive = false;
        raidRoutine = null;
        RaidUIManager.Instance.ShowLootAndClose(grantedLoot);
    }
}

