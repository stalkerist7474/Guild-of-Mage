using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineUI : MonoBehaviour
{
    [SerializeField] private LineController lineController;
    [SerializeField] private Transform[] points;

    private void Start()
    {
        lineController.SetUpLine(points);
    }
}
