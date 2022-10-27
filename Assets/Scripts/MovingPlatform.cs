using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private float _speed;
    private int _direction;


    void Start()
    {
        _direction = Random.Range(0, 1) * 2 - 1;
        _speed = Random.Range(1.5f, 3f);
    }

    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x < 0.1f)
            _direction = 1;
        else if (pos.x >= 0.9f)
            _direction = -1;

        transform.Translate(_direction * _speed * Time.deltaTime, 0f, 0f);
    }
}
