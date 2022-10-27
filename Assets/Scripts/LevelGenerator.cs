using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabList = new List<GameObject>();
    [SerializeField] private float[] probabilities = { 0.75f, 0.8f, 0.95f, 1f };

    [SerializeField] private int numberOfPlatforms = 200;
    [SerializeField] private float levelWidth = 2.5f;
    [SerializeField] private float minY = .5f;
    [SerializeField] private float maxY = 1.5f;


    void Start()
    {
        Vector3 spawnPosition = new Vector3(0f, -3f, 0f);

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            float rand = UnityEngine.Random.Range(0f, 1f);
            int prefabIndex = -1;

            while (rand > probabilities[++prefabIndex]) ;

            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            Instantiate(prefabList[prefabIndex], spawnPosition, Quaternion.identity);
        }
    }

}
