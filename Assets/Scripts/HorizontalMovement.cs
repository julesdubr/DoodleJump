using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    [SerializeField] private float minSpeed = 1.5f;
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float offset = .1f;

    private float _speed;
    private int _direction;

    void Start()
    {
        _direction = Random.Range(0, 1) * 2 - 1;
        _speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x < (0f + offset))
            _direction = 1;
        else if (pos.x >= (1f - offset))
            _direction = -1;

        transform.Translate(_direction * _speed * Time.deltaTime, 0f, 0f);
    }
}
