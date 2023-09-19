using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBar : Bar
{
    [SerializeField] protected Player _player;


    private void OnEnable()
    {
        
        _player.ExpChanged += OnValueChanged;
        Slider.value = 1;

    }

    private void OnDisable()
    {
        _player.ExpChanged -= OnValueChanged;
    }
}
