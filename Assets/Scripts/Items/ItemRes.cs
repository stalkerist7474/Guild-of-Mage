using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemResData",menuName = "AddItem/Resource")]
public class ItemRes : ScriptableObject
{
    public int ID;
    public string Name;
    public string Description;
    public string Rarity;  //{ "Common", "Rare", "Epic", "Legendary" };
    public Sprite Icon;
    public int SellPrice;
}
