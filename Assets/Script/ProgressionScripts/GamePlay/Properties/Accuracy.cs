using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accuracy : MonoBehaviour
{
    private float _correctChar;
    private float _incorrectChar;
    // Start is called before the first frame update
    void Start()
    {
        _correctChar = 0;
        _incorrectChar = 0;
    }

    public void SetCorrectChar(float point)
    {
        _correctChar += point;
    }

    public void SetIncorrectChar(float point)
    {
        _incorrectChar += point;
    }

    public float CalculateAccuracy()
    {
        float total = _correctChar + _incorrectChar;
        float acc = (_correctChar - _incorrectChar)/ total * 100;
        return acc; // Round to 2 decimal places
    }
}
