using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Progress;

public class InventoryBase : MonoBehaviour
{
    public static InventoryBase instance;

    //стартовые вещи
    [SerializeField] List <ItemEquipment> ItemEquipmentStart = new List <ItemEquipment> ();
    [SerializeField] List <ItemRes> ItemResStart = new List <ItemRes> ();

    //списки с инветарем базы
    public List<ItemEquipment> ItemEquipmentBase = new List<ItemEquipment>();
    public List<ItemRes> ItemResBase = new List<ItemRes>();

    [SerializeField] ItemRes _moneyTemplate;


    private bool _itemResFound = false;
    private int _idMoney = 0;

    //словарь доступных ресурсов
    public Dictionary<int, int> ResourcesOnBase = new Dictionary<int, int>();


    private void Awake()
    {

        if (!instance)      //гарантия что экземпляр будет один
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        
        
    }

    private void Start()
    {
    }


    //добавление амуниции в инвентарь базы
    public void AddItemEquipment(ItemEquipment item)
    {
        ItemEquipmentBase.Add (item);
    }

    //Удаление амуниции из инвентаря базы
    public void RemoveItemEquipment(ItemEquipment item)
    {
        ItemEquipmentBase.Remove (item);
    }


    //добавление ресурса в инвентарь базы
    public void AddItemRes(int id, int count)
    {
        

        for (int i = 0; i < ItemResBase.Count; i++)
        {
            if(id == ItemResBase[i].ID)
            {
                ItemResBase[i].Count += count;
            }
            else
            {
                Debug.Log($"not found res ID={id}");
            }
        }
        
    }

    //Удаление ресурса из инвентаря базы
    public void RemoveItemRes(ItemRes item, int count)
    {
        _itemResFound = false;
        //ItemResBase.Remove(item);
        for (int i = 0; i < ItemResBase.Count; i++)
        {
            if (item.ID == ItemResBase[i].ID)
            {
                ItemResBase[i].Count -= count;
                _itemResFound = true;
            }
        }
        if (_itemResFound == false)
        {
            Debug.Log($"NO in base inventory this res{item.Name}");
        }
    }


    //Пополнение баланса базы игрока
    public void AddMoneyBase(int money)
    {
        _itemResFound = false;
        
        for (int i = 0; i < ItemResBase.Count; i++)
        {
            if (_idMoney == ItemResBase[i].ID)
            {
                ItemResBase[i].Count += money;
                _itemResFound = true;
            }
        }
        if (_itemResFound == false)
        {
            _moneyTemplate.Count = money;
            ItemResBase.Add(_moneyTemplate);
        }
    }
    //Уменьшение баланса базы игрока
    public void RemoveMoneyBase(int money)
    {
        
        for (int i = 0; i < ItemResBase.Count; i++)
        {
            if (_idMoney == ItemResBase[i].ID)
            {
                ItemResBase[i].Count -= money;
                
            }
        }
    }

 
}
