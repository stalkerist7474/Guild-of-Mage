using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public static TimerUI Instance;
    [SerializeField] public CanvasGroup _canvas;
    [SerializeField] public TMP_Text _timeInfo;


    private void Awake()
    {
        Instance = this;
    }

}
