using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingStageManagerMine : MonoBehaviour
{
    public static BuildingStageManagerMine Instance;
    [SerializeField] private string _titleBuilding;
    [SerializeField] private List<Stage> _stages = new List<Stage>();

    public event UnityAction OnNextStageComplete;

    private string _aboutBuildingStage;
    private Sprite _currentStageicon;
    private SpriteRenderer _icon;
    private Stage _currentStage;
    public int _currentStageId;


    private void Awake()
    {
        _icon = GetComponent<SpriteRenderer>();
        Instance = this;

    }

    //����� ������������� ������� ������ ������

    private void Start()
    {
        foreach (var item in _stages)
        {
            item.Load();
        }

        foreach (var stage in _stages)
        {
            if (stage != null)
            {
                if (stage._currentStageBool)
                {

                    _currentStage = stage;
                    _currentStageicon = stage._currentIcon;
                    _aboutBuildingStage = stage._nameStage;

                    GetIndexStage();
                    Debug.Log($"_currentStageId={_currentStageId}");

                    _icon.sprite = _currentStageicon;
                }
            }
        }
        LoadStage();
    }

    private void GetIndexStage()
    {
        for (int i = 0; i < _stages.Count; i++)
        {
            if (_stages[i]._currentStageBool)
            {
                _currentStageId = i;
            }
        }
    }
    //����� �������� ������ �� ����������

    private void LoadStage()
    {
        Debug.Log("Load1");
        _currentStageId = PlayerPrefs.GetInt("Mine_stageID");

        _currentStage = _stages[_currentStageId];

        _currentStageicon = _currentStage._currentIcon; //��������� ��������
        _icon.sprite = _currentStageicon;
        _aboutBuildingStage = _currentStage._nameStage; //��������� �������� ������ ������
        Debug.Log("Load2");
        GetIndexStage();
    }
    //����� ���������� ������ �� ����������

    public void SaveStage()
    {
        Debug.Log("save1");
        PlayerPrefs.SetInt("Mine_stageID", _currentStageId);
        Debug.Log("saveOK");
    }

    //����� ��������� ������

    public void TryImproveBuilding()
    {

        Debug.Log("UP0");
        Debug.Log($"_currentStage._needResourcesForNextStage={_currentStage._needResourcesForNextStage.Count}_currentStage._needResourcesForNextStage={_currentStage._needResourcesForNextStage.Keys}");
        Debug.Log($"_stages={_stages.Count}");


        if (CheckResForImprove(InventoryBase.instance.ItemResBase, _currentStage._needResourcesForNextStage))
        {

            Debug.Log("UP1");
            PayResInBuilding(_currentStage._needResourcesForNextStage); //������ �� ���������
            _currentStage._currentStageBool = false;  //������� ���� ������������ �� ������ ������

            _currentStage = _currentStage._nextStage; //��������� ������ ������ �������
            _currentStage._statusStageBool = true; //��������� ��� ����� ������ ���������
            _currentStage._currentStageBool = true;

            _currentStageicon = _currentStage._currentIcon; //��������� ��������
            _icon.sprite = _currentStageicon;
            _aboutBuildingStage = _currentStage._nameStage; //��������� �������� ������ ������

            GetIndexStage();
            SaveStage();

            Debug.Log("UP2");
        }
    }

    //�������� �� �� ��� ���������� �� ��������

    private bool CheckResForImprove(List<ItemRes> resStorage, Dictionary<int, int> needResForNext)
    {
        int currentID = 0;
        int currentValue = 0;
        bool status = false;
        Debug.Log("Check for up");


        foreach (var res in needResForNext)
        {
            currentID = res.Key; currentValue = res.Value;
            Debug.Log($"currentID={currentID}////currentValue={currentValue}");

            for (var i = 0; i < resStorage.Count; i++)
            {
                if (resStorage[i].ID == currentID)
                {
                    Debug.Log("Check ID OK!");

                    if (resStorage[i].Count >= currentValue)
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                        Debug.Log("Check for up NO RES");
                    }



                }




            }
        }

        if (status)
        {
            return true;
        }
        else { return false; }

    }

    //������ ������� � ������

    private void PayResInBuilding(Dictionary<int, int> payingListRes)
    {

        int currentID = 0;
        int currentValue = 0;

        var storage = InventoryBase.instance.ItemResBase;

        foreach (var res in payingListRes)
        {
            currentID = res.Key; currentValue = res.Value;

            for (var i = 0; i < storage.Count; i++)
            {
                if (storage[i].ID == currentID)
                {

                    if (storage[i].Count >= currentValue)
                    {
                        storage[i].Count -= currentValue;
                        Debug.Log($"Pay {storage[i].name} = {currentValue} count ");
                    }
                    else
                    {
                        Debug.Log("eerr pay");
                    }



                }




            }
        }

    }


}
