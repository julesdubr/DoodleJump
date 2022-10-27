using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusMovement : MonoBehaviour
{
    [SerializeField] private float speed = 7.5f;
    [SerializeField] private float intensity = .15f;

    private Vector3 startingPos;

    void Awake()
    {
        startingPos = transform.position;
    }

    void Update()
    {
        transform.position = startingPos + intensity * new Vector3(
            Mathf.PerlinNoise(Time.time * speed, 1),
            Mathf.PerlinNoise(Time.time * speed, 1), 0f);
    }
}
