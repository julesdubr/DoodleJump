using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 20f;
    private Rigidbody2D _rb;

    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _rb.velocity = transform.up * speed;
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}