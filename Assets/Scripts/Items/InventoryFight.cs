using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class InventoryFight : MonoBehaviour
{
    public static InventoryFight instance;

    [SerializeField] private Player _player;
    //��������� ����
    [SerializeField] List<ItemEquipment> ItemEquipmentStart = new List<ItemEquipment>();
    [SerializeField] List<ItemRes> ItemResStart = new List<ItemRes>();

    //������ � ��������� ���
    List<ItemEquipment> ItemEquipmentFight = new List<ItemEquipment>();
    List<ItemRes> ItemResFight = new List<ItemRes>();
    private bool _itemResFound = false;

    public event UnityAction OnBaseResourcesChange;
    private void Awake()
    {
        instance = this;


    }
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


    //���������� �������� � ��������� ���
    public void AddItemEquipment(ItemEquipment item)
    {
        ItemEquipmentFight.Add(item);
    }

    //�������� �������� �� ��������� ���
    public void RemoveItemEquipment(ItemEquipment item)
    {
        ItemEquipmentFight.Remove(item);
    }


    //���������� ������� � ��������� ���*************************
    public void AddItemRes(ItemRes item)
    {
        //ItemResFight.Add(item);
        _itemResFound = false;
        //ItemResBase.Add(item);
        for (int i = 0; i < ItemResFight.Count; i++)
        {
            if (item.ID == ItemResFight[i].ID)
            {
                ItemResFight[i].Count += item.Count;
                _itemResFound = true;
            }
        }
        if (_itemResFound == false)
        {
            ItemResFight.Add(item);
        }
    }

    //�������� ���������� ������� �� ��������� ��� 
    public void RemoveItemRes(ItemRes item, int count)
    {
        //ItemResFight.Remove(item);
        _itemResFound = false;
        //ItemResBase.Remove(item);
        for (int i = 0; i < ItemResFight.Count; i++)
        {
            if (item.ID == ItemResFight[i].ID)
            {
                ItemResFight[i].Count -= count;
                _itemResFound = true;
            }
        }
        if (_itemResFound == false)
        {
            Debug.Log($"NO in base inventory this res{item.Name}");
        }
    }
    //�������� ����� ������� �� ��������� ��� 
    public void RemoveAllValueItemRes(ItemRes item)
    {
        ItemResFight.Remove(item);
    }

    ///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    
    
    
    //���������� �������� � ��������� ���� �� ���
    public void TrasportItemEquipmentToBase(ItemEquipment item)
    {
        InventoryBase.instance.AddItemEquipment(item);
        RemoveItemEquipment(item);
        

    }

    // ������� ���� ��������� Equipment � �������� �� �� ����
    public void TrasportAllItemEquipmentToBase()
    {

        for (int i = 0; i < ItemEquipmentFight.Count; i++)
        {
            TrasportItemEquipmentToBase(ItemEquipmentFight[i]);
            
        }
        
    }

    //���������� ������� � ��������� ���� �� ���
    public void TrasportItemResToBase(ItemRes item)
    {
        InventoryBase.instance.AddItemRes(item);
        RemoveAllValueItemRes(item);
        


    }

    // ������� ���� ��������� Res � �������� �� �� ����
    public void TrasportAllItemResToBase()
    {

        for (int i = 0; i < ItemResFight.Count; i++)
        {
            TrasportItemResToBase(ItemResFight[i]);
            
        }
        OnBaseResourcesChange?.Invoke();
    }

    // ������� ����� ������ �� ����
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
