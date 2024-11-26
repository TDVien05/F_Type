using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickScript : MonoBehaviour
{
    public AudioSource audioSource;
    public void playAudioClick()
    {
        audioSource.Play();
    }
}
