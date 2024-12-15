using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParagraphSpawn : MonoBehaviour
{
    public GameObject paragraphPrefabs;// Gan prefabs
    public Transform spawnPoint;// Diem spawn
    private int numberOfPrefabs = 5;// So luong prefabs muon spawn 
    private List<GameObject> spawnedparagraphs = new List<GameObject>();// Mang luu cac prefabs da spawn
    private int indexOfPositions = 0;
    private float[] positions = new float[5]
    {
        -7.45f, -3.96f, -0.58f, 3.33f, 6.75f
    };
    private string[] wordsList;
    private int indexOfWordsList = 0;

    private bool isEnd = false;

    void Start()
    {
        SpawnAllParagraphs();
        CreateWordsList();
        SetWordsToPrefabs();
    }

    // Ham tao words list
    public void CreateWordsList()
    {
        int random = Random.Range(1, 3);
        string list = ChooseTopic(random);
        wordsList = list.Split(' ');
    }

    // Spawn tat ca prefabs
    void SpawnAllParagraphs()
    {
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            GameObject paragraph = Spawn();
            spawnedparagraphs.Add(paragraph);
        }
        Debug.Log("Spawned all paragraphs");
    }
    
    // Spawn prefab
    GameObject Spawn()
    {
        float x = positions[indexOfPositions];
        indexOfPositions++;
        float y = spawnPoint.position.y;
        Vector3 position = new Vector3(x, y, 0);
        GameObject gameObject = Instantiate(paragraphPrefabs, position, Quaternion.identity);
        return gameObject;
    }

    // Random lay topic
    string ChooseTopic(int number)
    {
        string topic = "Hello worlds. Haha haha";
        switch (number)
        {
            case 1:
                topic = "My name is Vien. I am 20 years old. I am a engineer of technology.";
                break;  
            case 2:
                topic = "My name is Quang. I am 20 years old. I am a engineer of technology.";
                break;
            case 3: 
                topic = "My name is Hieu. I am 20 years old. I am a engineer of technology.";
                break;
        }
        return topic;
    }

    // Set cho all prefabs tu ban dau
    void SetWordsToPrefabs()
    {
        Debug.Log("The size of array of prefabs : " + spawnedparagraphs.Count);
        Debug.Log("The size of words list : " + wordsList.Length);
        for (int i = 0; i < spawnedparagraphs.Count; i++)
        {
            SetWords(spawnedparagraphs[i].GetComponentInChildren<TMP_Text>());
            Debug.Log(spawnedparagraphs[i].GetComponentInChildren<TMP_Text>().text);
        }
    }

    // Set chu tu topic vao prefabs
    public void SetWords(TMP_Text Text)
    {
        Debug.Log("The index of words list is : " + indexOfWordsList);
        Text.text = wordsList[indexOfWordsList];
        indexOfWordsList++;
        if (indexOfWordsList >= wordsList.Length)
        {
            isEnd = true;
        }
    }

    public bool GetIsEnd()
    {
        return isEnd;
    }

    // Lay ra chuoi topic
    public string[] GetListWords()
    {
        return wordsList;
    }
}
