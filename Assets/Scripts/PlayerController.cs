using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private Animator _animator;
    private Renderer _renderer;

    private Vector2 _input;
    private float flightDuration = 0f;
    private float flyingCoef = 0f;
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

    private GameObject powerUp = null;

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
        if (state != PlayerState.Normal) return;

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
            // Handle the landing impact on the object
            controller.ProcessPlayerLanding(_collider);

            if (controller.Bounce())
            {
                // Make the player bounce
                Vector2 velocity = _rigidbody.velocity;
                velocity.y = jumpForce * controller.coef;
                _rigidbody.velocity = velocity;

                _animator.SetTrigger("Jump");
            }

            if (controller.Fly())
            {
                state = PlayerState.Flying;
                flightDuration = controller.flightDuration;
                flyingCoef = controller.coef;

                powerUp = other.gameObject;

                powerUp.GetComponent<Animator>().SetTrigger("On");

                switch (powerUp.tag)
                {
                    case "Propeller":
                        powerUp.transform.parent = propellerPoint;
                        powerUp.GetComponent<Renderer>().sortingOrder = 1;
                        break;
                    case "Jetpack":
                        powerUp.transform.parent = jetpackPoint;
                        break;
                    default:
                        break;
                }

                powerUp.transform.localPosition = Vector3.zero;
                return;
            }
        }

        // Kill the player
        if (controller.killPlayer)
        {
            if (state == PlayerState.Flying)
            {
                Destroy(other.gameObject);
                return;
            }
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

        // Fly
        if (flightDuration > 0)
        {
            flightDuration -= Time.deltaTime;
            Vector2 flyVelocity = _rigidbody.velocity;

            flyVelocity.y = jumpForce * flyingCoef * Mathf.Min(Mathf.Exp(1 / flightDuration), 1);

            if (flightDuration <= 0) flyVelocity.y = jumpForce * 0.75f;

            _rigidbody.velocity = flyVelocity;
        }
        if (flightDuration < 0)
        {
            state = PlayerState.Normal;
            flightDuration = 0f;
            Destroy(powerUp);
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
