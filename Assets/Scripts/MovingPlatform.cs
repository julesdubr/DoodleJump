using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private float speed;
    private int direction;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        direction = Random.Range(0, 1) * 2 - 1;
        speed = Random.Range(1.5f, 3f);
    }

    void FixedUpdate()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x < 0.1f)
            direction = 1;
        else if (pos.x >= 0.9f)
            direction = -1;

        Vector2 velocity = _rigidbody.velocity;
        velocity.x = direction * speed;
        _rigidbody.velocity = velocity;
    }
}
