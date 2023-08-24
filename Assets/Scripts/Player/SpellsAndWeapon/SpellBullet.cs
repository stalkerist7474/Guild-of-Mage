using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellBullet : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _flightBulletRange = 500f;
    [SerializeField] private Dictionary<ParticleSystem, GameObject> _particlesBullet;
    
    private Vector2 _vectorFlight;


    public string Label => _label;
    public int Damage => _damage;
    public float Speed => _speed;
    public float FlightBulletRange => _flightBulletRange;
    public Vector2 VectorFlight => _vectorFlight;
    public Dictionary<ParticleSystem, GameObject> ParticlesBullet => _particlesBullet;

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private GameObject GetCurrentEnemy()
    {
        GameObject go = GameObject.Find("Player");
        Player player = go.GetComponent<Player>();
        GameObject enemy = player.CurrentEnemyToAttack;
        return enemy;
    }
}
