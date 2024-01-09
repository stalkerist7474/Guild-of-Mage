using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemRes : MonoBehaviour
{
    public int ID;
    public string Name;
    public string Description;
    [SerializeField] public List<TypeRarityItem> Rarity;
    public Sprite Icon;
    public int Count;
    public int SellPrice;
 

    public DropItemRes(int id, string name, string description, TypeRarityItem type, int countItem, int sellPrice)
    {
        this.ID = id;
        this.Name = name;
        this.Description = description;
        this.Rarity[0] = type;

        this.Count = countItem;
        this.SellPrice = sellPrice;
    }
}
