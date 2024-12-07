using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpace : MonoBehaviour
{
    public GameObject objectToSpawn; // Gan prefabs 
    public Transform spawnPoint;    // Diem spawn
    public int spawnLimit = 10;      // So luong prefabs muon spawn
    private float randomX;
    private float randomY;

    private float[] spaceUnitY = new float[4]
    {
        1, 2, 3, 4
    };
    private GameObject[] spawnedObjects; // Mang luu cac prefabs spawn

    private void Start()
    {
        // Tao mang bang so luong prefabs muon spawn
        spawnedObjects = new GameObject[spawnLimit];

        // Ham tao cac prefabs mot lan
        SpawnAllObjects();
    }

    private void SpawnAllObjects()
    {
        for (int i = 0; i < spawnLimit; i++)
        {
            // Vi tri ngau nhien tren truc X
            randomX = Random.Range(-9f, 9);
            int index = Random.Range(0, spaceUnitY.Length);
            randomY = spawnPoint.position.y + spaceUnitY[index]; 
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

            // Tao prefabs va luu vao mang
            GameObject spawned = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            spawnedObjects[i] = spawned;
        }

        Debug.Log("Done spawning all objects!");
    }

    // Lay mang
    public List<GameObject> getListWords()
    {
        if (spawnedObjects == null || spawnedObjects.Length == 0)
        {
            Debug.LogWarning("No objects have been spawned.");
            return new List<GameObject>();
        }

        return new List<GameObject>(spawnedObjects);
    }
}
