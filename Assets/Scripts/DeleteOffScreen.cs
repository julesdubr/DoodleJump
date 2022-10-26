using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOffScreen : MonoBehaviour
{
    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.y < -0.01f)
            Destroy(this.gameObject);
    }
}
