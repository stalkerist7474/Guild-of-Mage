using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResBalanceTest : MonoBehaviour
{
    [SerializeField] private TMP_Text _Text;

    [SerializeField] private ItemRes _res;



    private void Update()
    {
        //Y��� ���������� ��� �������

        _Text.text = _res.Count.ToString();
    }
}
