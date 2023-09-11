using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryFight : MonoBehaviour
{
    [SerializeField] private Player _player;
    //��������� ����
    [SerializeField] List<ItemEquipment> ItemEquipmentStart = new List<ItemEquipment>();
    [SerializeField] List<ItemRes> ItemResStart = new List<ItemRes>();

    //������ � ��������� ���
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


    //���������� ������� � ��������� ���
    public void AddItemRes(ItemRes item)
    {
        ItemResFight.Add(item);
    }

    //�������� ������� �� ��������� ���
    public void RemoveItemRes(ItemRes item)
    {
        ItemResFight.Remove(item);
    }

    ///++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    
    
    
    //���������� �������� � ��������� ���� �� ���
    public void TrasportItemEquipmentToBase(ItemEquipment item)
    {
        InventoryBase.instance.AddItemEquipment(item);
        Debug.Log("TrasportItemEquipmentToBase");

    }

    // ������� ���� ��������� Equipment � �������� �� �� ����
    public void TrasportAllItemEquipmentToBase()
    {

        for (int i = 0; i < ItemEquipmentFight.Count; i++)
        {
            TrasportItemEquipmentToBase(ItemEquipmentFight[i]);
            Debug.Log("TrasportAllItemEquipmentToBase");
        }
        
    }

    //���������� ������� � ��������� ���� �� ���
    public void TrasportItemResToBase(ItemRes item)
    {
        InventoryBase.instance.AddItemRes(item);
        Debug.Log("TrasportItemResToBase");


    }

    // ������� ���� ��������� Res � �������� �� �� ����
    public void TrasportAllItemResToBase()
    {

        for (int i = 0; i < ItemResFight.Count; i++)
        {
            TrasportItemResToBase(ItemResFight[i]);
            Debug.Log("TrasportAllItemResToBase");
        }

    }

    // ������� ����� ������ �� ����
    public void TrasportBalance(int money)
    {

        

    }

}
