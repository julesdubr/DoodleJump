using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public Transform anchor;
    public Vector3 pos;

    // Update is called once per frame
    void Update()
    {
        if (anchor == null) {Destroy(gameObject); return;}
        this.transform.SetPositionAndRotation(pos + anchor.position, Quaternion.identity);
    }
}
