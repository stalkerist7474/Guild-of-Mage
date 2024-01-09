using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Mine : MonoBehaviour
{
    public static Mine Instance;
    [SerializeField] private BuildingStageManagerMine _building;
    [SerializeField] private ItemRes _targetRes;
    [SerializeField] private CanvasGroup _buildingCanvasGroup; // Канвас здания с управлением
    [SerializeField] private Button _buildingButtonOpen; // Канвас здания с управлением
    [SerializeField] private List<float> _productionMultiply; //коэффициент производства соответствует уровню здания
    [SerializeField] private GameObject _timer;
    [SerializeField] private TimerUI _timerUI;
    private bool _makeResComplete = false; // Добыча ресурса завершена ли
    private bool _upgradeComplete = false; // Добыча ресурса завершена ли

    private int _countResInProduction;


    public event UnityAction OnProductionComplete;
    public event UnityAction OnClickBuildingMine;

    private void Awake()
    {

        if (!Instance)      //гарантия что экземпляр будет один
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }


    }

    private void OnEnable()
    {
        OnClickBuildingMine += onOpenClickBuildingMine;

        _buildingButtonOpen.onClick.AddListener(OnClickBuildingMine);
    }

    private void OnDisable()
    {
        OnClickBuildingMine -= onOpenClickBuildingMine;
        _buildingButtonOpen.onClick.RemoveListener(OnClickBuildingMine);
    }


    private void Start()
    {
        _building = GetComponent<BuildingStageManagerMine>();

    }

    public void onOpenClickBuildingMine()
    {
        if (_upgradeComplete) //если готово улучшение
        {
            UpgradeComplete();
            _upgradeComplete = false;
            return;
        }
        if (_makeResComplete) //если готовы ресы
        {
            ProductionComplete();
            _makeResComplete = false;
            StopTimer();
            return;
        }


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
    private void ProductionComplete()
    {
        
        InventoryBase.instance.AddItemRes(_targetRes.ID, _countResInProduction);
        _countResInProduction = 0;
        TaskManagerRock.Instance.TaskComplete();
    }

    public void StartProduction(int count, float timeDelay)
    {
        if (BuildingStageManagerMine.Instance._currentStageId == 0)
        {
            
            return;
        }
        for (int i = 0; i < _productionMultiply.Count; i++)
        {
            
            if (BuildingStageManagerMine.Instance._currentStageId ==  _productionMultiply[i])
            {
                
                _countResInProduction = (int)System.Math.Round(count * _productionMultiply[i]);
            }
        }



        Invoke("Production", timeDelay);//вызываем задежку
        StartTimer(timeDelay);


    }

    private void Production()
    {
        _makeResComplete = true;
        OnProductionComplete?.Invoke();
    }


    private float time;
    private float _timeLeft = 0f;

    private IEnumerator StartTimerCor1()
    {
        while (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            UpdateTimeText();
            yield return null;
        }
    }

    public void StartTimer(float time)
    {
        _timeLeft = time;
        _timer.GetComponent<CanvasGroup>().alpha = 1;
        StartCoroutine(StartTimerCor1());
    }
    public void StopTimer()
    {

        _timer.GetComponent<CanvasGroup>().alpha = 0;
        StopCoroutine(StartTimerCor1());

    }

    private void UpdateTimeText()
    {
        if (_timeLeft < 0)
            _timeLeft = 0;

        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        _timer.GetComponent<TimerUI>()._timeInfo.text = string.Format("{0:00} : {1:00}", minutes, seconds);

    }
}
