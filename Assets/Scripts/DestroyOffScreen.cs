using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffScreen : MonoBehaviour
{
    private Renderer _renderer;
    
    void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);

        if (!_renderer.isVisible && position.y < 0f)
            Destroy(gameObject);
    }
}
