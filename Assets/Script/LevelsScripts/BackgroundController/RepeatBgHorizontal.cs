using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBgHorizontal : MonoBehaviour
{
    private BoxCollider2D coll; // BoxCollider2D để lấy kích thước nền.
    private Rigidbody2D rb;     // Rigidbody2D để di chuyển đối tượng.
    private float width;        // Chiều rộng của nền.
    public float speed = 1f;    // Tốc độ di chuyển từ trái sang phải.

    void Start()
    {
        // Lấy các component cần thiết.
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        // Lấy chiều rộng của collider để làm tham chiếu.
        width = coll.size.x;

        // Gán vận tốc cho Rigidbody2D (di chuyển theo trục X).
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
    }

    void Update()
    {
        // Nếu vị trí của nền vượt qua giới hạn chiều rộng dương, tái định vị.
        if (transform.position.x > width)
        {
            Reposition();
        }
    }

    private void Reposition()
    {
        // Di chuyển nền sang bên trái để lặp lại.
        Vector2 vector = new Vector2(-width * 2f , 0);
        transform.position = (Vector2)transform.position + vector;
    }
}
