using System.Collections;
using System.Collections.Generic;
using GameLogic;
using UnityEngine;
using UnityEngine.UI;

namespace Script.LevelsScripts.GamePlay
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 5f;
        private Rigidbody2D rb;
        private Vector2 movement;

        /// <summary>
        /// Mapping word and its position using dictionary collection
        /// Reference to player and all obstacles on the scene
        /// </summary>
        public GameObject player;

        private List<Text> _obstacles;
        private Dictionary<string, Obstacle> ObstacleMap = new Dictionary<string, Obstacle>();
        private Vector3 prevPosition;

        /// 

        public GameObject bullet; // reference to bullet object and               

        public Transform firePoint; // bullet respawn position
        public Canvas spawnTextCanvas;
        public Camera cam;

        IEnumerator Start()
        {
            yield return new WaitForSeconds(0.1f); // wait for text generation
            rb = GetComponent<Rigidbody2D>();
            LoadObstaclesDic();
            prevPosition = player.transform.position;
        }


        // Check player's input key for every frames
        void Update()
        {
            if (Input.anyKeyDown)
            {
                string key = Input.inputString;
                // CheckInputKey(key);
                Shoot();
            }
        }

        /// <summary>
        /// This function is going to retrieve all the obstacles on the scene
        /// Mapped their text and position to the dictionary
        /// </summary>
        private void LoadObstaclesDic()
        {
            SpawnAndMoveText spawnTexts = spawnTextCanvas.GetComponent<SpawnAndMoveText>();
            if (spawnTexts == null) return;
            _obstacles = spawnTexts.GetListWords();
            foreach (Text text in _obstacles)
            {
                string positionKey = text.GetComponent<ObstacleController>().GetNextText();
                while (ObstacleMap.ContainsKey(positionKey))
                {
                    Debug.Log(positionKey + " has been existed");
                    Destroy(text.gameObject);
                    Text tmp = spawnTexts.GenerateNewText();
                    positionKey = tmp.GetComponent<ObstacleController>().GetNextText();
                    Debug.Log(positionKey + " new key added");
                }

                ObstacleMap.Add(positionKey, new Obstacle
                {
                    Text = text,
                    LeftoverText = text.text.Substring(1)
                });
            }

            foreach (KeyValuePair<string, Obstacle> kvp in ObstacleMap)
            {
                Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value.Text.transform.position}");
            }
            Debug.Log("Number of obstacles map " + ObstacleMap.Count);
        }


        void CheckInputKey(string key)
        {
            if (ObstacleMap.ContainsKey(key) && IsObjectVisible(cam, ObstacleMap[key].Text.transform))
            {
                float angle = GetAngle(player.transform.position, ObstacleMap[key].Text.transform.position, prevPosition);
                player.transform.rotation = Quaternion.Euler(0, 0, angle);
                prevPosition = player.transform.position;
                Shoot();
                if (ObstacleMap[key].HasNextText())
                {
                    string positionKey = ObstacleMap[key].LeftoverText.Substring(0, 1);
                    ObstacleMap[key].LeftoverText = ObstacleMap[key].LeftoverText.Substring(1);
                    ReWordMap(positionKey, ObstacleMap[key]);
                }
                ObstacleMap.Remove(key);
            }
            else
            {
                Debug.Log($"{key} not found or object out of bounds");
            }
        }
        void ReWordMap(string key,Obstacle obstacle)
        {
            ObstacleMap.Add(key,obstacle);
        }
        bool IsObjectVisible(Camera cam, Transform obj)
        {
            // Convert the object's position to viewport coordinates
            Vector3 viewportPoint = cam.WorldToViewportPoint(obj.position);

            // Check if the object is within the viewport bounds
            bool isVisible = viewportPoint.x is >= 0 and <= 1 &&
                             viewportPoint.y is >= 0 and <= 1 &&
                             viewportPoint.z > 0; 
            return isVisible;
        }
    

        private float GetAngle(Vector2 playerPosition, Vector2 targetPosition, Vector3 prevPosition)
        {
            Vector2 direction = targetPosition - playerPosition;
            float angle = Mathf.Atan2(targetPosition.y - player.transform.position.y, 
                targetPosition.x - player.transform.position.x) * Mathf.Rad2Deg;


            // change the angle on the right side
            // if (targetPosition.x > 0)
            //     if (targetPosition.x > prevPosition.x)
            //         angle *= -1;
            //
            // // change the angle on the left side
            // if (targetPosition.x < 0)
            //     if (targetPosition.x > prevPosition.x)
            //         angle *= -1;
            return angle;
        }
        private void Shoot()
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
    }
}
