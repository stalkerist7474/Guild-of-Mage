using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyFightItem : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Button _sellButton;
    [SerializeField] private int _count;
    [SerializeField] private int _price;
    [SerializeField] private int _healValue;
    [SerializeField] private bool _isSold = false;

    public void TrySellItem()
    {
        if (_price <= _player.Money)
        {
            if( _player.CurrentHeath != _player.MaxHeath)
            {
                _player.RemoveMoney(_price);
                _player.AddHitPoints(_healValue);
                RemoveItem(1);


            }
        }
    }

    private void RemoveItem(int count)
    {
        _count -= count;
        if (_count <= 0)
        {
            _count = 0;
            _isSold = true;
            _sellButton.gameObject.SetActive(false);
        }
    }
}
