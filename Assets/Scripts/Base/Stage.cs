using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Stage : MonoBehaviour, ISerializationCallbackReceiver
{
    [SerializeField] private string _nameBuilding;
    [SerializeField] private string _nameStage;
    [SerializeField] private Stage _nextStage;
    [SerializeField] private bool _statusStage = false; //статус улучшения
    [SerializeField] private bool _currentStage = false; // текущее последнее улучшение
    [SerializeField] private float _timeForUpNextStage;
    [SerializeField] private Sprite _currentIcon;

    [SerializeField]private List<int> _keysIdItem = new List<int>();
    [SerializeField]private List<int> _valueCountItem = new List<int>();
    [SerializeField]private DictionarySO _dictionaryData;
    [SerializeField] private Dictionary<int, int> _needResourcesForNextStage = new Dictionary<int, int>();  // Id предмета и его количество

    public bool modifyValues;

    //Настройка для отображение словаря в инспекторе
    public void OnBeforeSerialize()
    {
        if(modifyValues == false)
        {
            _keysIdItem.Clear();
            _valueCountItem.Clear();

            for (int i = 0; i < Mathf.Min(_dictionaryData.KeysIdItem.Count, _dictionaryData.ValueCountItem.Count); i++)
            {
                _keysIdItem.Add(_dictionaryData.KeysIdItem[i]);
                _valueCountItem.Add(_dictionaryData.ValueCountItem[i]);
            }
        }
    }

    public void OnAfterDeserialize()
    {
        
    }

    public void DeserializeDictionary()
    {
        Debug.Log("DESERIALIZE");
        _needResourcesForNextStage = new Dictionary<int, int>();
        _dictionaryData.KeysIdItem.Clear ();
        _dictionaryData.ValueCountItem.Clear ();

        for (int i = 0; i < Mathf.Min(_keysIdItem.Count, _valueCountItem.Count); i++)
        {
            _dictionaryData.KeysIdItem.Add(_keysIdItem[i]);
            _dictionaryData.ValueCountItem.Add(_valueCountItem[i]);
            _needResourcesForNextStage.Add(_keysIdItem[i], _valueCountItem[i]);
        }
        modifyValues = false;

    }


}
