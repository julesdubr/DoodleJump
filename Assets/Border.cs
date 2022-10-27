using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;

        if (go.CompareTag("Player"))
        {
            // Teleport player to the other side of the screen
            Vector3 position = Camera.main.WorldToViewportPoint(go.transform.position);

            if (position.x < 0f)
                position = new Vector3(1f, position.y, position.z);
            else if (position.x >= 1f)
                position = new Vector3(0f, position.y, position.z);
            else
                return;

            go.transform.position = Camera.main.ViewportToWorldPoint(position);
            return;
        }

        // MovingPlatform moving_controller = go.GetComponent<MovingPlatform>();

        // if (moving_controller != null)
        //     moving_controller.ChangeDirection();
    }
}
