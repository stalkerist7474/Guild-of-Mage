using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;
    [SerializeField] private List<TaskBuilding> _listTask; //Список заданий на добычу

    public event UnityAction OnStartTask;

    private void OnEnable()
    {
        OnStartTask += OnClickTask; //Событие на нажатие кнопки старта любого задания

        for (int i = 0; i < _listTask.Count; i++) //проставление слушателя этого евента на все кнопки
        {
            _listTask[i]._button.onClick.AddListener(OnStartTask);
        }
        
    }

    private void OnDisable()
    {
        OnStartTask -= OnClickTask;
        for (int i = 0; i < _listTask.Count; i++)
        {
            _listTask[i]._button.onClick.RemoveListener(OnStartTask);
        }
        
    }


    public void OnClickTask()
    {
        Debug.Log("1");
        for (int i = 0; i < _listTask.Count; i++)
        {
            if (_listTask[i].statusInProduction)
            {
                _listTask[i]._textButton.text = "in production";
                _listTask[i]._button.interactable = false;
                _listTask[i]._panelTask.color = Color.blue;
                OnStartTask?.Invoke();
                Debug.Log("2");
            }
            else
            {
                _listTask[i]._button.interactable = false;
                Debug.Log("3");
            }
        }
    }

    public void TaskComplete()
    {
        for (int i = 0; i < _listTask.Count; i++)
        {

            _listTask[i].statusInProduction = false;
            _listTask[i]._button.interactable = true;
            _listTask[i]._button.name = "Start";
            
            
        }
    }
}
