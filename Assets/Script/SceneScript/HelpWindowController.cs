using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpWindowController : MonoBehaviour
{
    public GameObject helpWindow;
    public GameObject textHelp;
    public GameObject closeButtom;
    public GameObject playButtom;
    public GameObject levelButtom;
    public GameObject helpButtom;
    public GameObject exitButtom;
    public GameObject title;
    public GameObject helpBg;

    public void ShowHelp()
    {
        if (helpWindow != null)
        {
            helpWindow.SetActive(true);
            textHelp.SetActive(true);
            closeButtom.SetActive(true);
            helpBg.SetActive(true);
            playButtom.SetActive(false);
            levelButtom.SetActive(false);
            helpButtom.SetActive(false);
            title.SetActive(false);
        }
    }

    public void CloseHelp()
    {
        if (helpWindow != null)
        {
            helpWindow.SetActive(false);
            textHelp.SetActive(false);
            closeButtom.SetActive(false);
            helpBg.SetActive(false);
            playButtom.SetActive(true);
            levelButtom.SetActive(true);
            helpButtom.SetActive(true);
            title.SetActive(true);
        }
    }
}
