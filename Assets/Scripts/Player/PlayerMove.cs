using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMove : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float _timeSmoothMove = 0.1f;

    public Animator Animation;
    public SpriteRenderer Renderer;


    private Rigidbody2D _rigidbody2D;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;



    private void Awake()
    {
        


        _rigidbody2D = GetComponent<Rigidbody2D>();
        Animation = GetComponent<Animator>();
        Renderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void FixedUpdate()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(_smoothedMovementInput, _movementInput, ref _movementInputSmoothVelocity, _timeSmoothMove);

        Animation.SetFloat("MoveX", Mathf.Abs(_movementInput.x));
        if(_movementInput.x ==0)
        {
            Animation.SetFloat("MoveX", Mathf.Abs(_movementInput.y));
        }
 

        _rigidbody2D.velocity = _smoothedMovementInput * _speed;

        Flip();
    }



    private void OnMove(InputValue inputValue)
    {
        

        _movementInput = inputValue.Get<Vector2>();
        
    }
    private void Flip()
    {
        if(_movementInput.x > 0)
        {
            Renderer.flipX = false;
        }
        if( _movementInput.x < 0)
        {
            Renderer.flipX = true;
        }

    }

}
