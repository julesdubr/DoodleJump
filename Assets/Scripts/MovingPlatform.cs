using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 2f;
    private int direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Random.Range(0, 1) * 2 - 1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x < 0.1f)
            direction = 1;
        else if (pos.x >= 0.9f)
            direction = -1;

        Vector2 velocity = rb.velocity;
        velocity.x = direction * speed;
        rb.velocity = velocity;
    }
}
