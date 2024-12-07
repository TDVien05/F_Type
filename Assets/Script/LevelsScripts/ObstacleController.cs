using UnityEngine;
using UnityEngine.UI;


namespace Script.LevelsScripts
{
    public class ObstacleController : MonoBehaviour
    {
        public Text text;
        public string GetNextText()
        {
            string head = "";
            if (text.text.Length > 0)
            {
                head = text.text.Substring(0, 1);
            }
            return head;
        }

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
                Destroy(gameObject);
                if (GetSubText() != "")
                {
                    text.text = GetSubText();
                }
                else
                {
                    ChangePosition();
                }
            }
            else if (other.CompareTag("Player"))
            {
                Debug.Log("Va cham voi may bay");
            }
        }

        void ChangePosition()
        {
            Debug.Log("changed position");
        }
    }
}
