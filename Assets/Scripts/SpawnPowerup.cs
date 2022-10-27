using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerup : MonoBehaviour
{
    [SerializeField] private float spawnProb = 0.05f;
    [SerializeField] private List<GameObject> powerUps;
    [SerializeField] private float[] probabilities = { 1f };


    void Start()
    {
        if (Random.Range(0f, 1f) > spawnProb)
            return;

        Vector3 offset = new Vector3(Random.Range(-.3f, .4f), .1f, 0f);

        float rand = Random.Range(0f, 1f);
        int indexPup = -1;

        while (rand > probabilities[++indexPup]);

        GameObject powerup = Instantiate(
            powerUps[indexPup], transform.position + offset, Quaternion.identity
        );

        powerup.transform.parent = transform;
    }
}
