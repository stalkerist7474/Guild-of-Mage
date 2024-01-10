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
    [SerializeField] List<ItemRes> ItemResFightData = new List<ItemRes>();
    [SerializeField] List<ItemRes> TypeDropItemResFightOnThisLevel = new List<ItemRes>();
    [SerializeField, Range(1f, 10f)] private int _maxRandomMultiply;
    private bool _itemResFound = false;

    public event UnityAction OnBaseResourcesChange;
    private void Awake()
    {
        instance = this;


    }
    private void Start()
    {


        //�������� ����� ������� ���������� ����������� �������� �� ���
        for (int i = 0; i < ItemResFightData.Count; i++)
        {
            ItemResFightData[i].Count = 0;
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
    public void AddItemRes(int resScooreRating)
    {
        int coefficientRand = Random.RandomRange(1, _maxRandomMultiply);

        int lostValueScoore = resScooreRating;
        Debug.Log($"resScooreRating={resScooreRating}");

        for (int i = 0; i < TypeDropItemResFightOnThisLevel.Count; i++)
        {
            int countAddRes = 0;
            if (lostValueScoore > 0)        // �������� ��� ����� ���� ��� �������������
            {

                if( i == TypeDropItemResFightOnThisLevel.Count)                         //���� ��� ��������� ����������� ������
                {
                    countAddRes = lostValueScoore % TypeDropItemResFightOnThisLevel[i].ScoorePrice;
                    TypeDropItemResFightOnThisLevel[i].Count += countAddRes;
                    countAddRes = 0;
                }

                countAddRes = (lostValueScoore / coefficientRand) % TypeDropItemResFightOnThisLevel[i].ScoorePrice; //���������� ������� ��������(������) ����� �������� �� ���������� ��� �� �����, ���� �������� �� ������

                Debug.Log($"Add fight inv ={countAddRes}-count");
                TypeDropItemResFightOnThisLevel[i].Count += countAddRes;   //��������� ���������� � ��������� ������
                lostValueScoore -= TypeDropItemResFightOnThisLevel[i].ScoorePrice * countAddRes; //�������� ����������� ���������� ����� �� ������ �����

                countAddRes = 0; //�������� �������
            }






        }
        
    }

    //�������� ���������� ������� �� ��������� ��� 
    public void RemoveItemRes(ItemRes item, int count)
    {  
        _itemResFound = false;

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
    //�������� ����� ������� �� ��������� ��� 
    public void RemoveAllValueItemRes(ItemRes item)
    {
        ItemResFightData.Remove(item);
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
    public void TrasportItemResToBase(int id, int count)
    {
        InventoryBase.instance.AddItemRes(id , count);

        


    }

    // ������� ���� ��������� Res � �������� �� �� ����
    public void TrasportAllItemResToBase()
    {
        InventoryBase.instance.AddCountPerk(_player.CountPerks);

        for (int i = 0; i < TypeDropItemResFightOnThisLevel.Count; i++)
        {
            TrasportItemResToBase(TypeDropItemResFightOnThisLevel[i].ID, TypeDropItemResFightOnThisLevel[i].Count);
            
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
