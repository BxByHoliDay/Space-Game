using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public SpawnPoint[] spawnPoints;

    [Header("Spawn Timing")]
    public Vector2 spawnIntervalRange = new Vector2(3f, 6f);
    public float difficultyStep = 0.5f;
    public float difficultyInterval = 10f;
    public float minSpawnTime = 1f;

    private void Start()
    {
        foreach (var sp in spawnPoints)
        {
            StartCoroutine(SpawnRoutine(sp));
        }

        StartCoroutine(DifficultyScaler());
    }

    private IEnumerator SpawnRoutine(SpawnPoint sp)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnIntervalRange.x, spawnIntervalRange.y));

            if (sp.spawnData == null || sp.spawnData.dropItems.Length == 0)
                continue;

            var item = GetRandomItem(sp);
            if (item == null || item.prefab == null)
            {
                Debug.LogWarning($"SpawnData '{sp.spawnData.name}' มี item ที่ไม่มี prefab");
                continue;
            }

            Instantiate(item.prefab, sp.transform.position, Quaternion.identity);
        }
    }

    private ItemDrop GetRandomItem(SpawnPoint sp)
    {
        float totalWeight = 0f;
        foreach (var d in sp.spawnData.dropItems)
            totalWeight += Mathf.Max(0, d.dropChance);

        if (totalWeight <= 0)
            return null;

        float pick = Random.Range(0, totalWeight);
        float cumulative = 0f;

        foreach (var item in sp.spawnData.dropItems)
        {
            cumulative += Mathf.Max(0, item.dropChance);
            if (pick <= cumulative)
                return item;
        }

        return null;
    }

    private IEnumerator DifficultyScaler()
    {
        while (true)
        {
            yield return new WaitForSeconds(difficultyInterval);

            spawnIntervalRange.x = Mathf.Max(minSpawnTime, spawnIntervalRange.x - difficultyStep);
            spawnIntervalRange.y = Mathf.Max(minSpawnTime, spawnIntervalRange.y - difficultyStep);

            Debug.Log($"Spawn speed increased → {spawnIntervalRange}");
        }
    }
}
