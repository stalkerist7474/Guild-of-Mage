using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryFight : MonoBehaviour
{
    [SerializeField] private Player _player;
    //стартовые вещи
    [SerializeField] List<ItemEquipment> ItemEquipmentStart = new List<ItemEquipment>();
    [SerializeField] List<ItemRes> ItemResStart = new List<ItemRes>();

    //списки с инветарем боя
    List<ItemEquipment> ItemEquipmentFight = new List<ItemEquipment>();
    List<ItemRes> ItemResFight = new List<ItemRes>();
    


    private void Start()
    {
        for (int i = 0; i < ItemEquipmentStart.Count; i++)
        {
            AddItemEquipment(ItemEquipmentStart[i]);
        }

        for (int i = 0; i < ItemResStart.Count; i++)
        {
            AddItemRes(ItemResStart[i]);
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


    //добавление ресурса в инвентарь боя
    public void AddItemRes(ItemRes item)
    {
        ItemResFight.Add(item);
    }

    //Удаление ресурса из инвентаря боя
    public void RemoveItemRes(ItemRes item)
    {
        ItemResFight.Remove(item);
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
    public void TrasportItemResToBase(ItemRes item)
    {
        InventoryBase.instance.AddItemRes(item);
        RemoveItemRes(item);
        


    }

    // перебор всех предметов Res и отправка их на базу
    public void TrasportAllItemResToBase()
    {

        for (int i = 0; i < ItemResFight.Count; i++)
        {
            TrasportItemResToBase(ItemResFight[i]);
            
        }

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
