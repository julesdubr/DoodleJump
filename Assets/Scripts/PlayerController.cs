using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed = 10f;
    public float jumpForce = 10f;
    private bool facingRight = true;

    private Vector2 input;
    private Vector2 constantVelocity;
    private Vector2 smoothInputVelocity;

    [SerializeField] private float smoothInputSpeed = .2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue movementValue)
    {
        input = movementValue.Get<Vector2>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Platform"))
            return;

        if (collision.relativeVelocity.y < 0f)
            return;

        Vector2 velocity = rb.velocity;
        velocity.y = jumpForce;
        rb.velocity = velocity;
    }

    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        // Game over when player falls off the screen
        if (pos.y < -0.1f)
            UnityEditor.EditorApplication.isPlaying = false;

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
    }

    void FixedUpdate()
    {
        constantVelocity = Vector2.Lerp(constantVelocity, input * speed, smoothInputSpeed);

        Vector2 velocity = rb.velocity;
        velocity.x = constantVelocity.x;
        rb.velocity = velocity;
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
