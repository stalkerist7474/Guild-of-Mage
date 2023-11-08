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
    [SerializeField] private CanvasGroup _buildingCanvasGroup; // ������ ������ � �����������
    [SerializeField] private Button _buildingButtonOpen; // ������ ������ � �����������
    //[SerializeField] private List<float> _delayProduction; //�������� � ������������ 
    //[SerializeField] private int _baseValueProduction; //������� ������� ������������ �� ���� �����������
    [SerializeField] private List<float> _productionMultiply; //����������� ������������ ������������� ������ ������
                                                              // [SerializeField] private List<TaskBuilding> _listTask; //������ ������� �� ������
    [SerializeField] private GameObject _timer;
    [SerializeField] private TimerUI _timerUI;
    private bool _makeResComplete = false; // ������ ������� ��������� ��
    private bool _upgradeComplete = false; // ������ ������� ��������� ��

    private int _countResInProduction;


    public event UnityAction OnProductionComplete;
    public event UnityAction OnClickBuilding;

    private void Awake()
    {

        if (!Instance)      //�������� ��� ��������� ����� ����
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
        _building = GetComponent<BuildingStageManagerSawmill>();
       
    }
    
    public void onOpenClickBuilding()
    {
        if (_upgradeComplete) //���� ������ ���������
        {
            UpgradeComplete();
            _upgradeComplete = false;
            return;
        }
        if (_makeResComplete) //���� ������ ����
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
    public void onCloseClickBuilding(CanvasGroup �anvasGroup)
    {
        �anvasGroup.alpha = 0.0f;
        �anvasGroup.blocksRaycasts = false;
        Time.timeScale = 1;
    }

    private void UpgradeComplete()
    {

    }
    private void ProductionComplete()
    {
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
            if( BuildingStageManagerSawmill.Instance._currentStageId == _productionMultiply[BuildingStageManagerSawmill.Instance._currentStageId])
            {
                _countResInProduction = (int)System.Math.Round(count * _productionMultiply[i]);
            }
        }
        
        
        
        Invoke("Production", timeDelay);//�������� �������
        Start(timeDelay);

        
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

    private IEnumerator StartTimer()
    {
        while (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            UpdateTimeText();
            yield return null;
        }
    }

    public void Start(float time)
    {
        _timeLeft = time;
        _timer.GetComponent<CanvasGroup>().alpha = 1;
        StartCoroutine(StartTimer());
    }
    public void StopTimer()
    {
        
        _timer.GetComponent<CanvasGroup>().alpha = 0;
        StopCoroutine(StartTimer());
        
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
