using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Progress;

public class InventoryBase : MonoBehaviour
{
    public static InventoryBase instance;
    //public static InventoryBase singleton { get; set; }
    //стартовые вещи
    [SerializeField] List <ItemEquipment> ItemEquipmentStart = new List <ItemEquipment> ();
    [SerializeField] List <ItemRes> ItemResStart = new List <ItemRes> ();

    //списки с инветарем базы
    public List<ItemEquipment> ItemEquipmentBase = new List<ItemEquipment>();
    public List<ItemRes> ItemResBase = new List<ItemRes>();

    [SerializeField] ItemRes _moneyTemplate;

    //public int BalanceBase;
    private bool _itemResFound = false;
    private int _idMoney = 0;

    //словарь доступных ресурсов
    public Dictionary<int, int> ResourcesOnBase = new Dictionary<int, int>();


    private void OnEnable()
    {

         InventoryFight.instance.OnBaseResourcesChange += OnCountingItemRes;
    }

    private void OnDisable()
    {
        InventoryFight.instance.OnBaseResourcesChange -= OnCountingItemRes; ;
    }



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
    public void AddItemRes(ItemRes item)
    {
        _itemResFound = false;
        //ItemResBase.Add(item);
        for (int i = 0; i < ItemResBase.Count; i++)
        {
            if(item.ID == ItemResBase[i].ID)
            {
                ItemResBase[i].Count += item.Count;
                _itemResFound = true;
            }
        }
        if(_itemResFound == false)
        {
            ItemResBase.Add(item);
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
        //BalanceBase = BalanceBase + money;
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
        //BalanceBase = BalanceBase - money;
        for (int i = 0; i < ItemResBase.Count; i++)
        {
            if (_idMoney == ItemResBase[i].ID)
            {
                ItemResBase[i].Count -= money;
                //_itemResFound = true;
            }
        }
    }

    private void OnCountingItemRes()
    {
        int currentItemID = 0;
        Debug.Log("Trandfer");
        foreach (var item in ItemResBase)
        {
            currentItemID = item.ID;
            if (ResourcesOnBase.ContainsKey(currentItemID))
            {
                ResourcesOnBase[currentItemID] += item.Count;
                currentItemID = 0;
            }
            else
            {
                ResourcesOnBase.Add(currentItemID, item.Count);
                currentItemID = 0;
            }
        }
        //ItemResBase.Clear();
    }
}
