using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeaponMain : MonoBehaviour
{
    [SerializeField] public Spell _spell;

    [SerializeField] private Color _ColorOn;
    [SerializeField] private Color _ColorOff;
    [SerializeField] private Button _button;
    private Spell _currentSpell;
    void Start()
    {
        _button = GetComponent<Button>();

        if (_spell.IsActive)
        {
            _currentSpell = _spell;
        }
        
    }


}
