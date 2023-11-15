using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpellView : MonoBehaviour
{
    [SerializeField] Spell spell;
    [SerializeField] MainSpellView nextSpellUnlock;
    [SerializeField] bool _gizmozShowLimit;
    MainSpellView currentSpell;

    //Line

    //private LineRenderer lr;
    //[SerializeField] private Transform[] points;

    //private void Awake()
    //{
    //    lr = GetComponent<LineRenderer>();
    //}

    private void Start()
    {
        currentSpell = GetComponent<MainSpellView>();
        nextSpellUnlock = GetComponent<MainSpellView>();


        //for (int i = 0; i < points.Length; i++)
        //{
        //    lr.SetPosition(i, points[i].position);
        //}

       // SetUpLine(points);
    }

    //public void SetUpLine(Transform[] points)
    //{
    //    lr.positionCount = points.Length;
    //    this.points = points;
    //}
    //private void OnDrawGizmos()
    //{
    //    Debug.Log("Giz");
    //    if (_gizmozShowLimit)
    //    {
    //        Debug.Log("Giz2");
    //        Gizmos.color = Color.red;
            
    //        Gizmos.DrawLine(new Vector2(currentSpell.transform.position.x, currentSpell.transform.position.y), new Vector2(nextSpellUnlock.transform.position.x, nextSpellUnlock.transform.position.y)); //вверх
    //        //Gizmos.DrawLine(new Vector2(_minPosition.x - 9, _minPosition.y - 5), new Vector2(_maxPosition.x + 9, _minPosition.y - 5)); //низ

    //        //Gizmos.DrawLine(new Vector2(_minPosition.x - 9, _minPosition.y - 5), new Vector2(_minPosition.x - 9, _maxPosition.y + 5));//лево
    //        //Gizmos.DrawLine(new Vector2(_maxPosition.x + 9, _maxPosition.y + 5), new Vector2(_maxPosition.x + 9, _minPosition.y - 5));//право
    //    }
    //}
}
