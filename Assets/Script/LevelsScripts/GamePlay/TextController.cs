using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    public float speed = 0.001f; // Toc do roi
    private TextMeshPro textMesh; // Text cua prefabs
    private Vector3 startPosition;
    private List<TextMeshPro> listWords = new List<TextMeshPro>();
    private void Start()
    {
        // Lay textMeshPro tu prefabs
        textMesh = GetComponentInChildren<TextMeshPro>();

        if (textMesh != null)
        {
            startPosition = transform.position;
            generateWords(textMesh);
        }
    }

    private void Update()
    {
        // Di chuyen prefabs di xuong
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y <= 0f)
        {
            transform.position = startPosition;
            generateWords(textMesh);
        }
    }

    public void generateWords(TextMeshPro textMesh)
    {
        // Tao tu ngau nhien
        textMesh.text = GenerateRandomWord();

        // Tao mau ngau nhien
        textMesh.color = GenerateRandomColor();
    }

    // Tao chu ngau nhien
    private string GenerateRandomWord()
    {
        string[] randomWords = {   "apple", "banana", "cherry", "dream", "eagle", "forest", "garden", "honey", "island", "jungle",
            "kangaroo", "lemon", "mountain", "nebula", "ocean", "planet", "queen", "river", "sunshine", "tiger",
            "umbrella", "valley", "whale", "xylophone", "yacht", "zebra", "adventure", "blossom", "cloud", "diamond",
            "energy", "freedom", "galaxy", "horizon", "imagine", "jewel", "kindness", "lighthouse", "miracle", "nature",
            "oasis", "paradise", "quest", "rainbow", "starlight", "tranquility", "unity", "voyage", "wisdom", "zenith" };
        int randomIndex = Random.Range(0, randomWords.Length);
        return randomWords[randomIndex];
    }

    // Tao mau ngau nhien (chi chua cac mau sang, mau toi trung back ground => kho choi)
    private Color GenerateRandomColor()
    {
        float r = Random.Range(0.5f, 1f);
        float g = Random.Range(0.5f, 1f);
        float b = Random.Range(0.5f, 1f);

        return new Color(r, g, b);
    }
}
