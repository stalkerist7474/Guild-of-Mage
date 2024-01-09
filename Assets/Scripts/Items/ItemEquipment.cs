using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemEquipmentData", menuName = "AddItem/Equipment")]
public class ItemEquipment : ScriptableObject
{
    public int ID;
    public string Name;
    public string Description;

    [SerializeField] public List<TypeRarityItem> Rarity;
    [SerializeField] public List<EquipmentSlotType> EquipmentSlot;
    public Sprite Icon;
    public int SellPrice;

    //Бонусы
    public float BonusValueAttack;
    public float BonusValueBlock;
    public float BonusValueSpeed;

    private void OnValidate()
    {
        List<TypeRarityItem> duplicates = Rarity.GroupBy(rarity => rarity)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key)
            .ToList();

        List<EquipmentSlotType> duplicates2 = EquipmentSlot.GroupBy(slot => slot)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key)
            .ToList();



        foreach (var item in duplicates)
            Debug.LogError(item.ToString());

        foreach (var item in duplicates2)
            Debug.LogError(item.ToString());
    }
}
