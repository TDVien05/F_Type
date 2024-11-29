using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    private Vector2 movement;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // Get input for horizontal and vertical movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement = new Vector2(horizontal, vertical);
    }

    void FixedUpdate()
    {
        // Apply movement to the Rigidbody2D
        rb.velocity = movement * speed;
    }
}
