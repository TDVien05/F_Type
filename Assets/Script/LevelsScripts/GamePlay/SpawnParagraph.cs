using System.Collections;
using System.Collections.Generic;
using Script.LevelsScripts.GamePlay;
using UnityEngine;
using TMPro;

public class SpawnParagraph : MonoBehaviour
{
    public GameObject textPrefab; // Đối tượng chứa chữ
    public Transform SpawnStartPosition; // Vị trí bắt đầu spawn
    private int spawnNumber = 5; // Số lượng chữ được sinh ra là 5
    private float spawnOffset = 4f; // Khoảng cách giữa các chữ
    public Vector3 SpawnPosition;

    // Quản lý WORD được sinh ra
    private int indexWord = 0; // Vị trí hiện tại của WORD
    private List<string> checkWords = new List<string>();

    // Quản lý trạng thái trò chơi
    public bool completedText = false;
    private string[] words;

    private RandomParagraph randomParagraph;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    private void Start()
    {
        randomParagraph = FindObjectOfType<RandomParagraph>();
        string theme = randomParagraph.theme;

        words = theme.Split(' '); // Chia theme thành các từ
        SpawnAllObjects(); // Sinh tất cả các đối tượng chữ
    }

    // Hàm spawn tất cả các đối tượng chữ
    private void SpawnAllObjects()
    {
        SpawnPosition = SpawnStartPosition.position;
        for (int i = 0; i < spawnNumber; i++)
        {
            GameObject spawned = Spawn();
            spawnedObjects.Add(spawned);
            SpawnPosition.x += spawnOffset; // Di chuyển vị trí spawn cho đối tượng tiếp theo
        }
    }

    // Hàm spawn một đối tượng chữ
    public GameObject Spawn()
    {
        GameObject gameObject = Instantiate(textPrefab, SpawnPosition, Quaternion.identity);
        TextMeshPro textMesh = gameObject.GetComponent<TextMeshPro>(); // Lấy TextMeshPro của đối tượng vừa sinh
        if (textMesh != null)
        {
            textMesh.text = GenerateWord(); // Gán từ được tạo vào TextMeshPro
        }
        return gameObject;
    }

    // Hàm tạo từ mới từ mảng words
    public string GenerateWord()
    {
        string selectedWord = words[indexWord];
        do
        {
            if (checkWords.Contains(selectedWord))
            {
                indexWord = indexWord + 1;
                selectedWord = words[indexWord];
            }
            else
            {
                checkWords.Add(selectedWord);
            }
        } while (checkWords.Contains(selectedWord));

        return words[indexWord];
    }

    // Trả về danh sách các đối tượng đã được sinh ra
    public List<GameObject> GetSpawnedObjects()
    {
        return spawnedObjects;
    }
}
