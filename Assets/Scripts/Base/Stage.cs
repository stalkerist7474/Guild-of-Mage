using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private string _nameBuilding;
    [SerializeField] private string _nameStage;
    [SerializeField] private Stage _nextStage;
    [SerializeField] private bool _statusStage = false; //������ ���������
    [SerializeField] private bool _currentStage = false; // ������� ��������� ���������
    [SerializeField] private float _timeForUpNextStage;
    [SerializeField] private Sprite _currentIcon;

    [SerializeField]private List<string> _keysIdItem = new List<string>();
    [SerializeField]private List<string> _valueCountItem = new List<string>();
    [SerializeField] private Dictionary<int, int> _needResourcesForNextStage = new Dictionary<int, int>();  // Id �������� � ��� ����������
    //��������� ��� ����������� ������� � ����������


}
