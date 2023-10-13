using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemResData",menuName = "AddItem/Resource")]
public class ItemRes : ScriptableObject
{
    public int ID;
    public string Name;
    public string Description;
    //public string Rarity;  //{ "Common", "Rare", "Epic", "Legendary" };
    [SerializeField] public List<TypeRarityItem> Rarity;
    public Sprite Icon;
    public int Count;
    public int SellPrice;

    private void OnValidate()
    {
        List<TypeRarityItem> duplicates = Rarity.GroupBy(rarity => rarity)
            .Where(group  => group.Count() > 1)
            .Select(group => group.Key) 
            .ToList();

        foreach (var item in duplicates)
            Debug.LogError(item.ToString());
    }
}
