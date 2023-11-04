using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Sawmill : MonoBehaviour
{
    [SerializeField] private Building _building;
    [SerializeField] private ItemRes _targetRes;
    [SerializeField] private CanvasGroup _buildingCanvasGroup; // ������ ������ � �����������
    [SerializeField] private Button _buildingButtonOpen; // ������ ������ � �����������
    [SerializeField] private List<float> _delayProduction; //�������� � ������������ 
    [SerializeField] private int _baseValueProduction; //������� ������� ������������ �� ���� �����������
    [SerializeField] private List<float> _productionMultiply; //����������� ������������ ������������� ������ ������

    private bool _makeResComplete = false; // ������ ������� ��������� ��

    public event UnityAction OnProductionComplete;
    public event UnityAction OnClickBuilding;


    private void OnEnable()
    {
        OnClickBuilding += onOpenClickBuilding;
        _buildingButtonOpen.onClick.AddListener(OnClickBuilding);
    }

    private void OnDisable()
    {
        OnClickBuilding -= onOpenClickBuilding;
        _buildingButtonOpen.onClick.RemoveListener(OnClickBuilding);
    }


    private void Start()
    {
        _building = GetComponent<Building>();
       
    }

    public void onOpenClickBuilding()
    {
        if (_makeResComplete) //���� ������ ����
        {
            UpgradeComplete();
            return;
        }
        // ��� �������� �� ������� ������ �����

        _buildingCanvasGroup.alpha = 1.0f;
        _buildingCanvasGroup.blocksRaycasts = true;
        Time.timeScale = 1;
    }
    public void onCloseClickBuilding(CanvasGroup �anvasGroup)
    {
        �anvasGroup.alpha = 0.0f;
        �anvasGroup.blocksRaycasts = false;
        Time.timeScale = 1;
    }

    private void UpgradeComplete()
    {

    }
}
