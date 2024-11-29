using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class PlayScence : MonoBehaviour
{
    public void change()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "Level.txt");
        if (File.Exists(filePath))
        {
            string level = File.ReadAllText(filePath);
            level = level.ToUpper();
            Debug.Log("Content of file: " + level);
            switch (level)
            {
                case "30S":
                    SceneManager.LoadScene("30s");
                    break;
                case "60S":
                    SceneManager.LoadScene("60s");
                    break;
                case "FAILURE":
                    SceneManager.LoadScene("Unlimitted");
                    break;
            }
        }
        else
        {
            SceneManager.LoadScene("30s");
            Debug.Log("File does not exits: " + filePath);
        }
    }
}
