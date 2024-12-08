using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpace : MonoBehaviour
{
    public GameObject objectToSpawn; // Gan prefabs 
    public Transform spawnPoint;    // Diem spawn
    public int spawnLimit = 10;      // So luong prefabs muon spawn
    public List<float> checkSpawnSpaceX = new List<float>();
    public List<float> checkSpawnSpaceY = new List<float>();
    private float[] _spaceUnitY = new float[4]
    {
        2, 3, 4, 5
    };
    private List<GameObject> spawnedObjects = new List<GameObject>(); // Mang luu cac prefabs spawn

    private void Start()
    {
        // Ham tao cac prefabs mot lan
        SpawnAllObjects();
    }

    private void SpawnAllObjects()
    {
        for (int i = 0; i < spawnLimit; i++)
        {
            // Tao prefabs va luu vao mang
            GameObject spawned = Spawn();
            spawnedObjects.Add(spawned);
        }

        Debug.Log("Done spawning all objects!");
    }

    public float RanDomX()
    {
        float randomX = Random.Range(-9f, 9);
        do
        {
            if (checkSpawnSpaceX.Contains(randomX))
            {
                randomX = Random.Range(-9f, 9);
            }
            else
            {
                checkSpawnSpaceX.Add(randomX);
                break;
            }
                
        } while(checkSpawnSpaceX.Contains(randomX));
        return randomX; 
    }

    public float RanDomY()
    {
        int index = Random.Range(0, _spaceUnitY.Length);
        float randomY = spawnPoint.position.y + _spaceUnitY[index];
        do
        {
            if (checkSpawnSpaceY.Contains(randomY))
            {
                index = Random.Range(0, _spaceUnitY.Length);
                randomY = spawnPoint.position.y + _spaceUnitY[index];
            }
            else
            {
                checkSpawnSpaceY.Add(randomY);
                break;
            }
        } while (checkSpawnSpaceY.Contains(randomY));
        return randomY;
    }

    public GameObject Spawn()
    {
        // Vi tri ngau nhien tren truc X (chon toi khi khong con sinh ra tai mot vi tri tren truc X)
        float x = RanDomX();
        float y = RanDomY();
        Vector3 spawnPosition = new Vector3(x, y, 0);
        GameObject gameObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        return gameObject;
    }

    // Lay mang
    public List<GameObject> GetListWords()
    {
        return spawnedObjects;
    }
}
