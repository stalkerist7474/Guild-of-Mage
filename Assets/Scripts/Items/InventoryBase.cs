using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    public void AddMoneyBase(int money)
    {
        BalanceBase = BalanceBase + money;
    }

    public void RemoveMoneyBase(int money)
    {
        BalanceBase = BalanceBase - money;
    }
}
