using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
       // _stages[0] = GetComponent<Stage>();
    }

    //����� ������������� ������� ������ ������

    private void Start()
    {
       // Debug.Log("+++++++");
        foreach (var stage in _stages)
        {
            if (stage != null)
            {
                if (stage._currentStage)
                {
                    //Debug.Log("+++++++********************");
                    _currentStage = stage;
                    _currentStageicon = stage._currentIcon;
                    _aboutBuildingStage = stage._nameStage;

                    _icon.sprite = _currentStageicon;
                }
            }
        }
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
            _currentStage._currentStage = false;  //������� ���� ������������ �� ������ ������

            _currentStage = _currentStage._nextStage; //��������� ������ ������ �������
            _currentStage._statusStage = true; //��������� ��� ����� ������ ���������
            _currentStage._currentStage = true;

            _currentStageicon = _currentStage._currentIcon; //��������� ��������
            _icon.sprite = _currentStageicon;
            _aboutBuildingStage = _currentStage._nameStage; //��������� �������� ������ ������

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


        foreach ( var res in needResForNext)
        {
            currentID = res.Key; currentID = res.Value;

            for ( var i = 0; i < resStorage.Count; i++)
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
            currentID = res.Key; currentID = res.Value;

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

    //�������� ���������� ������ ������

    //����� ��� ��������� ������� ������ ������ ��� ������ �������
}
