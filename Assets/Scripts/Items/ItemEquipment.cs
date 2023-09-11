using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemEquipmentData", menuName = "AddItem/Equipment")]
public class ItemEquipment : ScriptableObject
{
    public int ID;
    public string Name;
    public string Description;
    public string Rarity;  //{ "Common", "Rare", "Epic", "Legendary" };
    public string EquipmentSlot; //{ "Weapon", "Chest", "Boots", "Hat" };
    public Sprite Icon;
    public int SellPrice;

    //Бонусы
    public float BonusValueAttack;
    public float BonusValueBlock;
    public float BonusValueSpeed;
    

}
