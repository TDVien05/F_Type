using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParagraphController : MonoBehaviour
{
    public float fallSpeed = 0.1f;
    private TMP_Text textMesh;
    private Vector3 startPosition;
    private ParagraphSpawn paragraphSpawn;

    void Start()
    {
        paragraphSpawn = FindObjectOfType<ParagraphSpawn>();
        textMesh = GetComponent<TMP_Text>();
        startPosition = transform.position;
    }

    void Update()
    {
        MovingTextDown();
        if (transform.position.y < 0)
        {
            paragraphSpawn.SetWords(textMesh);
        }

        if (paragraphSpawn.GetIsEnd() == true)
        {
            Debug.Log("End");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Va cham voi may bay.");
        }
    }

    void MovingTextDown()
    {
        if(transform.position.y > 7)
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }
}
