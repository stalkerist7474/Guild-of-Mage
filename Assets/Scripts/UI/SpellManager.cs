using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public static SpellManager instance;
    [SerializeField] private InventoryBase _base;
    [SerializeField] private List<GameObject> _buttonSpells;
    private Spell _activeSpell;


   

    private void Awake()
    {

        foreach (GameObject spell in _buttonSpells)
        {
            if (spell.GetComponent<ChangeWeaponMain>()._spell.IsActive)
            {
                _activeSpell = spell.GetComponent<ChangeWeaponMain>()._spell;
            }
        }


        _activeSpell = _buttonSpells[0].GetComponent<ChangeWeaponMain>()._spell;

        if (!instance)      //гарантия что экземпляр будет один
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        

    }


    public void ChangeWeapon(ChangeWeaponMain spellbutton)
    {
        //проверяем можем ли купить если еще не куплено и если да меняем статус и отнимаем очки перков
        if (spellbutton._spell.IsBuyed == false)
        {
            if (CheckRes(spellbutton))
            {
                spellbutton._spell.Buy();
                _base.DecreasePerkPoint(spellbutton._spell.Price);
            }
        }


        //активация уже купленого но активного заклинания
        if ((spellbutton._spell.IsBuyed) && (spellbutton._spell.IsActive != true))
        {
            foreach (GameObject spell in _buttonSpells)
            {
                if (spell.GetComponent<ChangeWeaponMain>()._spell.IsActive)
                {
                    spell.GetComponent<ChangeWeaponMain>()._spell.Deactivate();
                }
            }

            spellbutton._spell.Activate();
            _activeSpell = spellbutton._spell;
        }


    }

    public Spell GetSpell()
    {

        return _activeSpell;


    }

    private bool CheckRes(ChangeWeaponMain spell)
    {

        if (_base.GetCountPerk() >= spell._spell.Price)
        {
            return true;
        }
        else { return false; }
    }
}
