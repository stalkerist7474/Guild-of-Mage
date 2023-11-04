using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Sawmill : MonoBehaviour
{
    [SerializeField] private Building _building;
    [SerializeField] private ItemRes _targetRes;
    [SerializeField] private CanvasGroup _buildingCanvasGroup; // Канвас здания с управлением
    [SerializeField] private Button _buildingButtonOpen; // Канвас здания с управлением
    [SerializeField] private List<float> _delayProduction; //задержка в производстве 
    [SerializeField] private int _baseValueProduction; //базовый уровень производства за одно отправление
    [SerializeField] private List<float> _productionMultiply; //коэффициент производства соответствует уровню здания

    private bool _makeResComplete = false; // Добыча ресурса завершена ли

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
        if (_makeResComplete) //если готовы ресы
        {
            UpgradeComplete();
            return;
        }
        // еще проверка на апгрейд здания позже

        _buildingCanvasGroup.alpha = 1.0f;
        _buildingCanvasGroup.blocksRaycasts = true;
        Time.timeScale = 1;
    }
    public void onCloseClickBuilding(CanvasGroup сanvasGroup)
    {
        сanvasGroup.alpha = 0.0f;
        сanvasGroup.blocksRaycasts = false;
        Time.timeScale = 1;
    }

    private void UpgradeComplete()
    {

    }
}
