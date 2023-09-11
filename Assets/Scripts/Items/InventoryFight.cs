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
        Debug.Log("TrasportItemEquipmentToBase");

    }

    // перебор всех предметов Equipment и отправка их на базу
    public void TrasportAllItemEquipmentToBase()
    {

        for (int i = 0; i < ItemEquipmentFight.Count; i++)
        {
            TrasportItemEquipmentToBase(ItemEquipmentFight[i]);
            Debug.Log("TrasportAllItemEquipmentToBase");
        }
        
    }

    //добавление ресурса в инвентарь базы из боя
    public void TrasportItemResToBase(ItemRes item)
    {
        InventoryBase.instance.AddItemRes(item);
        Debug.Log("TrasportItemResToBase");


    }

    // перебор всех предметов Res и отправка их на базу
    public void TrasportAllItemResToBase()
    {

        for (int i = 0; i < ItemResFight.Count; i++)
        {
            TrasportItemResToBase(ItemResFight[i]);
            Debug.Log("TrasportAllItemResToBase");
        }

    }

    // перевод денег уровня на базу
    public void TrasportBalance(int money)
    {

        

    }

}
