﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
    {
        public GameObject player; // Tham chieu den doi tuong la nguoi choi

        void Update()
        {
            //Kiem tra xem ke dich co cham vao day man hinh khong
            CheckEnemiesFalling();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // kiem tra tag 'Enemy' cua doi tuong va cham
            if (other.CompareTag("obstacle"))
            {
                GameOver();
            }
        }

    private void CheckEnemiesFalling()
        {
            // Lay tat ca doi tuong co tag "Enemy"
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("obstacle");

            foreach (GameObject enemy in enemies)
            {
                if (enemy.transform.position.y <= 0)
                {
                    GameOver();
                    return;
                }
            }
        }

    private void GameOver()
        {
            // Chờ một 1 thoi gian trước khi chuyển cảnh
            Invoke("LoadGameOverScene", 0f);
        }

        private void LoadGameOverScene()
        {
            // Chuyển sang scene "GameOver"
            SceneManager.LoadScene("GameOver");
        }
    }