using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> platformsList;
    [SerializeField] private float[] platformsProbs = { 0.75f, 0.8f, 0.95f, 1f };

    [SerializeField] private float obstacleSpawnProb = 0.01f;
    [SerializeField] private List<GameObject> obstaclesList;
    [SerializeField] private float[] obstaclesProbs = { 0.3f, 0.6f, 0.9f, 1f };

    [SerializeField] private float levelWidth = 2.5f;
    [SerializeField] private float minY = .5f;
    [SerializeField] private float maxY = 1.5f;

    private Vector3 spawnPosition = new Vector3(0f, -3.5f, 0f);
    [SerializeField] private Transform spawnLimitPoint;
    private Collider2D hitCollider;

    void Awake()
    {
        while (CanSpawn()) SpawnObstaclesPlatforms();
    }

    void Update()
    {
        if (CanSpawn()) SpawnObstaclesPlatforms();
    }

    private void SpawnObstaclesPlatforms()
    {
        // Spawn obstacle
        if (spawnPosition.y > 10f && Random.Range(0f, 1f) <= obstacleSpawnProb)
        {
            GameObject obstacle = GetRandomPrefab(obstaclesList, obstaclesProbs);

            // Randomize position
            RandomizeSpawn(-.1f, .2f);
            Instantiate(obstacle, spawnPosition, Quaternion.identity);
        }

        // Spawn a random platform
        GameObject platform = GetRandomPrefab(platformsList, platformsProbs);

        // Randomize position
        RandomizeSpawn(minY, maxY);
        Instantiate(platform, spawnPosition, Quaternion.identity);
    }

    private GameObject GetRandomPrefab(List<GameObject> prefabList, float[] probs)
    {
        float rand = Random.Range(0f, 1f);
        int index = -1;

        while (rand > platformsProbs[++index]) ;

        return prefabList[index];
    }

    void RandomizeSpawn(float minY, float maxY)
    {
        do // pas l'impression que ca marche a chaque fois
        {
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            hitCollider = Physics2D.OverlapBox(spawnPosition, new Vector2(1.2f, .3f), 0f);
        }
        while (hitCollider != null);
        spawnPosition.y += Random.Range(minY, maxY);
    }

    bool CanSpawn()
    {
        return spawnPosition.y <= spawnLimitPoint.position.y;
    }
}
