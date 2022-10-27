using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 20f;
    private Rigidbody2D _rigidbody;
    [SerializeField] private AudioClip _monsterDeathSound;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _rigidbody.velocity = transform.up * speed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ennemy"))
        {
            AudioSystem.Instance.PlaySound(_monsterDeathSound);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}