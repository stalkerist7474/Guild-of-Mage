using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]
public class AreaAttack : MonoBehaviour
{

    private void Update()
    {
        ChangeSizeAttackArea();
    }

    public void ChangeSizeAttackArea()
    {
        GameObject go = GameObject.Find("Player");
        Player player = go.GetComponent<Player>();
        float radius = player.AreaAttack;

        transform.localScale = new Vector3(radius, radius, radius);
    }




}
