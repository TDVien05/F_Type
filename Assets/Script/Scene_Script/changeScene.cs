using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    public string sceneName;
    public void change()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void MenuPage()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PlayPage()
    {
        SceneManager.LoadScene("ModePlay");
    }

    public void Model30()
    {
        SceneManager.LoadScene("30s");
    }

    public void Model60()
    {
        SceneManager.LoadScene("60s");
    }

    public void ModelUnlimit()
    {
        SceneManager.LoadScene("Unlimitted");
    }

    public void Cancel()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
