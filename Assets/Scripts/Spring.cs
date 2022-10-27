using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite extended;

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var normal = collision.contacts[0].normal;
        if(normal.y < 0)
        spriteRenderer.sprite = extended;
    }
}
