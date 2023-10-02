using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class Building : MonoBehaviour
{
    [SerializeField] private string _titleBuilding;
    [SerializeField] private List<Stage> _stages = new List<Stage>();

    public event UnityAction OnNextStageComplete;

    private string _aboutBuildingStage;
    private Sprite _currentStageicon;
    private SpriteRenderer _icon;
    private Stage _currentStage;
    

    private void Awake()
    {
        _icon = GetComponent<SpriteRenderer>();
    }

    //метод инициализации текущей стадии здания

    private void Start()
    {
        Debug.Log("+++++++");
        foreach (var stage in _stages)
        {
            if (stage != null)
            {
                if (stage._currentStage)
                {
                    Debug.Log("+++++++********************");
                    _currentStage = stage;
                    _currentStageicon = stage._currentIcon;
                    _aboutBuildingStage = stage._nameStage;

                    _icon.sprite = _currentStageicon;
                }
            }
        }
    }



    //метод улучшения здания

    public void TryImproveBuilding()
    {
        if (CheckResForImprove(InventoryBase.instance.BalanceBase, InventoryBase.instance.ResourcesOnBase))
        {

            Debug.Log("UP1");
            _currentStage._currentStage = false;  //убираем флаг актуальности со старой стадии

            _currentStage = _currentStage._nextStage; //следующая стадия теперь текущая
            _currentStage._statusStage = true; //обновляем что новая стадия последняя
            _currentStage._currentStage = true;

            _currentStageicon = _currentStage._currentIcon; //обновляем картинку
            _icon.sprite = _currentStageicon;
            _aboutBuildingStage = _currentStage._nameStage; //обновляем описание стадии здания

            Debug.Log("UP2");
        }
    }

    //проверка на то что достаточно ли ресурсов

    private bool CheckResForImprove(int money, Dictionary<int, int> resStorage)
    {
        int currentID = 0;
        int currentValue = 0;
        //Stage.instance._needResourcesForNextStage;
        foreach (var item in Stage.instance._needResourcesForNextStage)
        {
            currentID = item.Key;
            currentValue = item.Value;
            if (currentValue != resStorage[currentID])
            {
                Debug.Log($"Не достаточно ресурсов{currentID}");
                return false;
            }
            currentID = 0;
            currentValue = 0;
        }
        return true;
    }

    //проверка актуальной стадии здания

    //метод для получения текущей стадии здания для других классов
}
