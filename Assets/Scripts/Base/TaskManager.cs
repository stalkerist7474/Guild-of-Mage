using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;
    [SerializeField] private List<TaskBuilding> _listTaskWood; //Список заданий на добычу

    public event UnityAction OnStartTask;

    //private void OnEnable()
    //{
    //    OnStartTask += OnClickTask; //Событие на нажатие кнопки старта любого задания

    //    for (int i = 0; i < _listTask.Count; i++) //проставление слушателя этого евента на все кнопки
    //    {
    //        _listTask[i]._button.onClick.AddListener(OnStartTask);
    //    }

    //}

    //private void OnDisable()
    //{
    //    OnStartTask -= OnClickTask;
    //    for (int i = 0; i < _listTask.Count; i++)
    //    {
    //        _listTask[i]._button.onClick.RemoveListener(OnStartTask);
    //    }

    //}
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

    public void OnClickTask()
    {
        Debug.Log("1");
        Debug.Log("1");
        Debug.Log($"1_listTask.Count={_listTaskWood.Count}");
        for (int i = 0; i < _listTaskWood.Count; i++)
        {
            if (_listTaskWood[i].statusInProduction)
            {
                _listTaskWood[i]._textButton.text = "in production";
                _listTaskWood[i]._button.interactable = false;
                _listTaskWood[i]._panelTask.color = Color.blue;
                Sawmill.Instance.StartProduction(_listTaskWood[i]._countRes, _listTaskWood[i]._timeNeedSec);
                Debug.Log("2");
            }
            if (_listTaskWood[i].statusInProduction == false)
            {
                _listTaskWood[i]._button.interactable = false;
                Debug.Log("3");
            }
        }
       // OnStartTask?.Invoke();
    }

    public void TaskComplete()
    {
        for (int i = 0; i < _listTaskWood.Count; i++)
        {

            _listTaskWood[i].statusInProduction = false;
            _listTaskWood[i]._button.interactable = true;
            _listTaskWood[i]._panelTask.color = Color.green;
            _listTaskWood[i]._textButton.text  = "Start";
            
            
        }
    }
}
