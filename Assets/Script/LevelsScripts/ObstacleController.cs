using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Script.LevelsScripts
{
    public class ObstacleController : MonoBehaviour
    {
        public TMP_Text text;
        
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
        string GetSubText()
        {
            if (text.text.Length > 0)
            {
                return text.text.Substring(1);
            }
            return "";
        }
        public string GetText() { return text.text; }

        public void SetText(string newText)
        {
            text.text = newText;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("bullet"))
            {
                Debug.Log("collide with bullet by " + gameObject.name);
            }
        }

        
    }
}
