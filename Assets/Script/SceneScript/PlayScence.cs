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
                case "TEXT":
                    SceneManager.LoadScene("30s");
                    break;
                case "NORMAL":
                    SceneManager.LoadScene("60s");
                    break;
                case "FAILURE":
                    SceneManager.LoadScene("Unlimitted");
                    break;
            }
        }
        else
        {
            Debug.Log("File does not exits: " + filePath);
        }
    }
}
