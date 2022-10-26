using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> prefabList = new List<GameObject>();

    public int numberOfPlatforms = 200;
    public float levelWidth = 2.5f;
    public float minY = .5f;
    public float maxY = 1.5f;

    private float[] probabilities = {0.75f, 0.8f, 0.95f, 1f};

    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPosition = new Vector3(0f, -3f, 0f);

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            float rand = UnityEngine.Random.Range(0f, 1f);
            int prefabIndex = 0;

            while (rand > probabilities[prefabIndex]) prefabIndex++;

            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            Instantiate(prefabList[prefabIndex], spawnPosition, Quaternion.identity);
        }
    }

}
