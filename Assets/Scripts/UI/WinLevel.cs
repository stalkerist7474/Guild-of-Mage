using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class WinLevel : MonoBehaviour
{
    
    [SerializeField] private CanvasGroup _winCanvas;

    private void OnEnable()
    {
        Spawner.OnLevelWin += OnWin;
    }

    private void OnDisable()
    {       
        Spawner.OnLevelWin -= OnWin;
    }

    private void Awake()
    {
        _winCanvas = GetComponent<CanvasGroup>();
        _winCanvas.alpha = 0.0f;
        _winCanvas.blocksRaycasts = false;
    }


    //Когда победил
    public void OnWin()
    {
        _winCanvas.alpha = 1.0f;
        _winCanvas.blocksRaycasts = true;
    }
}
