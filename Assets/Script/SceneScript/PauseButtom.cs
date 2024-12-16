using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Script.SceneScript
{
    public class PauseButtom : MonoBehaviour
    {
        private bool isPaused = false; // Default ispaused = false (game is continue)

        public void TogglePause()
        {
            if (isPaused)
            {
                Time.timeScale = 1f; // The game scene is work
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0f; // isPaused = false then the game scene is stop;
                isPaused = true;
            }
        }
        
        public bool IsPaused() {return isPaused;}
    }
}
