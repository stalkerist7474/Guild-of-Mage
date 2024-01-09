using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskManagerRock : MonoBehaviour
{
    public static TaskManagerRock Instance;
    [SerializeField] private List<TaskBuilding> _listTaskRock; //Список заданий на добычу

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
        Debug.Log($"1_listTask.Count={_listTaskRock.Count}");
        for (int i = 0; i < _listTaskRock.Count; i++)
        {
            if (_listTaskRock[i].statusInProduction)
            {
                _listTaskRock[i]._textButton.text = "in production";
                _listTaskRock[i]._button.interactable = false;
                _listTaskRock[i]._panelTask.color = Color.blue;
                Mine.Instance.StartProduction(_listTaskRock[i]._countRes, _listTaskRock[i]._timeNeedSec);
                Debug.Log("2");
            }
            if (_listTaskRock[i].statusInProduction == false)
            {
                _listTaskRock[i]._button.interactable = false;
                Debug.Log("3");
            }
        }

    }

    public void TaskComplete()
    {
        for (int i = 0; i < _listTaskRock.Count; i++)
        {

            _listTaskRock[i].statusInProduction = false;
            _listTaskRock[i]._button.interactable = true;
            _listTaskRock[i]._panelTask.color = Color.green;
            _listTaskRock[i]._textButton.text = "Start";


        }
    }
}
