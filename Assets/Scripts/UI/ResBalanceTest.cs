using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResBalanceTest : MonoBehaviour
{
    [SerializeField] private TMP_Text _Text;
    //[SerializeField] private Player _player;
    [SerializeField] private ItemRes _res;

    //private void OnEnable()
    //{
    //    _money.text = _player.Money.ToString();
    //    _player.MoneyChanged += OnMoneyChaged;
    //}

    //private void OnDisable()
    //{
    //    _player.MoneyChanged -= OnMoneyChaged;
    //}

    //private void OnMoneyChaged(int money)
    //{
       
    //    _money.text = Convert.ToString(money);
        
        
    //}

    private void Update()
    {
        _Text.text = _res.Count.ToString();
    }
}
