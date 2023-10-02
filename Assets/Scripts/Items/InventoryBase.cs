using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class InventoryBase : MonoBehaviour
{
    public static InventoryBase instance;
    //стартовые вещи
    [SerializeField] List <ItemEquipment> ItemEquipmentStart = new List <ItemEquipment> ();
    [SerializeField] List <ItemRes> ItemResStart = new List <ItemRes> ();

    //списки с инветарем базы
    public List<ItemEquipment> ItemEquipmentBase = new List<ItemEquipment>();
    public List<ItemRes> ItemResBase = new List<ItemRes>();
    public int BalanceBase;

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
        instance = this;
        
        
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
        ItemResBase.Add(item);
    }

    //Удаление ресурса из инвентаря базы
    public void RemoveItemRes(ItemRes item)
    {
        ItemResBase.Remove(item);
    }


    //Пополнение баланса базы игрока
    public void AddMoneyBase(int money)
    {
        BalanceBase = BalanceBase + money;
    }
    //Уменьшение баланса базы игрока
    public void RemoveMoneyBase(int money)
    {
        BalanceBase = BalanceBase - money;
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
                ResourcesOnBase[currentItemID] += 1;
                currentItemID = 0;
            }
            else
            {
                ResourcesOnBase.Add(currentItemID, 1);
                currentItemID = 0;
            }
        }
        ItemResBase.Clear();
    }
}
