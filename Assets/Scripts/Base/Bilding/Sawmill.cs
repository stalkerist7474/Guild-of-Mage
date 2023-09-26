using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sawmill : MonoBehaviour
{
    [SerializeField] private Building _building;
    [SerializeField] private ItemRes _targetRes;
    [SerializeField] private List<float> _delayProduction; //�������� � ������������ 
    [SerializeField] private int _baseValueProduction; //������� ������� ������������ �� ���� �����������
    [SerializeField] private List<float> _productionMultiply; //����������� ������������ ������������� ������ ������

    public event UnityAction OnProductionComplete;


    private void Start()
    {
        _building = GetComponent<Building>();
    }

}
