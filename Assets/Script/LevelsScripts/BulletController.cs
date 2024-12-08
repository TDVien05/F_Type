using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    private Camera cam;

    public GameObject FirePoint;

    void Start()
    {
        cam = Camera.main;
        FirePoint = GameObject.Find("FirePoint");
        if (rb != null && FirePoint != null)
        {
            rb.velocity = (Vector2)FirePoint.transform.up * speed;
        }
    }
    // Update is called once per frame
    void Update()
    {

        Vector3 viewPortPos = cam.WorldToViewportPoint(transform.position);

        if (viewPortPos.y < 0 || viewPortPos.y > 1.1 || viewPortPos.x < 0 || viewPortPos.x > 1)
        {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag($"text"))
        {
           Debug.Log("bullet collide with: " + other.name);
           Destroy(gameObject);
        }
    }

}
