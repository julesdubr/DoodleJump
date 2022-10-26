using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLand : MonoBehaviour
{
    private Collider2D _collider;
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        if (collision.relativeVelocity.y > 0f)
            return;

        if (gameObject.CompareTag("Trap"))
        {
            _collider.enabled = false;
            _animator.SetTrigger("Destroy");
            _rigidbody.isKinematic = false;
            _rigidbody.gravityScale = 1f;
            return;
        }

        Destroy(gameObject);
    }
}
