using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothTime = .25f;
    private Vector3 _velocity = Vector3.zero;

    [SerializeField] public Transform target;

    // Update is called once per frame
    void LateUpdate()
    {
        if (target.position.y <= transform.position.y)
            return;

        Vector3 targetPosition = new Vector3(transform.position.x, target.position.y, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
    }
}
