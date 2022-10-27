using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerup : MonoBehaviour
{
    public List<GameObject> powerUps;
    private float[] probabilities = {1f};
    private float spawn_prct = 0.05f;
    private Transform anchor;

    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        anchor = this.transform;
        if (Random.Range(0f, 1f) > spawn_prct) return;
        float rand = Random.Range(0f, 1f);
        int indexPup = 0;
        while (rand > probabilities[indexPup]) indexPup++;
        pos = new Vector3(Random.Range(-0.2f, 0.2f), 0.2f, 0f);
        GameObject res = Instantiate(powerUps[indexPup], pos + anchor.position, Quaternion.identity);
        res.GetComponent<Powerup>().pos = pos;
        res.GetComponent<Powerup>().anchor = anchor;
    }
}
