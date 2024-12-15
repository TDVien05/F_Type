using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParagraphController : MonoBehaviour
{
    public float fallSpeed = 0.1f;
    private TMP_Text textMesh;
    private Vector3 startPosition;

    void Start()
    {
        textMesh = GetComponent<TMP_Text>();
        startPosition = transform.position;
    }

    void Update()
    {
        MovingTextDown();
    }


    void MovingTextDown()
    {
        if(transform.position.y > 7)
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
