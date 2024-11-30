using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
    {
        public GameObject player; // Tham chieu den doi tuong la nguoi choi
        public float screenBottomY; //Y-coordinate cua day man hinh

        void Start()
        {
            screenBottomY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        }

        void Update()
        {
            //Kiem tra xem ke dich co cham vao day man hinh khong
            CheckEnemiesFalling();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Goi ham kiem tra neu ke dich va cham 
            CheckEnemyCollision(other);
        }

        void CheckEnemyCollision(Collider2D other)
        {
            // kiem tra tag 'Enemy' cua doi tuong va cham
            if (other.CompareTag("Enemy"))
            {
                GameOver();
            }
        }

    private void CheckEnemiesFalling()
        {
            // Lay tat ca doi tuong co tag "Enemy"
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                if (enemy.transform.position.y < screenBottomY)
                {
                    GameOver();
                    return;
                }
            }
        }

    private void GameOver()
        {
            // Chờ một 1s trước khi chuyển cảnh
            Invoke("LoadGameOverScene", 1f);
        }

        private void LoadGameOverScene()
        {
            // Chuyển sang scene "GameOver"
            SceneManager.LoadScene("GameOver");
        }
    }