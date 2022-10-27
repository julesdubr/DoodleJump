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

    [SerializeField] private int numberOfPlatforms = 200;
    [SerializeField] private float levelWidth = 2.5f;
    [SerializeField] private float minY = .5f;
    [SerializeField] private float maxY = 1.5f;

    private float rand;

    void Start()
    {
        Vector3 spawnPosition = new Vector3(0f, -3f, 0f);

        for (int i = 0; i < numberOfPlatforms; i++)
        {

            // Spawn obstacle
            if (Random.Range(0f, 1f) <=  obstacleSpawnProb)
            {
                rand = Random.Range(0f, 1f);
                int obstacleIndex = -1;
                while (rand > obstaclesProbs[++obstacleIndex]) ;

                spawnPosition.x = Random.Range(-levelWidth, levelWidth);
                spawnPosition.y += Random.Range(-.1f, .2f);

                Instantiate(obstaclesList[obstacleIndex], spawnPosition, Quaternion.identity);
            }

            // Find a free position
            spawnPosition.y += Random.Range(minY, maxY);
            do
            {
                spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            }
            while (Physics2D.OverlapBox(spawnPosition, new Vector2(1.2f, .3f), 0f) != null);

            // Spawn a random platform
            rand = Random.Range(0f, 1f);
            int platformIndex = -1;
            while (rand > platformsProbs[++platformIndex]) ;

            Instantiate(platformsList[platformIndex], spawnPosition, Quaternion.identity);
        }
    }

}
