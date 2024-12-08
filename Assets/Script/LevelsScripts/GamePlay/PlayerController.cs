using System.Collections;
using System.Collections.Generic;
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
        /// Reference to player, spawn base and all obstacles on the scene
        /// </summary>
        public GameObject player;
        public GameObject baseOject;
        private List<GameObject> _obstacles;
        // map the hold words on the scene
        private readonly Dictionary<string, TextMeshPro> _obstacleMap = new Dictionary<string, TextMeshPro>();
        // map the current word
        private Dictionary<string, TextMeshPro> _localTextMap = new Dictionary<string, TextMeshPro>();
        private bool _isLocalText;
        /// 

        public GameObject bullet; // reference to bullet prefab and               
        public Transform firePoint; // bullet respawn position
        public Camera cam;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.1f); // wait for text generation
            rb = GetComponent<Rigidbody2D>();
            _isLocalText = false; // set to local words scope first
            LoadObstaclesDic();
        }


        // Check player's input key for every frames
        void Update()
        {
            if (Input.anyKeyDown)
            {
                string key = Input.inputString;
                CheckInputKey(key);
            }
        }

        /// <summary>
        /// This function is going to retrieve all the obstacles on the scene
        /// through spawn base object.
        /// Mapped their text and position to the dictionary.
        /// </summary>
        private void LoadObstaclesDic()
        {
            SpawnSpace spawned = baseOject.GetComponent<SpawnSpace>();
            if (spawned == null) return; // return if no spawn base found
            _obstacles = spawned.GetListWords();
            Debug.Log("Obstacle List: " + _obstacles.Count);
            foreach (var item in _obstacles)
            {
                TextMeshPro tmp = item.GetComponentInChildren<TextMeshPro>();
                ObstacleController controller = tmp.GetComponent<ObstacleController>();
                
                // regenerate if the key is existed 
                while (_obstacleMap.ContainsKey(controller.GetNextText()))
                {
                    // regenerate other Obstacle
                    Debug.Log($"Key {controller.GetNextText()} existed. Regenerate another obstacle");
                    var reGenerateObject = spawned.Spawn();
                    tmp = reGenerateObject.GetComponentInChildren<TextMeshPro>();
                    controller = tmp.GetComponent<ObstacleController>(); 
                }
                _obstacleMap.Add(controller.GetNextText(), tmp);
                Debug.Log($"key: {controller.GetNextText()}, value: {tmp.text}");
            }
            Debug.Log("size of map: " + _obstacleMap.Count);
        }
        
        /// <summary>
        /// This function checks input key by 2 options:
        /// + Current typing word (local text)
        ///     - In this case, it will search through the _localTextMap dictionary for desired key 
        /// + Any scene words (global text)
        ///     - In this case, it will search through the _obstacleMap dictionary for desired key 
        /// </summary>
        /// <param name="key"> input from player </param>
        void CheckInputKey(string key)
        {
            if (_isLocalText)
            {
                if (_localTextMap.ContainsKey(key))
                {
                    TextMeshPro obstacleText = _localTextMap[key];
                    RotatePlayerTowardsTarget(obstacleText.gameObject);
                    Shoot();
                    // upgrade key and value for leftover text
                    _localTextMap = ReLocalTextMap(obstacleText);
                    if(obstacleText.text.Length == 0) 
                        _isLocalText = false;
                }
                else
                {
                    Debug.Log($"{key} key not found in local text map");
                }
            }
            else
            {
                if (_obstacleMap.ContainsKey(key))
                {
                    TextMeshPro obstacleText = _obstacleMap[key];
                    ObstacleController controller = obstacleText.GetComponent<ObstacleController>();
                    // if the valid key and is visible by player
                    if (IsObjectVisible(obstacleText.transform))
                    {
                        RotatePlayerTowardsTarget(obstacleText.gameObject);
                        Shoot();
                        // turn to local text context
                        _isLocalText = true;
                        _localTextMap = ReLocalTextMap(obstacleText);
                        controller.SetTyping(true);
                    }
                    else
                    {
                        Debug.Log(obstacleText.text + " isn't visible");
                    }
                }
                else
                {
                    Debug.Log($"{key} key not found in obstacle map");
                }
            }
        }
        
        // this function will reassign pair key and value of the _localTextMap for each valid key
        private Dictionary<string, TextMeshPro> ReLocalTextMap(TextMeshPro obstacleText)
        {
            Dictionary<string, TextMeshPro> localTextMap = new Dictionary<string, TextMeshPro>(); // new dictionary
            ObstacleController controller = obstacleText.GetComponent<ObstacleController>();
            obstacleText.text = controller.GetSubText(); // upgrade leftover text 
            string key = controller.GetNextText();
            if(!localTextMap.ContainsKey(key) && key.Length > 0)
                localTextMap.Add(key, obstacleText);
                
            return localTextMap;
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
    
        // calculate angle for player's rotation 
        private void RotatePlayerTowardsTarget(GameObject target)
        {
            // Calculate direction to the target
            Vector2 directionToTarget = (target.transform.position - player.transform.position).normalized;

            // Get the angle in degrees relative to the player's current forward direction
            float angleToTarget = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
            angleToTarget -= 90;
            
            // Apply rotation
            player.transform.rotation = Quaternion.Euler(0, 0, angleToTarget);
        }

        
        private void Shoot()
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
    }
}
