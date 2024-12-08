using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameLogic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.LevelsScripts.GamePlay
{
    public class PlayerController : MonoBehaviour
    {
        private List<GameObject> listSquare = new List<GameObject>();
        [SerializeField] private SpawnSpace spawnSpace;

        IEnumerator Start()
        {
            yield return new WaitForSeconds(0.1f);
            loadText();
        }

        private void loadText()
        {
            listSquare = spawnSpace.GetListWords();
            Debug.Log(listSquare.Count);
        }
    }
}
