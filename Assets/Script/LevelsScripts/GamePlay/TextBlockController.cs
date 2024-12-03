using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlockController : MonoBehaviour
{
    public float initialSpeed = 7f; 
    public float maxSpeed = 20f; 
    public float speedIncreaseRate = 0.1f; 
    private float currentSpeed; 
    public Vector2 xRange = new Vector2(-9f, 9f);
    public Text textUI;
    private RectTransform textTransform;
    private float timeElapsed = 0f; 
    
    void Start()
    {
        textTransform = textUI.GetComponent<RectTransform>();
        currentSpeed = initialSpeed; 
        ResetText();
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        IncreaseSpeedOverTime(); 


        textTransform.Translate(Vector3.down * currentSpeed * Time.deltaTime);


        if (textTransform.anchoredPosition.y < -500f)
        {
            ResetText();
        }

    }

    void IncreaseSpeedOverTime()
    {

        currentSpeed = Mathf.Min(initialSpeed + (timeElapsed * speedIncreaseRate), maxSpeed);
    }

    void ResetText()
    {

        textUI.text = GenerateRandomWord();


        textUI.color = GenerateRandomColor();


        float randomX = Random.Range(xRange.x, xRange.y);
        textTransform.anchoredPosition = new Vector2(randomX, 300); 
    }

    string GenerateRandomWord()
    {

        string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        int length = Random.Range(3, 8);
        string randomWord = "";

        for (int i = 0; i < length; i++)
        {
            randomWord += characters[Random.Range(0, characters.Length)];
        }

        return randomWord;
    }

    Color GenerateRandomColor()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        return new Color(r, g, b);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            ResetText();
        }
    }
}
