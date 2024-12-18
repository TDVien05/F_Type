using System;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

namespace Script.GamePlay.PrefapController
{
    public class ObstacleController : MonoBehaviour
    {
        private TextMeshPro _textMesh; // text object
        public TMP_Text text; // prefab text
        private bool _isTyping; 
        
        private AudioSource audioSource;
        private Timer timer;
        private Camera cam;
        
        private float knockbackForce = 4f;
        private Rigidbody2D _rb;
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = 0.125f;
            _isTyping = false;
            _textMesh = GetComponentInChildren<TextMeshPro>();
            
            cam = FindObjectOfType<Camera>();
            timer = FindObjectOfType<Timer>();
            _rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (IsObjectAtbottom(_textMesh.gameObject.transform))
            {
                Debug.Log("Obstacle out of bound");
                timer.End();
            }
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
                KnockBack(other); // knock back the obstacle
            }
        }

        private void KnockBack(Collider2D other)
        {
            Vector2 direction = (transform.position-other.transform.position  ).normalized;
            Vector2 knockback = direction * knockbackForce;
            
            _rb.AddForce(knockback, ForceMode2D.Impulse);
        }
        bool IsObjectAtbottom(Transform obj)
        {
            // Convert the object's position to viewport coordinates
            Vector3 viewportPoint = cam.WorldToViewportPoint(obj.position);

            // Check if the object is within the viewport bounds
            bool isAtBot =  viewportPoint.y < 0;
            return isAtBot;
        }
    }
}
