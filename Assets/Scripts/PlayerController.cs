using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    public Animator animator;
    public float speed = 5f;
    public float jumpForce = 11f;
    private bool facingRight = true;

    private Vector2 input;
    private Vector2 constantVelocity;
    private Vector2 smoothInputVelocity;

    public Transform firePoint;
    public GameObject projectilePrefab;

    public TextMeshProUGUI scoreText;
    private float distance = 0;

    [SerializeField] private float smoothInputSpeed = .1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue movementValue)
    {
        input = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        animator.SetTrigger("Fire");
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ennemy") || collision.gameObject.CompareTag("BlackHole"))
            GameOver();

        if (!collision.gameObject.CompareTag("Platform"))
            return;

        if (collision.relativeVelocity.y < 0f)
            return;

        Vector2 velocity = rb.velocity;
        velocity.y = jumpForce;
        rb.velocity = velocity;

        animator.SetTrigger("Jump");
    }

    void Update()
    {
        // Movement Right or Left
        constantVelocity = Vector2.Lerp(constantVelocity, input * speed, smoothInputSpeed);

        Vector2 velocity = rb.velocity;
        velocity.x = constantVelocity.x;
        rb.velocity = velocity;

        // Game over when player falls off the screen
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.y < -0.1f)
            GameOver();

        // Teleport player to the other side of the screen
        if (pos.x < 0f)
            pos = new Vector3(1f, pos.y, pos.z);
        else if (pos.x >= 1f)
            pos = new Vector3(0f, pos.y, pos.z);

        transform.position = Camera.main.ViewportToWorldPoint(pos);

        // Flip sprite depending on input
        if (input.x > 0 && !facingRight)
            Flip();
        else if (input.x < 0 && facingRight)
            Flip();

        // Update score
        if (transform.position.y > distance)
        {
            distance = transform.position.y;
            scoreText.text = (distance * 50).ToString("F0");
        }
    }

    void Flip()
    {
        // Vector3 currentScale = gameObject.transform.localScale;
        // currentScale.x *= -1;
        // gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}
