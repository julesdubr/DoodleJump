using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float _speed;
    private int _direction;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _direction = Random.Range(0, 1) * 2 - 1;
        _speed = Random.Range(1.5f, 3f);
    }

    void FixedUpdate()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x < 0.1f)
            _direction = 1;
        else if (pos.x >= 0.9f)
            _direction = -1;


        Vector2 velocity = _rigidbody.velocity;
        velocity.x = _direction * _speed;
        _rigidbody.velocity = velocity;
    }
}
