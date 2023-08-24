using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private int _heath;
    [SerializeField] private List<Spell> _spells;
    [SerializeField] private Transform _shootpoint;
    [SerializeField] private float _areaAttack;
    [SerializeField] private GameObject _currentEnemyToAttack;

    private int _currentHeath;
    private Animator _animation;
    private Spell _currentSpell;

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyOrExpChanged;
    public event UnityAction<int> CountPerksChanged;


    public int Money { get; private set; }
    public int Exp { get; private set; }
    public int CountPerks { get; private set; }

    public List<GameObject> EnemysInAttackCircle = new List<GameObject>();
    public GameObject CurrentEnemyToAttack => _currentEnemyToAttack;
    public float AreaAttack => _areaAttack;
    public Transform Shootpoint => _shootpoint;
   

    private void Start()
    {
        _player = GetComponent<Player>();
        _animation = GetComponent<Animator>();
        _currentHeath = _heath;
        _currentSpell = _spells[0];
        

    }
    private void Update()
    {
       
        _player.CheckNearEnemyInArea(EnemysInAttackCircle);
       
    }


    private void Attack(GameObject enemy)
    {

     
        _currentSpell.CastSpell(_shootpoint);

        

    }
    

    //Клик атаки
    private void OnFire()
    {
        if (_currentEnemyToAttack != null)
        {
            Attack(_currentEnemyToAttack);
            Debug.Log("SHOT");

        }
    }
    private void OnAltFire()
    {
        //Debug.Log("==========================================");
        //Debug.Log($"Текущая цель ближайшая-{_currentEnemyToAttack}");
        //Debug.Log($"Колво целей в списке целей круга-{EnemysInAttackCircle.Count}");
        //Debug.Log($"В списке на 0-{EnemysInAttackCircle.Capacity}");
        //Debug.Log("==========================================");
        
    }
    
    //выбор ближайшей цели
    private void CheckNearEnemyInArea(List<GameObject> gameObjects)
    {
        float minNearX = 50; 
        float minNearY = 50;
        gameObjects.RemoveAll(s => s == null);

        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (gameObjects[i].transform.position.x < minNearX && gameObjects[i].transform.position.x < minNearY)
            {
                minNearX = gameObjects[i].transform.position.x;
                minNearY = gameObjects[i].transform.position.y;
                _currentEnemyToAttack = gameObjects[i];

             
                //Debug.Log($"Ближайшая Цель ВЫБРАНА ======{_currentEnemyToAttack.name}");
            }
        }
    }
    

    //включение в список обласи атаки
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
        EnemysInAttackCircle.Add( collision.gameObject );
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemysInAttackCircle.Remove( collision.gameObject );
        if (collision.gameObject == _currentEnemyToAttack)
            _currentEnemyToAttack = null;
    }

    public void ChangeWeapon(Spell spell)
    {
        _currentSpell = spell;
    }


    public void ApplyDamage(int damage)
    {
        _currentHeath -= damage;
        HealthChanged?.Invoke(_currentHeath, _heath);

        if (_currentHeath <= 0)
        {

            Destroy(gameObject);
        }
    }

    private void OnEnemyDied(int reward)
    {
        
    }


    public void AddMoney(int money)
    {
        Money += money;
        MoneyOrExpChanged?.Invoke(Money);

    }
    public void AddExp(int exp)
    {
        Exp += exp;
        MoneyOrExpChanged?.Invoke(Exp);
        if (Exp == 10)
        {
            Exp = 0;
            CountPerks++;
            MoneyOrExpChanged?.Invoke(Exp);
            CountPerksChanged?.Invoke(CountPerks);
        }

    }

   


    public void UnlockSpell(Spell spell)
    {
        
    }
}


