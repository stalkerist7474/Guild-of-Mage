using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sawmill : MonoBehaviour
{
    [SerializeField] private Building _building;
    [SerializeField] private ItemRes _targetRes;
    [SerializeField] private List<float> _delayProduction; //задержка в производстве 
    [SerializeField] private int _baseValueProduction; //базовый уровень производства за одно отправление
    [SerializeField] private List<float> _productionMultiply; //коэффициент производства соответствует уровню здания

    public event UnityAction OnProductionComplete;


    private void Start()
    {
        _building = GetComponent<Building>();
    }

}
