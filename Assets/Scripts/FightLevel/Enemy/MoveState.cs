using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    [SerializeField] private float _speed;
    private Animator _animation;
    private Vector2 _movementVector;
    private SpriteRenderer _renderer;
    private void Awake()
    {
        _animation = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _movementVector = Vector2.MoveTowards(transform.position, Target.transform.position, _speed * Time.deltaTime);
        transform.position = _movementVector;

        _animation.SetFloat("MoveX", Mathf.Abs(_movementVector.x));
        if (_movementVector.x == 0)
        {
            _animation.SetFloat("MoveX", Mathf.Abs(_movementVector.y));
        }
        Flip();
    }

    private void Flip()
    {
        if (_movementVector.x > 0)
        {
            _renderer.flipX = false;
        }
        if (_movementVector.x < 0)
        {
            _renderer.flipX = true;
        }
        //Renderer.flipX = _movementInput.x < 0;
    }
}
