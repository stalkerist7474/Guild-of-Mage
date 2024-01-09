using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    //public WinLevel winLevel;

    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private float _timeAfterLastWaveDone;
    private float _timeNextWaveDelay;
    private int _spawned;
    private int _enemyCount;
    private int _numWaveOnThisLevel;
    private bool _waveComplete;
    private bool _waveAllEnemySpawned;

    private string _nameLevel;

    //drop setting

    public int ResScoore;

    public event UnityAction AllEnemySpawned;
    //public event UnityAction OnAllEnemyDieCurrentWave;
    //public event UnityAction OnAllWaveEnd;

    public static event UnityAction OnLevelWin;
    //public event UnityAction OnLevelGameOver;

    public event UnityAction<int, int> EnemyCountChanged;


    private void OnEnable()
    {
        AllEnemySpawned += OnAllEnemySpawned;
        //OnLevelWin += winLevel.OnWin;
    }


    private void OnDisable()
    {
        AllEnemySpawned -= OnAllEnemySpawned;

    }


    private void Start()
    {
        _nameLevel = SceneManager.GetActiveScene().name;
        Debug.Log(_nameLevel);

        _numWaveOnThisLevel = _waves.Count;

        SetWave(_currentWaveNumber);
        _waveComplete = false;
        _waveAllEnemySpawned = false;
    }

    private void Update()
    {
        _timeAfterLastSpawn += Time.deltaTime;
        _timeAfterLastWaveDone += Time.deltaTime;


        // ������� ���� ��� ���� ���������� � ��� ��� �����
        if (_waveAllEnemySpawned == true)
        {
           // Debug.Log("End wave1");
            if (_waveComplete == true)
            {
               // Debug.Log("End wave2");
                            
                if (_timeAfterLastWaveDone >= _timeNextWaveDelay)
                {
                    NextWave();
                   // Debug.Log("End wave3");
                }
            }

        }
        //�������� ���� �� ��� �����
        if (_currentWave == null)
            return;

        //�������� ��� ������ ������� �� ������� �������� ������
        if(_timeAfterLastSpawn >= _currentWave.Delay)
        {
            
            InstantiateEnemy();
            _spawned++;
            _enemyCount++;
            _timeAfterLastSpawn = 0;
            EnemyCountChanged?.Invoke(_spawned, _currentWave.Count);
        }
        


        //�������� ��� �� ������� ����������
        if(_currentWave.Count <= _spawned)
        {
            //� ���� ����� ���� �� ������� ������ ����� ������� ����� 
            if (_waves.Count > _currentWaveNumber + 1)
                AllEnemySpawned?.Invoke();
                

            _currentWave = null;
         
        }


        
    }
    // ����� ������
    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.Dying += OnEnemyDying;
    }

    //��������� ��������� �����
    private void SetWave(int index)
    {
        _currentWave = _waves[index];
        EnemyCountChanged?.Invoke(0, 1);
        _timeNextWaveDelay = _currentWave.DelayWave;    //���������� ����� �������� ���� �����

    }

    //��������� ��������� �����
    public void NextWave()
    {
        _spawned = 0;
        _waveAllEnemySpawned = false;
        _waveComplete = false;  
        _currentWaveNumber++;
        SetWave(_currentWaveNumber);
    }

    //���� ����
    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;
        _enemyCount--;
        _player.AddMoney(enemy.RewardGold);
        _player.AddExp(enemy.RewardExp);


        ResScoore += enemy.RewardResScoore;
        
        
        if (_enemyCount == 0)
        {
            _timeAfterLastWaveDone = 0;
            _waveComplete = true;
            _numWaveOnThisLevel--;
                if (_numWaveOnThisLevel == 0)
                {
                    Win();
                }

        }
    }

    //��� ����� ����������
    private void OnAllEnemySpawned()
    {
        _waveAllEnemySpawned = true;

    }

    //����� �������
    public void Win()
    {

        CalculatedDrop();
        LevelManager.instance.CompleteLevel(_nameLevel);
        OnLevelWin?.Invoke();

    }

    private void CalculatedDrop()
    {
        
        InventoryFight.instance.AddItemRes(ResScoore);
    }

}


[System.Serializable]
public class Wave
{
    public GameObject Template;
    public float Delay;
    public int Count;
    public int DelayWave;
}