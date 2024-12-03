using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class PlayScence : MonoBehaviour
{
    public void change()
    {
        SceneManager.LoadScene("GameProgression");
    }
}
