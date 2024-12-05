using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnAndMoveText : MonoBehaviour
{
    public GameObject textPrefab; // Gan text prefab
    public Canvas canvas; // Canvas dung de hien thi text prefabs
    public float fallSpeed = 50f; // Toc do roi cua text (co the chinh sua ben ngoai
    public string[] words = new string[50]
    {
        "apple", "banana", "cherry", "dream", "eagle", "forest", "garden", "honey", "island", "jungle",
        "kangaroo", "lemon", "mountain", "nebula", "ocean", "planet", "queen", "river", "sunshine", "tiger",
        "umbrella", "valley", "whale", "xylophone", "yacht", "zebra", "adventure", "blossom", "cloud", "diamond",
        "energy", "freedom", "galaxy", "horizon", "imagine", "jewel", "kindness", "lighthouse", "miracle", "nature",
        "oasis", "paradise", "quest", "rainbow", "starlight", "tranquility", "unity", "voyage", "wisdom", "zenith"
    }; // Mang de tao ra cac tu co nghia

    public int numberOfTexts = 4; // So luong text prefabs se duoc tao ra
    public float spawnInterval = 1f; // Thoi gian cach nhau de tao text prefabs (neu spam ra mot lan se kho de go va vi tri sinh co the trung
    private List<GameObject> activeTexts = new List<GameObject>(); // Mang de chua 4 text prefabs da duoc tao
    private List<RectTransform> textRects = new List<RectTransform>(); // Mang chua React Transform cua 4 prefabs
    private List<Text> listWords = new List<Text>(); // Mang Text de lay ra

    void Start()
    {
        StartCoroutine(SpawnTextsOverTime());  
    }

    void Update()
    {
        MoveTextsDown();
    }
    // Phuong thuc SpawnTextOverTime va SpawnSingleText text sau mot khoang thoi gian (chia ra => de quan li)
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
    // Sinh text voi word va color moi+
    void UpdateTextProperties(GameObject textObj)
    {
        Text textComponent = textObj.GetComponent<Text>();
        if (textComponent != null)
        {
            textComponent.text = words[Random.Range(0, words.Length)]; // Lay words random tu mang
            textComponent.color = GetRandomColor();
        }
        fallSpeed += 0.1f; // Sau khi sinh words moi se tang do roi (do kho tang dan cho game)
        listWords.Add(textComponent);
    }

    public List<Text> getListWords()
    {
        return new List<Text>(listWords); // Lay ra mot mang bao gom cac Text
    }
    //Phuong thuc di chuyen text di xuong
    void MoveTextsDown()
    {
        for (int i = 0; i < activeTexts.Count; i++)
        {
            RectTransform currentTextRect = textRects[i];
            if (currentTextRect != null)
            {
                currentTextRect.anchoredPosition -= new Vector2(0, fallSpeed * Time.deltaTime);            
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

    // Text prefabs nao va cham voi bullet se reset lai 
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            for (int i = 0; i < activeTexts.Count; i++)
            {
                ResetText(i);
            }
        }
    }
}
