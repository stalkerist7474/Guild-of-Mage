using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Dictionary Storage", menuName = "Data Objects/Dictionary Storage object")]

public class DictionarySO : ScriptableObject
{
    [SerializeField] private List<int> _keysIdItem = new List<int>();
    [SerializeField] private List<int> _valueCountItem = new List<int>();

    public List<int> KeysIdItem { get => _keysIdItem; set => _keysIdItem = value; }
    public List<int> ValueCountItem { get => _valueCountItem; set => _valueCountItem = value; }
}
