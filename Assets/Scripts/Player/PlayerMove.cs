using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMove : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float _timeSmoothMove = 0.1f;

    

    private Rigidbody2D _rigidbody2D;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;



    private void Awake()
    {
        


        _rigidbody2D = GetComponent<Rigidbody2D>();
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

        _rigidbody2D.velocity = _smoothedMovementInput * _speed;
    }



    private void OnMove(InputValue inputValue)
    {
        

        _movementInput = inputValue.Get<Vector2>();
        
    }
}
