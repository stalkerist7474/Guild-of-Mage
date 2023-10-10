using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void OpenPanel(CanvasGroup panel)
    {
        panel.alpha = 1.0f;
        panel.blocksRaycasts = true;
        Time.timeScale = 0;
    }

    public void ClosePanel(CanvasGroup panel)
    {
        panel.alpha = 0.0f;
        panel.blocksRaycasts = false;
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
