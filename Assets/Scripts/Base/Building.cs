using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class Building : MonoBehaviour
{
    [SerializeField] private string _titleBuilding;
    [SerializeField] private string _aboutBuilding;
    [SerializeField] private List<Stage> _stages = new List<Stage>();

    public event UnityAction OnNextStageComplete;

    [SerializeField] private Sprite _currentStageicon;

    private void Start()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = _currentStageicon;
    }
}
