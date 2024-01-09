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
    [SerializeField] private int _maxHeath;
    [SerializeField] private List<Spell> _spells;
    [SerializeField] private Transform _shootpoint;
    [SerializeField] private float _areaAttack;
    [SerializeField] private GameObject _currentEnemyToAttack;

    [SerializeField] public int _needExpForUp = 10;
    
    private int _currentHeath;
    private Animator _animation;
    private Spell _currentSpell;

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChanged;
    public event UnityAction<int, int> ExpChanged;
    public event UnityAction<int> CountPerksChanged;
    public static event UnityAction OnLevelGameOver;


    public int Money { get; private set; }
    public int Exp { get; private set; }
    public int CountPerks { get; private set; }

    public List<GameObject> EnemysInAttackCircle = new List<GameObject>();
    public GameObject CurrentEnemyToAttack => _currentEnemyToAttack;
    public float AreaAttack => _areaAttack;
    public int CurrentHeath => _currentHeath;
    public int MaxHeath => _maxHeath;
    public Transform Shootpoint => _shootpoint;
   

    private void Start()
    {
        _player = GetComponent<Player>();
        _animation = GetComponent<Animator>();
        _currentHeath = _maxHeath;
        _currentSpell = _spells[0];
        ExpChanged?.Invoke(Exp, _needExpForUp); // для правильного отображение опыта в начале уровня


    }
    private void Update()
    {
       
        _player.CheckNearEnemyInArea(EnemysInAttackCircle);
       
    }


    private void Attack(GameObject enemy)
    {

     
        _currentSpell.CastSpell(_shootpoint);

        _animation.ResetTrigger("Fire");

    }
    

    //Клик атаки
    private void OnFire()
    {
        if (_currentEnemyToAttack != null)
        {
            Attack(_currentEnemyToAttack);
            Debug.Log("SHOT");
            _animation.SetTrigger("Fire");

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

    //получение урона от врагов
    public void ApplyDamage(int damage)
    {
        _currentHeath -= damage;
        HealthChanged?.Invoke(_currentHeath, _maxHeath);

        if (_currentHeath <= 0)
        {
            Debug.Log("Ты умер");
            //Destroy(gameObject);
            OnLevelGameOver?.Invoke();
        }
    }

    private void OnEnemyDied(int reward)
    {
        
    }


    public void AddMoney(int money)
    {
        Money += money;
        MoneyChanged?.Invoke(Money);

    }
    public void RemoveMoney(int money)
    {
        Money -= money;
        MoneyChanged?.Invoke(Money);

    }
    public void AddExp(int exp)
    {
        Exp += exp;
        ExpChanged?.Invoke(Exp, _needExpForUp);
        if (Exp == _needExpForUp)
        {
            Exp = 0;
            CountPerks++;
            ExpChanged?.Invoke(Exp, _needExpForUp);
            CountPerksChanged?.Invoke(CountPerks);
        }

    }
    //лечение персонажа = надо убрать баг с тем что можно похилиться выше максимального уровня здоровья
    public void AddHitPoints(int hitpoints)
    {
        _player._currentHeath += hitpoints;
        HealthChanged?.Invoke(_currentHeath, _maxHeath);
    }

    public void ClearMoney()
    {
        Money = 0;
        MoneyChanged?.Invoke(Money);

    }




    public void UnlockSpell(Spell spell)
    {
        
    }
}


