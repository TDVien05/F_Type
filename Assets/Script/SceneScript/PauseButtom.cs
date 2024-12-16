using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Script.SceneScript
{
    public class PauseButtom : MonoBehaviour
    {
        public Sprite playIcon; // Play image
        public Sprite pauseIcon; // Pause image

        private bool isPaused = false; // Status (playing/ pausing)
        private Image buttonImage; // Image of buttom

        void Start()
        {
            buttonImage = GetComponent<Image>(); 
            buttonImage.sprite = pauseIcon; // Default is pause image
        }

        public void TogglePause()
        {
            isPaused = !isPaused; 

            if (isPaused)
            {
                Time.timeScale = 0; // Stop the scene
                buttonImage.sprite = playIcon; 
            }
            else
            {
                Time.timeScale = 1; // Continue scene
                buttonImage.sprite = pauseIcon; 
            }
        }
        
        public bool IsPaused() {return isPaused;}
    }
}
