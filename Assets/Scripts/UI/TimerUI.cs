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
    //private float time;
    //private float _timeLeft = 0f;

    //private IEnumerator StartTimer()
    //{
    //    while (_timeLeft > 0)
    //    {
    //        _timeLeft -= Time.deltaTime;
    //        UpdateTimeText();
    //        yield return null;
    //    }
    //}

    //public void Start(float time)
    //{
    //    _timeLeft = time;
    //    StartCoroutine(StartTimer());
    //}

    //private void UpdateTimeText()
    //{
    //    if (_timeLeft < 0)
    //        _timeLeft = 0;

    //    float minutes = Mathf.FloorToInt(_timeLeft / 60);
    //    float seconds = Mathf.FloorToInt(_timeLeft % 60);
    //    _timeInfo.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    //}
}
