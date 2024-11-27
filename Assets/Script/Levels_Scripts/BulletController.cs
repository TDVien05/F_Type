using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    private Camera camera;

    void Start()
    {
        camera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if (rb != null)
        {
            rb.velocity = new Vector2(0, speed);
        }

        Vector3 viewPortPos = camera.WorldToViewportPoint(transform.position);

        if (viewPortPos.y < 0 || viewPortPos.y > 1.1)
        {
            Destroy(gameObject);
        }
    }
}
