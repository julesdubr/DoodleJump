using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    [SerializeField] private enum LandingImpact { None, Break, Destroy, Extend }

    public bool bounce = true;
    public bool fly = false;
    public float flightDuration = 0f;
    public float bounceCoef = 1f;
    public bool killPlayer = false;
    [SerializeField] private LandingImpact onLanding = LandingImpact.None;
    [SerializeField] private AudioClip _hitSound;

    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    public void ProcessPlayerLanding(Collider2D player_collider)
    {
        if (_hitSound != null)
            AudioSystem.Instance.PlaySound(_hitSound);

        switch (onLanding)
        {
            case LandingImpact.Break:
                Physics2D.IgnoreCollision(player_collider, _collider);

                _animator.SetTrigger("Destroy");
                _rigidbody.gravityScale = 1f;

                killPlayer = false;
                break;
            case LandingImpact.Destroy:
                Destroy(gameObject);
                killPlayer = false;
                break;
            case LandingImpact.Extend:
                _animator.SetTrigger("Extend");
                break;

            default:
                break;
        }
    }
}
