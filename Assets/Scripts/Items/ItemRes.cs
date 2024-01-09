using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemResData",menuName = "AddItem/Resource")]
public class ItemRes : ScriptableObject
{
    [SerializeField] public int ID;
    [SerializeField] public string Name;
    [SerializeField] public string Description;
    [SerializeField] public List<TypeRarityItem> Rarity;
    [SerializeField] public Sprite Icon;
    [SerializeField] public int Count;
    [SerializeField] public int SellPrice;
    [SerializeField] public int ScoorePrice;



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
