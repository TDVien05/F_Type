using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnAndMoveText : MonoBehaviour
{
    public GameObject textPrefab;// Dung de gan text prefabs 
    public Canvas canvas;//Dung de hien thi text prefabs
    public float fallSpeed = 50f;//Toc do roi cua text
    public string[] words = new string[50]
    {
        "apple", "banana", "cherry", "dream", "eagle", "forest", "garden", "honey", "island", "jungle",
        "kangaroo", "lemon", "mountain", "nebula", "ocean", "planet", "queen", "river", "sunshine", "tiger",
        "umbrella", "valley", "whale", "xylophone", "yacht", "zebra", "adventure", "blossom", "cloud", "diamond",
        "energy", "freedom", "galaxy", "horizon", "imagine", "jewel", "kindness", "lighthouse", "miracle", "nature",
        "oasis", "paradise", "quest", "rainbow", "starlight", "tranquility", "unity", "voyage", "wisdom", "zenith"
    };//Mang chua cac tu co nghia

    public int numberOfTexts = 4;//So luong text prefabs sinh ra
    public float spawnInterval = 1f;//Thoi gian delay roi trung binh cua tung prefabs
    private List<GameObject> activeTexts = new List<GameObject>();//Mang luu cac prefabs da sinh
    private List<RectTransform> textRects = new List<RectTransform>();//Mang luu React Transform cua cac prefabs
    private List<float> startDelays = new List<float>(); // Mang luu tung delay cho cac prefabs
    //Delay giup cho cac text khi roi xuong se khong bi trung voi nhau, tranh roi mat nguoi choi
    private List<Text> listWords  = new List<Text>();

    void Start()
    {
        SpawnAllTexts();
        
    }

    void Update()
    {
        MoveTextsDown();
    }

    void SpawnAllTexts()
    {
        for (int i = 0; i < numberOfTexts; i++)
        {
            GameObject newText = Instantiate(textPrefab, canvas.transform);
            RectTransform newTextRect = newText.GetComponent<RectTransform>();

            if (newTextRect != null)
            {
                newTextRect.anchoredPosition = new Vector2(Random.Range(-450f, 450f), 300);
                activeTexts.Add(newText);
                textRects.Add(newTextRect);
                startDelays.Add(Time.time + i * spawnInterval); // Moi prefabs se co mot delay thoi gian roi rieng
                UpdateTextProperties(newText);
            }
        }
    }
    
    void MoveTextsDown()
    {
        for (int i = 0; i < activeTexts.Count; i++)
        {
            RectTransform currentTextRect = textRects[i];
            if (currentTextRect != null && Time.time >= startDelays[i]) // Prefabs se roi xuong sau khi het delay
            {
                currentTextRect.anchoredPosition -= new Vector2(0, fallSpeed * Time.deltaTime);
            }
        }
    }

    void UpdateTextProperties(GameObject textObj)
    {
        Text textComponent = textObj.GetComponent<Text>();
        if (textComponent != null)
        {
            textComponent.text = words[Random.Range(0, words.Length)];
            textComponent.color = GetRandomColor();
        }
        listWords.Add(textComponent);
    }

    void ResetText(int index)
    {
        RectTransform currentTextRect = textRects[index];
        if (currentTextRect != null)
        {
            currentTextRect.anchoredPosition = new Vector2(Random.Range(-450f, 450f), 300);
            UpdateTextProperties(activeTexts[index]);
        }
    }



    // Text prefabs nao va cham voi bullet se reset lai 
    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("bullet"))
    //     {
    //         for (int i = 0; i < activeTexts.Count; i++)
    //         {
    //             ResetText(i);
    //         }
    //     }
    // }

    Color GetRandomColor()
    {
        float r = Random.Range(0.5f, 1f);
        float g = Random.Range(0.5f, 1f);
        float b = Random.Range(0.5f, 1f);

        return new Color(r, g, b);
    }
    
    public List<Text> GetListWords() {return listWords;}

    public Text GenerateNewText()
    {
        GameObject newText = Instantiate(textPrefab, canvas.transform);
        RectTransform newTextRect = newText.GetComponent<RectTransform>();

        if (newTextRect != null)
        {
            newTextRect.anchoredPosition = new Vector2(Random.Range(-450f, 450f), 300);
            activeTexts.Add(newText);
            textRects.Add(newTextRect);
            startDelays.Add(Time.time + Random.Range(0,10) * spawnInterval); // Moi prefabs se co mot delay thoi gian roi rieng
        }
        Text textComponent = newText.GetComponent<Text>();
        if (textComponent != null)
        {
            textComponent.text = words[Random.Range(0, words.Length)];
            textComponent.color = GetRandomColor();
        }

        return textComponent;
    }
}
