using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    [SerializeField] private enum LandingImpact { None, Break, Destroy }

    public bool bounce = true;
    public bool killPlayer = false;
    [SerializeField] private LandingImpact onLanding = LandingImpact.None;
    [SerializeField] private AudioClip _breakSound;

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
        switch (onLanding)
        {
            case ColliderController.LandingImpact.Break:
                Physics2D.IgnoreCollision(player_collider, _collider);

                AudioSystem.Instance.PlaySound(_breakSound);

                _animator.SetTrigger("Destroy");

                _rigidbody.isKinematic = false;
                _rigidbody.gravityScale = 1f;

                killPlayer = false;
                break;
            case ColliderController.LandingImpact.Destroy:
                Destroy(gameObject);
                killPlayer = false;
                break;
            default:
                break;
        }
    }
}
