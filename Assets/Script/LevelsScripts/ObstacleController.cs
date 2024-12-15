using System;
using Script.LevelsScripts.GamePlay;
using UnityEngine;
using TMPro;
namespace Script.LevelsScripts
{
    public class ObstacleController : MonoBehaviour
    {
        private TextMeshPro _textMesh; // text object
        public TMP_Text text; // prefab text
        private bool _isTyping; 
        public TextController textController;
        
        private AudioSource audioSource;
        
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = 0.125f;
            _isTyping = false;
            _textMesh = GetComponentInChildren<TextMeshPro>();
        }

        // return the first character of the text
        public string GetNextText()
        {
            string head = "";
            if (text.text.Length > 0)
            {
                head = text.text.Substring(0, 1);
            }
            return head;
        }
            
        // return leftover text
        public string GetSubText()
        {
            if (text.text.Length > 0)
            {
                return text.text.Substring(1);
            }
            return "";
        }
        public void SetText(string newText)
        {
            text.text = newText;
        }
    
        // set typing status 
        public void SetTyping(bool isTyping)
        {
            this._isTyping = isTyping;
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("bullet") && _isTyping)
            {
                audioSource.Play();
                Debug.Log("Collided with bullet and is typing.");
                Destroy(other.gameObject);
            }
        }
    }
}
