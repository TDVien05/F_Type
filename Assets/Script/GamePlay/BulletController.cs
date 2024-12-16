using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    private Camera _cam;

    private GameObject _firePoint;

    void Start()
    {
        _cam = Camera.main;
        _firePoint = GameObject.Find("FirePoint");
        if (rb != null && _firePoint != null)
        {
            rb.velocity = (Vector2)_firePoint.transform.up * speed;
        }
    }
    // Update is called once per frame
    void Update()
    {

        Vector3 viewPortPos = _cam.WorldToViewportPoint(transform.position);

        if (viewPortPos.y < 0 || viewPortPos.y > 1.1 || viewPortPos.x < 0 || viewPortPos.x > 1)
        {
            Destroy(gameObject);
        }
    }
}
