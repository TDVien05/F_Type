using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private Vector3 bottomP;
    private float bottomY;
    void Start()
    {
        // Lấy tọa độ đáy màn hình 
        bottomY = Camera.main.transform.position.y - Camera.main.orthographicSize;
        Debug.Log("Calculated Bottom Y: " + bottomY);
    }

    void Update()
    {
        // Kiểm tra xem kẻ địch có chạm vào đáy màn hình không
        CheckEnemiesFalling();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra va chạm với đối tượng có tag "text"
        if (collision.CompareTag("text"))
        {
            Debug.Log("Trigger with tag 'text' detected!");
            GameOver();
        }
    }

    private void CheckEnemiesFalling()
    {
        // Lay tat ca doi tuong co tag "text"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("text");

        foreach (GameObject enemy in enemies)
        {
            if (enemy.transform.position.y < bottomY)
            {
                GameOver();
                return;
            }
        }
    }

    private void GameOver()
    {
        // Chuyen sang scene "GameOver"
        SceneManager.LoadScene("GameOver");
    }
}