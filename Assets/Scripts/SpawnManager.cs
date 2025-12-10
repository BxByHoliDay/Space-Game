using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public SpawnPoint[] spawnPoints;

    [Header("Spawn Timing")]
    public Vector2 spawnIntervalRange = new Vector2(3f, 6f); 
    public float difficultyStep = 0.5f;     
    public float difficultyInterval = 10f;   
    public float minSpawnTime = 1f;       

    void Start()
    {
        foreach (var sp in spawnPoints)
        {
            StartCoroutine(SpawnRoutine(sp));
        }

        StartCoroutine(DifficultyScaler());
    }

    IEnumerator SpawnRoutine(SpawnPoint sp)
    {
        while (true)
        {
            float delay = Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
            yield return new WaitForSeconds(delay);

            if (sp.spawnData == null || sp.spawnData.dropItems.Length == 0)
                continue;

            
            float totalWeight = 0f;
            foreach (var d in sp.spawnData.dropItems)
            {
                totalWeight += Mathf.Max(0f, d.dropChance);
            }
            if (totalWeight <= 0f) continue;

            
            float pick = Random.Range(0f, totalWeight);

            
            float cumulative = 0f;
            foreach (var item in sp.spawnData.dropItems)
            {
                cumulative += Mathf.Max(0f, item.dropChance);
                if (pick <= cumulative)
                {
                    if (item.prefab != null)
                    {
                        Instantiate(item.prefab, sp.transform.position, Quaternion.identity);
                        
                    }
                    else
                    {
                        Debug.LogWarning($"Spawn data {sp.spawnData.name} มี ItemDrop ที่ prefab หายไป");
                    }
                    break;
                }
            }
        }
    }

    IEnumerator DifficultyScaler()
    {
        while (true)
        {
            yield return new WaitForSeconds(difficultyInterval);

            spawnIntervalRange.x = Mathf.Max(minSpawnTime, spawnIntervalRange.x - difficultyStep);
            spawnIntervalRange.y = Mathf.Max(minSpawnTime, spawnIntervalRange.y - difficultyStep);

            Debug.Log("Spawn speed increased! Interval now: " + spawnIntervalRange);
        }
    }
}
