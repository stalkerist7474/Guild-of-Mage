using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using static UnityEditor.Progress;

public class Stage : MonoBehaviour
{
    public static Stage instance;
    [SerializeField] public string _nameBuilding;
    [SerializeField] public string _nameStage;
    [SerializeField] public Stage _nextStage;
    [SerializeField] public bool _statusStageBool = false; //статус улучшения
    [SerializeField] public bool _currentStageBool = false; // текущее последнее улучшение
    [SerializeField] public float _timeForUpNextStage;
    [SerializeField] public Sprite _currentIcon;

    [SerializeField]private List<int> _keysIdItem = new List<int>();
    [SerializeField]private List<int> _valueCountItem = new List<int>();
    [SerializeField]private DictionarySO _dictionaryData;
    [SerializeField] public Dictionary<int, int> _needResourcesForNextStage = new Dictionary<int, int>();  // Id предмета и его количество


    public bool OnThisStage = true;



    private void Start()
    {

    }

    //Настройка для отображение словаря в инспекторе и подгрузка данных из Скриптапл обж
    public void Load()
    {
        if(OnThisStage == true)
        {
            _keysIdItem.Clear();
            _valueCountItem.Clear();

            for (int i = 0; i < _dictionaryData.KeysIdItem.Count; i++)
            {


                _keysIdItem.Add(_dictionaryData.KeysIdItem[i]);
                _valueCountItem.Add(_dictionaryData.ValueCountItem[i]);

                _needResourcesForNextStage.Add(_keysIdItem[i], _valueCountItem[i]);
                Debug.Log($"Add item#{i}={_keysIdItem[i]}/{_keysIdItem[i]}");
            }
        }
    }





}
