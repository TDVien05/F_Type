using System.Collections.Generic;
using UnityEngine;

namespace Script.GamePlay.TextControl
{
    public class SpawnSpace : MonoBehaviour
    {
        public GameObject objectToSpawn; // Gan prefabs 
        public Transform spawnPoint;    // Diem spawn
        private int spawnLimit = 4;      // So luong prefabs muon spawn
        public List<float> checkSpawnSpaceX = new List<float>();

        private List<float> checkRandomTime = new List<float>();
        private int checkIndexDelayTime = 0;
        private List<GameObject> spawnedObjects = new List<GameObject>(); // Mang luu cac prefabs spawn

        private void Start()
        {
            // Ham tao cac prefabs mot lan
            SpawnAllObjects();
            SetDelayTimeToMovingDown();
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

        private float RandomTimeToDelay()
        {
            float[] randomTime = new float[4]
            {
                1f, 2.5f, 4.5f, 6.5f
            };
            float ans = randomTime[checkIndexDelayTime];
            checkIndexDelayTime++;
            return ans;
        }

        private void SetDelayTimeToMovingDown()
        {
            float delay;
            for (int i = 0; i < spawnedObjects.Count; i++)
            {
                delay = RandomTimeToDelay();
                spawnedObjects[i].GetComponentInChildren<TextController>().SetDelayTime(delay);
            }
            Debug.Log("Done setting delay time!");
        }

        public float RanDomX()
        {
            float randomX = Random.Range(-8f, 8);
            do
            {
                if (checkSpawnSpaceX.Contains(randomX))
                {
                    randomX = Random.Range(-8f, 8);
                } else
                {
                    checkSpawnSpaceX.Add(randomX);
                    break;
                }
            } while(checkSpawnSpaceX.Contains(randomX));
            return randomX;
        }

        public float RanDomY()
        {
            return transform.position.y;
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
}
