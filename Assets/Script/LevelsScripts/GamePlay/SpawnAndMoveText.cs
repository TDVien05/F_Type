using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnAndMoveText : MonoBehaviour
{
    public GameObject textPrefab;
    public Canvas canvas;
    public float fallSpeed = 50f;
    public string[] words = new string[50]
    {
        "apple", "banana", "cherry", "dream", "eagle", "forest", "garden", "honey", "island", "jungle",
        "kangaroo", "lemon", "mountain", "nebula", "ocean", "planet", "queen", "river", "sunshine", "tiger",
        "umbrella", "valley", "whale", "xylophone", "yacht", "zebra", "adventure", "blossom", "cloud", "diamond",
        "energy", "freedom", "galaxy", "horizon", "imagine", "jewel", "kindness", "lighthouse", "miracle", "nature",
        "oasis", "paradise", "quest", "rainbow", "starlight", "tranquility", "unity", "voyage", "wisdom", "zenith"
    };

    public int numberOfTexts = 4;
    public float spawnInterval = 1f;
    public float[] positionX = new float[6] { -150, -200, -300, 0, 450, 300 };
    private List<GameObject> activeTexts = new List<GameObject>();
    private List<RectTransform> textRects = new List<RectTransform>();
    private List<Text> listWords = new List<Text>();

    void Start()
    {
        StartCoroutine(SpawnTextsOverTime());
    }

    void Update()
    {
        MoveTextsDown();
    }

    IEnumerator SpawnTextsOverTime()
    {
        for (int i = 0; i < numberOfTexts; i++)
        {
            SpawnSingleText();
            yield return new WaitForSeconds(spawnInterval); 
        }
    }

    void SpawnSingleText()
    {
        GameObject newText = Instantiate(textPrefab, canvas.transform);
        RectTransform newTextRect = newText.GetComponent<RectTransform>();

        if (newTextRect != null)
        {        
            newTextRect.anchoredPosition = new Vector2(Random.Range(-450f, 450f), 270);
            activeTexts.Add(newText);
            textRects.Add(newTextRect);
            UpdateTextProperties(newText);
        }
        listWords.Add(newText.GetComponent<Text>()); 
    }

    void UpdateTextProperties(GameObject textObj)
    {
        Text textComponent = textObj.GetComponent<Text>();
        if (textComponent != null)
        {
            textComponent.text = words[Random.Range(0, words.Length)];
            textComponent.color = GetRandomColor();
        }
        fallSpeed += 0.1f;
        listWords.Add(textComponent);
    }

    public List<Text> getListWords()
    {
        return new List<Text>(listWords);
    }

    void MoveTextsDown()
    {
        for (int i = 0; i < activeTexts.Count; i++)
        {
            RectTransform currentTextRect = textRects[i];
            if (currentTextRect != null)
            {
                currentTextRect.anchoredPosition -= new Vector2(0, fallSpeed * Time.deltaTime);
                if (currentTextRect.anchoredPosition.y <= 0)
                {
                    ResetText(i);
                }
            }
        }
    }

    void ResetText(int index)
    {
        RectTransform currentTextRect = textRects[index];
        if (currentTextRect != null)
        {
            currentTextRect.anchoredPosition = new Vector2(Random.Range(-450f, 450f), 270);
            UpdateTextProperties(activeTexts[index]);
        }
    }

    Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            ResetAllTexts();
        }
    }

    void ResetAllTexts()
    {
        for (int i = 0; i < activeTexts.Count; i++)
        {
            ResetText(i);
        }
    }
}
