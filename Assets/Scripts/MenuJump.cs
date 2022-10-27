using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuJump : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    public float jumpForce = 11f;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _rigidbody.velocity = Vector2.up * 15f;
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        // Make the player bounce
        Vector2 velocity = _rigidbody.velocity;
        velocity.y = jumpForce;
        _rigidbody.velocity = velocity;

        _animator.SetTrigger("Jump");
    }
}
