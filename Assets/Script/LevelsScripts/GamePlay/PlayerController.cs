using System.Collections;
using System.Collections.Generic;
using GameLogic;
using JetBrains.Annotations;
using UnityEngine;
using TMPro;
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
        public GameObject baseOject;
        private List<GameObject> _obstacles;
        private Dictionary<string, TextMeshPro> ObstacleMap = new Dictionary<string, TextMeshPro>();

        /// 

        public GameObject bullet; // reference to bullet object and               

        public Transform firePoint; // bullet respawn position
        public Camera cam;

        public GameObject target1;

        IEnumerator Start()
        {
            yield return new WaitForSeconds(0.1f); // wait for text generation
            rb = GetComponent<Rigidbody2D>();
            LoadObstaclesDic();

        }


        // Check player's input key for every frames
        void Update()
        {
            if (Input.anyKeyDown)
            {
                string key = Input.inputString;
                CheckInputKey(key);
                RotatePlayerTowardsTarget(target1);
                
                Shoot();
            }

        }

        /// <summary>
        /// This function is going to retrieve all the obstacles on the scene
        /// Mapped their text and position to the dictionary
        /// </summary>
        private void LoadObstaclesDic()
        {
            // SpawnSpace spawned = baseOject.GetComponent<SpawnSpace>();
            // if (spawned == null) return;
            // _obstacles = spawned.GetListWords();
            // foreach (GameObject item in _obstacles)
            // {
            //     TextMeshPro tmp = item.GetComponent<TextMeshPro>();
            //     ObstacleController controller = tmp.GetComponent<ObstacleController>();
            //     ObstacleMap.Add(controller.GetNextText(), tmp);
            //     
            //     Debug.Log($"key: {controller.GetNextText()} , value: {tmp.text}");
            // }
            // Debug.Log("size of map: " + _obstacles.Count);
        }
    



        void CheckInputKey(string key)
        {
            
        }

        void ReWordMap(string key,Obstacle obstacle)
        {
            // ObstacleMap.Add(key,obstacle);
        }
        bool IsObjectVisible(Transform obj)
        {
            // Convert the object's position to viewport coordinates
            Vector3 viewportPoint = cam.WorldToViewportPoint(obj.position);

            // Check if the object is within the viewport bounds
            bool isVisible = viewportPoint.x is >= 0 and <= 1 &&
                             viewportPoint.y is >= 0 and <= 1;
            return isVisible;
        }
    
        private void RotatePlayerTowardsTarget(GameObject target)
        {
            // Calculate direction to the target
            Vector2 directionToTarget = (target.transform.position - player.transform.position).normalized;

            // Get the angle in degrees relative to the player's current forward direction
            float angleToTarget = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
            angleToTarget -= 90;
            
            // Apply rotation
            player.transform.rotation = Quaternion.Euler(0, 0, angleToTarget);

            Debug.Log("Player rotated to angle: " + angleToTarget);
        }

        
        private void Shoot()
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
    }
}
