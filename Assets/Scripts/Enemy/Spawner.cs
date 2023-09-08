using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private float _timeAfterLastWaveDone;
    private int _spawned;
    private int _enemyCount;

    public event UnityAction AllEnemySpawned;
    public event UnityAction AllEnemyDieCurrentWave;
    public event UnityAction AllWaveEnd;

    public event UnityAction<int, int> EnemyCountChanged;

    private void Start()
    {
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {
        _timeAfterLastSpawn += Time.deltaTime;
        _timeAfterLastWaveDone += Time.deltaTime;

        //проверка есть ли еще волны
        if (_currentWave == null)
            return;

        //проверка для спавна монстра по времени задержки спавна
        if(_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawned++;
            _enemyCount++;
            _timeAfterLastSpawn = 0;
            EnemyCountChanged?.Invoke(_spawned, _currentWave.Count);
        }

        //проверка все ли монстры заспавнены
        if(_currentWave.Count <= _spawned)
        {
            //и если число волн на уровень больше числа текущей волны 
            if (_waves.Count > _currentWaveNumber + 1)
                AllEnemySpawned?.Invoke();
            
                
                
            _currentWave = null;
        }

        //если все монстры заспавнены и число монстров оставшихся на карте равно 0
        if(_spawned == _currentWave.Count && _enemyCount <= 0)
        {
            Debug.Log("End wave");
            _enemyCount = 0;
            _timeAfterLastWaveDone = 0;
            if (_timeAfterLastWaveDone >= _currentWave.DelayWave)
            {
                NextWave();
            }

        }
        
    }
    // Спавн врагов
    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.Dying += OnEnemyDying;
    }

    //Установка начальной волны
    private void SetWave(int index)
    {
        _currentWave = _waves[index];
        EnemyCountChanged?.Invoke(0, 1);

    }

    //установка следующей волны
    public void NextWave()
    {
        _currentWaveNumber++;
        SetWave(_currentWaveNumber);
        _spawned = 0;
    }

    //враг убит
    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;
        _enemyCount--;
        _player.AddMoney(enemy.RewardGold);
        _player.AddExp(enemy.RewardExp);
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