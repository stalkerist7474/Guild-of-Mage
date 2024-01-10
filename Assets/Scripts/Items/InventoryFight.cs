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
    [SerializeField, Range(1f, 10f)] private int _maxRandomMultiply;
    private bool _itemResFound = false;

    public event UnityAction OnBaseResourcesChange;
    private void Awake()
    {
        instance = this;


    }
    private void Start()
    {


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


    //добавление ресурса в инвентарь боя
    public void AddItemRes(int resScooreRating)
    {
        int coefficientRand = Random.RandomRange(1, _maxRandomMultiply);

        int lostValueScoore = resScooreRating;
        Debug.Log($"resScooreRating={resScooreRating}");

        for (int i = 0; i < TypeDropItemResFightOnThisLevel.Count; i++)
        {
            int countAddRes = 0;
            if (lostValueScoore > 0)        // проверка что общий счет еще положительный
            {

                if( i == TypeDropItemResFightOnThisLevel.Count)                         //если это последний добавляемый ресурс
                {
                    countAddRes = lostValueScoore % TypeDropItemResFightOnThisLevel[i].ScoorePrice;
                    TypeDropItemResFightOnThisLevel[i].Count += countAddRes;
                    countAddRes = 0;
                }

                countAddRes = (lostValueScoore / coefficientRand) % TypeDropItemResFightOnThisLevel[i].ScoorePrice; //определяем сколько добавить(купить) можно ресурсов на оставшееся кол во очков, есть поправка на рандом

                Debug.Log($"Add fight inv ={countAddRes}-count");
                TypeDropItemResFightOnThisLevel[i].Count += countAddRes;   //добавляем количество в скриптабл объект
                lostValueScoore -= TypeDropItemResFightOnThisLevel[i].ScoorePrice * countAddRes; //отнимаем затраченное количество очков от общего счета

                countAddRes = 0; //обнуляем счетчик
            }






        }
        
    }

    //Удаление количество ресурса из инвентаря боя 
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

        


    }

    // перебор всех предметов Res и отправка их на базу
    public void TrasportAllItemResToBase()
    {
        InventoryBase.instance.AddCountPerk(_player.CountPerks);

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
