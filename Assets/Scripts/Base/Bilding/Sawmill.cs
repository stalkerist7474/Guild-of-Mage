using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Sawmill : MonoBehaviour
{
    public static Sawmill Instance;
    [SerializeField] private BuildingStageManagerSawmill _building;
    [SerializeField] private ItemRes _targetRes;
    [SerializeField] private CanvasGroup _buildingCanvasGroup; // Канвас здания с управлением
    [SerializeField] private Button _buildingButtonOpen; // Канвас здания с управлением
    //[SerializeField] private List<float> _delayProduction; //задержка в производстве 
    //[SerializeField] private int _baseValueProduction; //базовый уровень производства за одно отправление
    [SerializeField] private List<float> _productionMultiply; //коэффициент производства соответствует уровню здания
                                                              // [SerializeField] private List<TaskBuilding> _listTask; //Список заданий на добычу
    [SerializeField] private GameObject _timer;
    [SerializeField] private TimerUI _timerUI;
    private bool _makeResComplete = false; // Добыча ресурса завершена ли
    private bool _upgradeComplete = false; // Добыча ресурса завершена ли

    private int _countResInProduction;


    public event UnityAction OnProductionComplete;
    public event UnityAction OnClickBuildingSawmill;

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
        OnClickBuildingSawmill += onOpenClickBuildingSawmill;
        
        _buildingButtonOpen.onClick.AddListener(OnClickBuildingSawmill);
    }

    private void OnDisable()
    {
        OnClickBuildingSawmill -= onOpenClickBuildingSawmill;
        _buildingButtonOpen.onClick.RemoveListener(OnClickBuildingSawmill);
    }


    private void Start()
    {
        _building = GetComponent<BuildingStageManagerSawmill>();
       
    }
    
    public void onOpenClickBuildingSawmill()
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

        Debug.Log($"_countResInProduction ADD = {_countResInProduction}");
        InventoryBase.instance.AddItemRes(_targetRes.ID, _countResInProduction);
        _countResInProduction = 0;
        TaskManagerWood.Instance.TaskComplete();
    }

    public void StartProduction(int count, float timeDelay)
    {
        if (BuildingStageManagerSawmill.Instance._currentStageId == 0) 
        {
            Debug.Log("Stage 0 not production res");
            return;
        }
        for (int i = 0; i < _productionMultiply.Count; i++)
        {
            if( BuildingStageManagerSawmill.Instance._currentStageId == _productionMultiply[i])
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

    //private void StartTimer(float time)
    //{
    //    var timer = new TimerUI();
    //    timer = _timer.GetComponent<TimerUI>();
    //    _timer.GetComponent<CanvasGroup>().alpha = 1;
    //    _timer.GetComponent<TimerUI>().Start(time);
    //}

    private float time;
    private float _timeLeft = 0f;

    private IEnumerator StartTimerCor()
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
        StartCoroutine(StartTimerCor());
    }
    public void StopTimer()
    {
        
        _timer.GetComponent<CanvasGroup>().alpha = 0;
        StopCoroutine(StartTimerCor());
        
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
