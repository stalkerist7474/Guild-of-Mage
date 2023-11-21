using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBallSpellBullet : SpellBullet
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

        Invoke("DestroyBullet", FlightBulletRange); //страховка что пуля не будет летать вечно

    }

    void Update()
    {

        _enemy = GetCurrentEnemy().transform;
        Debug.Log(GetCurrentEnemy().name);

        _vectorFlight = _enemy.position - transform.position; // вычисляем направление

        transform.Translate(_vectorFlight * _speed * Time.deltaTime);//перемещаем




    }

    private void OnTriggerEnter2D(Collider2D collision) //Эффект от попадания в цель
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            GameObject effect = Instantiate(HitEffect, transform.position, Quaternion.identity);  //анимация попадания пули
            Destroy(effect, 2f);


            enemy.TakeDamage(_damage);

            Destroy(gameObject);
        }
    }

    private GameObject GetCurrentEnemy() // получение цели
    {
        GameObject go = GameObject.Find("Player");
        Player player = go.GetComponent<Player>();
        GameObject enemy = player.CurrentEnemyToAttack;
        return enemy;
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
