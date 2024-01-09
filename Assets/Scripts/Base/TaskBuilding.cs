using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskBuilding : MonoBehaviour
{

    [SerializeField] public Button _button;
    [SerializeField] public TMP_Text _textButton;
    [SerializeField] public Image _panelTask;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] public int _countRes;
    [SerializeField] public float _timeNeedSec;
    [SerializeField] public bool statusInProduction=false;

    [SerializeField] private TMP_Text _countText;
    [SerializeField] private Image _iconRes;

    private void Start()
    {
        _countText.text = _countRes.ToString();
        _iconRes.sprite = _icon;    
    }

    public void StartProduction() //функция для кнопки задания
    {
        statusInProduction = true;
        
    }
}
