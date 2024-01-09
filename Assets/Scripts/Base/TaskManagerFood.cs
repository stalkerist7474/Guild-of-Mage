using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskManagerFood : MonoBehaviour
{
    public static TaskManagerFood Instance;
    [SerializeField] private List<TaskBuilding> _listTaskFood; //Список заданий на добычу

    public event UnityAction OnStartTask;

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
        Debug.Log($"1_listTask.Count={_listTaskFood.Count}");
        for (int i = 0; i < _listTaskFood.Count; i++)
        {
            if (_listTaskFood[i].statusInProduction)
            {
                _listTaskFood[i]._textButton.text = "in production";
                _listTaskFood[i]._button.interactable = false;
                _listTaskFood[i]._panelTask.color = Color.blue;
                Farm.Instance.StartProduction(_listTaskFood[i]._countRes, _listTaskFood[i]._timeNeedSec);
                Debug.Log("2");
            }
            if (_listTaskFood[i].statusInProduction == false)
            {
                _listTaskFood[i]._button.interactable = false;
                Debug.Log("3");
            }
        }

    }

    public void TaskComplete()
    {
        for (int i = 0; i < _listTaskFood.Count; i++)
        {

            _listTaskFood[i].statusInProduction = false;
            _listTaskFood[i]._button.interactable = true;
            _listTaskFood[i]._panelTask.color = Color.green;
            _listTaskFood[i]._textButton.text = "Start";


        }
    }
}
