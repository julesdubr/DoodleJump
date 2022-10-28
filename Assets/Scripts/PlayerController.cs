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
    private Renderer _renderer;

    private Vector2 _input;
    private Vector2 _constantVelocity;
    [SerializeField] private float smoothInputSpeed = .1f;
    private bool _facingRight = true;

    public float speed = 5f;
    public float jumpForce = 11f;

    public Transform firePoint;
    public GameObject projectilePrefab;
    public Transform starsPoint;
    public GameObject starsPrefab;
    public Transform propellerPoint;
    public GameObject propellerPrefab;
    public Transform jetpackPoint;
    public GameObject jetpackPrefab;

    public TextMeshProUGUI scoreText;
    private float _distance = 0;
    private int _score = 0;

    [SerializeField] private AudioClip[] _fireSounds;
    [SerializeField] private AudioSource _gameOverSound;

    public enum PlayerState { Normal, Flying, Dead };
    public PlayerState state = PlayerState.Normal;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<Renderer>();
    }

    void OnMove(InputValue movementValue)
    {
        if (state == PlayerState.Dead) return;

        _input = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        if (state == PlayerState.Dead) return;

        AudioSystem.Instance.PlaySound(_fireSounds[Random.Range(0, _fireSounds.Length)]);
        _animator.SetTrigger("Fire");
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        ColliderController controller = other.gameObject.GetComponent<ColliderController>();

        if (controller == null)
            return;

        // Check if player is colliding while coming from above
        bool fromAbove = other.relativeVelocity.y >= 0f;

        if (fromAbove)
        {
            if (controller.bounce)
            {
                // Make the player bounce
                Vector2 velocity = _rigidbody.velocity;
                velocity.y = jumpForce * controller.bounceCoef;
                _rigidbody.velocity = velocity;

                _animator.SetTrigger("Jump");
            }

            // Handle the landing impact on the object
            controller.ProcessPlayerLanding(_collider);
        }

        // Kill the player
        if (controller.killPlayer)
        {
            GameObject stars = Instantiate(starsPrefab, starsPoint.position, starsPoint.rotation);
            stars.transform.parent = transform;
            GameOver();
        }
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

        // Handling falling off the screen
        if (state == PlayerState.Normal && position.y < -0.1f)
            GameOver();

        // Flip sprite depending on input
        if (_input.x > 0 && !_facingRight)
            Flip();
        else if (_input.x < 0 && _facingRight)
            Flip();

        // Update score
        if (transform.position.y > _distance)
        {
            _distance = transform.position.y;
            _score = (int)(_distance * 50);
            scoreText.text = _score.ToString();
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
        if (_score > PlayerPrefs.GetInt("HighScore", 0))
            PlayerPrefs.SetInt("HighScore", _score);

        state = PlayerState.Dead;
        _collider.enabled = false;
        StartCoroutine(PlayGameOverSound());
    }

    IEnumerator PlayGameOverSound()
    {
        _gameOverSound.Play();
        yield return new WaitWhile(() => _gameOverSound.isPlaying || _renderer.isVisible);
        SceneManager.LoadScene(0);
    }
}
