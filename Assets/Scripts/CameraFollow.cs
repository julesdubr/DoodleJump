using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothTime = .25f;
    private Vector3 _velocity = Vector3.zero;

    [SerializeField] private PlayerController player;

    private Transform target;
    private Vector3 targetPosition;

    void Awake()
    {
        target = player.transform;
    }

    void LateUpdate()
    {
        if (target.position.y > transform.position.y)
            targetPosition = new Vector3(transform.position.x, target.position.y, transform.position.z);

        else if (player.isDead)
            targetPosition = new Vector3(transform.position.x, target.position.y - 2, transform.position.z);

        else return;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
    }
}
