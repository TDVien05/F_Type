using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomParagraph : MonoBehaviour
{
    private int topicNumber;
    public string theme;
    
    private void Start()
    {
        // Chon chu de ngau nhien
        topicNumber = Random.Range(1, 5); // Random số từ 1 đến 4
        theme = SelectedTopic();  // Truyền topicNumber vào SelectedTopic
    }

    public string SelectedTopic()
    {
        switch (topicNumber)
        {
            case 1:
                theme = "Noia dunge chsu dey mot cwo khoyang hai tttram cshu";
                break;
            case 2:
                theme = "Noib dungda cshu dhe hai cdo khroang hai trtam chdu";
                break;
            case 3:
                theme = "Noic dundg wechu ude ba cno khorang hai tramm cshu";
                break;
            case 4:
                theme = "Noid daung chtu due bon cto khroang hai trram chcu";
                break;
        }
        Debug.Log("Selected topic sucessful");
        return theme;
    }
}
