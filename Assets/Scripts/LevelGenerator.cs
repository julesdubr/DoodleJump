using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> platformsList = new List<GameObject>();
    [SerializeField] private float[] platformsProbs = { 0.75f, 0.8f, 0.95f, 1f };

    [SerializeField] private float obstacleSpawnProb = 0.01f;
    [SerializeField] private List<GameObject> obstaclesList = new List<GameObject>();
    [SerializeField] private float[] obstaclesProbs = { 0.3f, 0.6f, 0.9f, 1f };

    [SerializeField] private int _numberOfPlatforms = 200;
    [SerializeField] private float _levelWidth = 2.5f;
    [SerializeField] private float _minY = .5f;
    [SerializeField] private float _maxY = 1.5f;

    private Vector3 spawnPosition = new Vector3(0f, -3f, 0f);
    private float rand;

    void Start()
    {
        for (int i = 0; i < _numberOfPlatforms; i++)
        {
            // Spawn obstacle
            if (Random.Range(0f, 1f) <=  obstacleSpawnProb)
            {
                GameObject obstacle = GetRandomPrefab(obstaclesList, obstaclesProbs);

                // Randomize position
                RandomizeSpawn(-.1f, .2f);
                Instantiate(obstacle, spawnPosition, Quaternion.identity);
            }

            // Spawn a random platform
            GameObject platform = GetRandomPrefab(platformsList, platformsProbs);

            // Randomize position
            RandomizeSpawn(_minY, _maxY);
            Instantiate(platform, spawnPosition, Quaternion.identity);
        }
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
        // spawnPosition.x = Random.Range(-_levelWidth, _levelWidth);
        // marche pas
        do
        {
            spawnPosition.x = Random.Range(-_levelWidth, _levelWidth);
        }
        while (Physics2D.OverlapBox(spawnPosition, new Vector2(1.2f, .3f), 0f) != null);
        spawnPosition.y += Random.Range(minY, maxY);
    }
}
