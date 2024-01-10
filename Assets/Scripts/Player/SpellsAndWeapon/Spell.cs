using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private float _cooldown;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isBuyed = false;
    [SerializeField] private bool _isActive = false;
    [SerializeField] private Dictionary<ParticleSystem, GameObject> _particles;
    [SerializeField] protected SpellBullet SpellBullet;



    public string Label => _label;
    public int Price => _price;
    public float Cooldown => _cooldown ;
    public Sprite Icon => _icon;
    public bool IsBuyed => _isBuyed;
    public bool IsActive => _isActive;

    public Dictionary<ParticleSystem, GameObject> Particles=> _particles;
    public abstract void CastSpell(Transform shootPoint);
    public abstract void StartCooldown();

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void Buy()
    {
        _isBuyed = true;
    }

    public void Activate()
    {
        _isActive = true;
    }

    public void Deactivate()
    {
        _isActive = false;
    }

}
