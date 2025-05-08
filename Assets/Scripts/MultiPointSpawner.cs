using UnityEngine;

public class MultiPointSpawner : MonoBehaviour
{
    public GameObject[] potionPrefabs;
    public Transform[] spawnPoints;
    public int potionsToSpawn = 10;

    void Start()
    {
        for (int i = 0; i < potionsToSpawn; i++)
        {
            SpawnPotionAtRandomPoint();
        }
    }

    void SpawnPotionAtRandomPoint()
    {
        int randomPointIndex = Random.Range(0, spawnPoints.Length);
        int randomPotionIndex = Random.Range(0, potionPrefabs.Length);

        Instantiate(potionPrefabs[randomPotionIndex], 
                    spawnPoints[randomPointIndex].position, 
                    Quaternion.identity);
    }
}
