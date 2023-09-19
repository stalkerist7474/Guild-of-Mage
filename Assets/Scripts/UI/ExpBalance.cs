using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExpBalance : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentExp;
    [SerializeField] private TMP_Text _ExpForUp;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _currentExp.text = _player.Exp.ToString();
        _player.ExpChanged += OnExpChagedText;

        _ExpForUp.text = _player._needExpForUp.ToString();
    }

    private void OnDisable()
    {
        _player.ExpChanged -= OnExpChagedText;
    }

    private void OnExpChagedText(int exp, int targetExp)
    {

        _currentExp.text = Convert.ToString(exp);
        _ExpForUp.text = Convert.ToString(targetExp);


    }
}
