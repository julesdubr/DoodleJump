using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLand : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y > 0f)
            return;

        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();

        if (rb == null)
            return;

        Destroy(this.gameObject);
    }
}
