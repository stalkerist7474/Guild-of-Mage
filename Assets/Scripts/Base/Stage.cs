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
        //Load();
        //Debug.Log("11");
        //Debug.Log(_needResourcesForNextStage.Count);
        //foreach (var item in _needResourcesForNextStage)
        //{
        //    Debug.Log("11");
        //    Debug.Log($"item={item.Value}");
        //}
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
                //Debug.Log($"OnBeforeSerialize={_dictionaryData.KeysIdItem[i]}/{_dictionaryData.ValueCountItem[i]}");

                _keysIdItem.Add(_dictionaryData.KeysIdItem[i]);
                _valueCountItem.Add(_dictionaryData.ValueCountItem[i]);

                _needResourcesForNextStage.Add(_keysIdItem[i], _valueCountItem[i]);
                Debug.Log($"Add item#{i}={_keysIdItem[i]}/{_keysIdItem[i]}");
            }
        }
    }

    //public void OnAfterDeserialize()
    //{
        
    //}

    //public void DeserializeDictionary()
    //{
    //    Debug.Log("DESERIALIZE");
    //   // _needResourcesForNextStage = new Dictionary<int, int>();
    //    _dictionaryData.KeysIdItem.Clear ();
    //    _dictionaryData.ValueCountItem.Clear ();

    //    for (int i = 0; i < Mathf.Min(_keysIdItem.Count, _valueCountItem.Count); i++)
    //    {
    //        Debug.Log($"DeserializeDictionary={_dictionaryData.KeysIdItem[i]}/{_dictionaryData.ValueCountItem[i]}");
    //        _dictionaryData.KeysIdItem.Add(_keysIdItem[i]);
    //        _dictionaryData.ValueCountItem.Add(_valueCountItem[i]);
    //        _needResourcesForNextStage.Add(_keysIdItem[i], _valueCountItem[i]);
    //    }
    //    modifyValues = false;

    //}









}
