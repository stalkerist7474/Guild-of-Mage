using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PerkCountTest : MonoBehaviour
{
    [SerializeField] private TMP_Text _Text;
    [SerializeField] private InventoryBase _base;



    private void Update()
    {
        //Y��� ���������� ��� �������

        _Text.text = _base.CountPerks.ToString();
    }
}
