using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private Animator _animator;

    private Vector2 _input;
    private Vector2 _constantVelocity;
    [SerializeField] private float smoothInputSpeed = .1f;
    private bool _facingRight = true;

    public Transform firePoint;
    public GameObject projectilePrefab;
    public float speed = 5f;
    public float jumpForce = 11f;

    public TextMeshProUGUI scoreText;
    private float _distance = 0;


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    void OnMove(InputValue movementValue)
    {
        _input = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        _animator.SetTrigger("Fire");
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        ColliderController controller = other.gameObject.GetComponent<ColliderController>();

        if (controller == null)
            return;

        bool fromAbove = other.relativeVelocity.y > 0f;

        if (fromAbove)
        {
            if (controller.bounce)
            {
                Vector2 velocity = _rigidbody.velocity;
                velocity.y = jumpForce;
                _rigidbody.velocity = velocity;

                _animator.SetTrigger("Jump");
            }

            controller.ProcessPlayerLanding(_collider);
        }

        if (controller.killPlayer)
            GameOver();
    }
 
    void FixedUpdate()
    {
        // Teleport player to the other side of the screen
        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);

        if (position.x < 0f)
            position = new Vector3(1f, position.y, position.z);
        else if (position.x >= 1f)
            position = new Vector3(0f, position.y, position.z);

        transform.position = Camera.main.ViewportToWorldPoint(position);

        // Flip sprite depending on input
        if (_input.x > 0 && !_facingRight)
            Flip();
        else if (_input.x < 0 && _facingRight)
            Flip();

        // Update score
        if (transform.position.y > _distance)
        {
            _distance = transform.position.y;
            scoreText.text = (_distance * 50).ToString("F0");
        }

        // Move
        _constantVelocity = Vector2.Lerp(_constantVelocity, _input * speed, smoothInputSpeed);

        Vector2 velocity = _rigidbody.velocity;
        velocity.x = _constantVelocity.x;
        _rigidbody.velocity = velocity;
    }

    void Flip()
    {
        _facingRight = !_facingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }

    public Animator GetAnimator()
    {
        return _animator;
    }
}
