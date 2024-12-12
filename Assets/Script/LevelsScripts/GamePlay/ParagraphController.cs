using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace Script.LevelsScripts.GamePlay
{
    public class ParagraphController : MonoBehaviour
    {
        private RandomParagraph randomParagraph;

        // Quan ly WORD duoc sinh ra
        private TextMeshPro textMesh; // Text cua prefabs
        public float speed = 0.5f; // Toc do roi
        private string[] words;

        private void Start()
        {
        }        

       private void Update()
       {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
       }
    }
}