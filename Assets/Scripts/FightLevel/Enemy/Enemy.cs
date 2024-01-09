using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _heath;
    [SerializeField] private int _rewardGold;
    [SerializeField] private int _rewardExp;

    [SerializeField] private int _rewardResScoore;


    private Animator _animation;
    private Player _target;

    public int RewardGold => _rewardGold;
    public int RewardExp => _rewardExp;
    public int RewardResScoore => _rewardResScoore;
    public Player Target => _target;

    public event UnityAction<Enemy> Dying;

    private void Awake()
    {
        _animation = GetComponent<Animator>();
    }

    public void Init(Player target)
    {
        _target = target;
    }

    public void TakeDamage(int damage)
    {
        _heath -= damage;
        _animation.SetTrigger("Hit");

        if (_heath <= 0)
        {
            Dying?.Invoke(this);

            _animation.SetBool("Dead", true);
            //----------------

            //----------------
            Destroy(gameObject);
            GetListEnemys().Remove(null);
        }
    }


    private List<GameObject> GetListEnemys()
    {
        GameObject go = GameObject.Find("Player");
        Player player = go.GetComponent<Player>();
        List<GameObject> enemys = player.EnemysInAttackCircle;
        return enemys;
    }
}
