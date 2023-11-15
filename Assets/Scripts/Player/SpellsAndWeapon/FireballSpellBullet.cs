using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpellBullet : SpellBullet
{
    

    private int _damage;
    private float _speed;
    private Vector2 _vectorFlight;
    private Transform _enemy;

    private void Start()
    {
        
        _damage = Damage;
        _speed = Speed;
        _vectorFlight = VectorFlight;

    }

    void Update()
    {
        _enemy = GetCurrentEnemy().transform;
        Debug.Log(GetCurrentEnemy().name);
                
        _vectorFlight = _enemy.position - transform.position; // ��������� �����������
                
        transform.Translate(_vectorFlight * _speed * Time.deltaTime);//����������

    }

    private void OnTriggerEnter2D(Collider2D collision) //������ �� ��������� � ����
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);

            Destroy(gameObject);
        }
    }

    private GameObject GetCurrentEnemy() // ��������� ����
    {
        GameObject go = GameObject.Find("Player");
        Player player = go.GetComponent<Player>();
        GameObject enemy = player.CurrentEnemyToAttack;
        return enemy;
    }
}
