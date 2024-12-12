using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private Vector3 bottomP;
    void Start()
    {
        Vector3 bottomP = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0f, 0f));
        Debug.Log("Bottom Screen Position: " + bottomP);
    }

    void Update()
    {
        //Kiem tra xem ke dich co cham vao day man hinh khong
        CheckEnemiesFalling();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("text"))
        {
            GameOver();
        }
    }

    private void CheckEnemiesFalling()
    {
        // Lay tat ca doi tuong co tag "text"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("text");
        foreach (GameObject enemy in enemies)
        {
            if (enemy.transform.position.y < -1.5)
            {
                GameOver();
                return;
            }
        }
    }

    private void GameOver()
    {
        // Cho mot thoi gian truoc khi chuyen canh
        Invoke("LoadGameOverScene", 0f);
    }

    private void LoadGameOverScene()
    {
        // Chuyen sang scene "GameOver"
        SceneManager.LoadScene("GameOver");
    }
}