using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class InventoryFight : MonoBehaviour
{
    public static InventoryFight instance;

    [SerializeField] private Player _player;
    //стартовые вещи
    [SerializeField] List<ItemEquipment> ItemEquipmentStart = new List<ItemEquipment>();
    [SerializeField] List<ItemRes> ItemResStart = new List<ItemRes>();

    //списки с инветарем боя
    List<ItemEquipment> ItemEquipmentFight = new List<ItemEquipment>();
    [SerializeField] List<ItemRes> ItemResFightData = new List<ItemRes>();
    [SerializeField] List<ItemRes> TypeDropItemResFightOnThisLevel = new List<ItemRes>();
    private bool _itemResFound = false;

    public event UnityAction OnBaseResourcesChange;
    private void Awake()
    {
        instance = this;


    }
    private void Start()
    {
        //for (int i = 0; i < ItemEquipmentStart.Count; i++)
        //{
        //    AddItemEquipment(ItemEquipmentStart[i]);
        //}

        //for (int i = 0; i < ItemResStart.Count; i++)
        //{
        //    AddItemRes(ItemResStart[i]);
        //}

        //обнуляем перед стартом показатели накопленных ресурсов за бой
        for (int i = 0; i < ItemResFightData.Count; i++)
        {
            ItemResFightData[i].Count = 0;
        }
    }


    //добавление амуниции в инвентарь боя
    public void AddItemEquipment(ItemEquipment item)
    {
        ItemEquipmentFight.Add(item);
    }

    //Удаление амуниции из инвентаря боя
    public void RemoveItemEquipment(ItemEquipment item)
    {
        ItemEquipmentFight.Remove(item);
    }


    //добавление ресурса в инвентарь боя*************************
    public void AddItemRes(int resScooreRating)
    {
        int count = 0;

        for (int i = 0; i < TypeDropItemResFightOnThisLevel.Count; i++)
        {
            count = 0;

            count = resScooreRating / 2 % TypeDropItemResFightOnThisLevel[i].ScoorePrice;

            TypeDropItemResFightOnThisLevel[i].Count += count;

            
        }
        ////ItemResFight.Add(item);
        //_itemResFound = false;
        ////ItemResBase.Add(item);
        //for (int i = 0; i < ItemResFightData.Count; i++)
        //{
        //    if (item.ID == ItemResFightData[i].ID)
        //    {
        //        ItemResFightData[i].Count += item.Count;
        //        _itemResFound = true;
        //    }
        //}
        //if (_itemResFound == false)
        //{
        //    ItemResFightData.Add(item);
        //}
    }

    //Удаление количество ресурса из инвентаря боя 
    public void RemoveItemRes(ItemRes item, int count)
    {
        //ItemResFight.Remove(item);
        _itemResFound = false;
        //ItemResBase.Remove(item);
        for (int i = 0; i < ItemResFightData.Count; i++)
        {
            if (item.ID == ItemResFightData[i].ID)
            {
                ItemResFightData[i].Count -= count;
                _itemResFound = true;
            }
        }
        if (_itemResFound == false)
        {
            Debug.Log($"NO in base inventory this res{item.Name}");
        }
    }
    //Удаление всего ресурса из инвентаря боя 
    public void RemoveAllValueItemRes(ItemRes item)
    {
        ItemResFightData.Remove(item);
    }

    ///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    
    
    
    //добавление амуниции в инвентарь базы из боя
    public void TrasportItemEquipmentToBase(ItemEquipment item)
    {
        InventoryBase.instance.AddItemEquipment(item);
        RemoveItemEquipment(item);
        

    }

    // перебор всех предметов Equipment и отправка их на базу
    public void TrasportAllItemEquipmentToBase()
    {

        for (int i = 0; i < ItemEquipmentFight.Count; i++)
        {
            TrasportItemEquipmentToBase(ItemEquipmentFight[i]);
            
        }
        
    }

    //добавление ресурса в инвентарь базы из боя
    public void TrasportItemResToBase(int id, int count)
    {
        InventoryBase.instance.AddItemRes(id , count);
        //RemoveAllValueItemRes( id, count);
        


    }

    // перебор всех предметов Res и отправка их на базу
    public void TrasportAllItemResToBase()
    {

        for (int i = 0; i < TypeDropItemResFightOnThisLevel.Count; i++)
        {
            TrasportItemResToBase(TypeDropItemResFightOnThisLevel[i].ID, TypeDropItemResFightOnThisLevel[i].Count);
            
        }
        OnBaseResourcesChange?.Invoke();
    }

    // перевод денег уровня на базу
    public void TrasportBalanceForWin()
    {
        InventoryBase.instance.AddMoneyBase(_player.Money);
        _player.ClearMoney();
        
    }

    public void TrasportBalanceForGameOver()
    {
        InventoryBase.instance.AddMoneyBase(_player.Money / 2);
        _player.ClearMoney();

    }

}
