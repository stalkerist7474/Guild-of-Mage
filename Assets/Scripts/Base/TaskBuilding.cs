using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskBuilding : MonoBehaviour
{
    [SerializeField] private Building _building;
    [SerializeField] private Button _button;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _countRes;
    [SerializeField] private float _timeNeedSec;
    [SerializeField] private bool statusInProduction=false;

    [SerializeField] private TMP_Text _countText;
    [SerializeField] private Image _iconRes;

    private void Start()
    {
        _countText.text = _countRes.ToString();
        _iconRes.sprite = _icon;    
    }
}
