using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    private GameObject[] obstacles;
    private Dictionary<string, GameObject> ObstacleMap = new Dictionary<string, GameObject>();
    private Vector3 prevPosition;
    /// 
  
    public GameObject bullet;    // reference to bullet object and               
    public Transform firePoint;  // bullet respawn position

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LoadObstaclesDic();
        prevPosition = player.transform.position;
    }


    // Check player's input key for every frames
    void Update()
    {
        // Get input for horizontal and vertical movement
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");
        //movement = new Vector2(horizontal, vertical);

        if (Input.anyKeyDown)
        {
            string key = Input.inputString;
            if (ObstacleMap.ContainsKey(key))
            {
                // if the key is mapped in dictionary and still exist
                if (Exist(key))
                {
                    Vector2 direction = (Vector2)ObstacleMap[key].transform.position - (Vector2)player.transform.position;

                    // Calculate the angle between player object 
                    // and target object
                    float angle = Vector2.Angle(player.transform.up, direction);
                    Debug.Log(angle);

                    //// change the angle on the right side
                    if (ObstacleMap[key].transform.position.x > 0)
                        if (ObstacleMap[key].transform.position.x > prevPosition.x)
                            angle *= -1;

                    // change the angle on the left side
                    if (ObstacleMap[key].transform.position.x < 0)
                        if (ObstacleMap[key].transform.position.x > prevPosition.x)
                            angle *= -1;


                    // Rotate the player according to calculated angle
                    player.transform.Rotate(0.0f, 0.0f, angle, Space.Self);
                    prevPosition = ObstacleMap[key].transform.position;
                    Shoot();
                }else
                {
                    Debug.Log($"Remove obstacle {key} from dictionary");
                    ObstacleMap.Remove(key);    
                }
            }
        }
    }

    void FixedUpdate()
    {
        // Apply movement to the Rigidbody2D
        rb.velocity = movement * speed;
    }


    /// <summary>
    /// This function is going to retrieve all the obstacles on the scene
    /// Mapped their text and position to the dictionary
    /// </summary>
    private void LoadObstaclesDic() {
        obstacles = GameObject.FindGameObjectsWithTag("obstacle");
        Debug.Log("Size of obstacles: " + obstacles.Length);
        TMP_Text text; // key for each position
        for (int i = 0; i < obstacles.Length; i++)
        {
            text = obstacles[i].GetComponentInChildren<TMP_Text>();
            if (text != null)
            {
                ObstacleMap.Add(text.text, obstacles[i]);
            }
        }

        Debug.Log("Size of dic: " + ObstacleMap.Count);
    }


    // Check if the obstacle still alive
    private bool Exist(string key)
    {
        GameObject checker = ObstacleMap[key];
        ObstacleController heal = checker.GetComponent<ObstacleController>();
        if (heal.GetHeal() <= 0)
            return false;
        return true;
    }


    private void Shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}
