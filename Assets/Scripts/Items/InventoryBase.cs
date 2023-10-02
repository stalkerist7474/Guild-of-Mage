using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class InventoryBase : MonoBehaviour
{
    public static InventoryBase instance;
    //��������� ����
    [SerializeField] List <ItemEquipment> ItemEquipmentStart = new List <ItemEquipment> ();
    [SerializeField] List <ItemRes> ItemResStart = new List <ItemRes> ();

    //������ � ��������� ����
    public List<ItemEquipment> ItemEquipmentBase = new List<ItemEquipment>();
    public List<ItemRes> ItemResBase = new List<ItemRes>();
    public int BalanceBase;

    //������� ��������� ��������
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


    //���������� �������� � ��������� ����
    public void AddItemEquipment(ItemEquipment item)
    {
        ItemEquipmentBase.Add (item);
    }

    //�������� �������� �� ��������� ����
    public void RemoveItemEquipment(ItemEquipment item)
    {
        ItemEquipmentBase.Remove (item);
    }


    //���������� ������� � ��������� ����
    public void AddItemRes(ItemRes item)
    {
        ItemResBase.Add(item);
    }

    //�������� ������� �� ��������� ����
    public void RemoveItemRes(ItemRes item)
    {
        ItemResBase.Remove(item);
    }


    //���������� ������� ���� ������
    public void AddMoneyBase(int money)
    {
        BalanceBase = BalanceBase + money;
    }
    //���������� ������� ���� ������
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
