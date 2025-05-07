using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public float spawnRate = 2f;
    public float spawnDistance = 10f;
    public float minHeight = 1f;
    public float maxHeight = 3f;

    private float nextSpawnTime;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnObstacle();
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
    }

    void SpawnObstacle()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnDistance, spawnDistance),
            Random.Range(minHeight, maxHeight),
            Random.Range(-spawnDistance, spawnDistance)
        );

        GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}