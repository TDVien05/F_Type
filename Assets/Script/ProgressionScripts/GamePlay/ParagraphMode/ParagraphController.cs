using TMPro;
using UnityEngine;

namespace Script.ProgressionScripts.GamePlay.ParagraphMode
{
    public class ParagraphController : MonoBehaviour
    {
        public float fallSpeed = 0.1f;
        private TMP_Text textMesh;
        private Vector3 startPosition;

        void Start()
        {
            textMesh = GetComponent<TMP_Text>();
            startPosition = transform.position;
        }

        void Update()
        {
            MovingTextDown();
        }


        void MovingTextDown()
        {
            if(transform.position.y > 5)
                transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
    
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("bullet"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
