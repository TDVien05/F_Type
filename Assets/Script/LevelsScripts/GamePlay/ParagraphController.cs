using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParagraphController : MonoBehaviour
{
    public float fallSpeed = 0.1f;
    private TMP_Text textMesh;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        textMesh = GetComponent<TMP_Text>();
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
            audioSource.Play();
        }
    }
}
