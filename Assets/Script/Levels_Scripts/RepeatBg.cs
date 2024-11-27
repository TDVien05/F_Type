using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBg : MonoBehaviour
{
    private BoxCollider2D myCollider2D;
    private Rigidbody2D rb;
    private float height;
    public float speed = -3f;
    // Start is called before the first frame update
    void Start()
    {
        myCollider2D = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        height = myCollider2D.size.y;
        rb.velocity = new Vector2(0, speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -height)
        {
            Reposition();
        }
    }

    private void Reposition()
    {
        Vector2 vector = new Vector2(0, height*2f - 1);
        transform.position = (Vector2)transform.position + vector; 
    }
}
