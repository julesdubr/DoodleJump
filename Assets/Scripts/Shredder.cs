using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;

        if (go.CompareTag("Player"))
        {
            go.GetComponent<PlayerController>().GameOver();
            return;
        }

        // Vector3 position = Camera.main.WorldToViewportPoint(go.transform.position);

        // if (position.y < 0f)
        //     Destroy(go);
        Destroy(go);
    }
}
