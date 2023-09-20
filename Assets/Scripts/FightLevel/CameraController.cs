using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] Player _player;

    [SerializeField] float _dumping = 1.3f;
    private Vector2 _offset = new Vector2(2f, 1f);
    private bool _isLeft;
    private int _lastX;
    private Transform _playerTransform;

    private void Start()
    {
        _playerTransform = _player.transform;
        _offset = new Vector2(Mathf.Abs(_offset.x),_offset.y);
        FindPlayer(_isLeft);
        
    }

    private void FindPlayer(bool playerIsLeft)
    {
        _lastX = Mathf.RoundToInt(_playerTransform.position.x);
        if(playerIsLeft)
        {
            transform.position = new Vector3(_playerTransform.position.x - _offset.x, _playerTransform.position.y - _offset.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(_playerTransform.position.x + _offset.x, _playerTransform.position.y + _offset.y, transform.position.z);
        }
    }



    [SerializeField] private float _leftLimit;
    [SerializeField] private float _rightLimit;
    [SerializeField] private float _bottomLimit;
    [SerializeField] private float _upperLimit;

    [SerializeField] private bool _gizmozShowLimit;

    private void Update()
    {
        if (_playerTransform)
        {
            int currentX = Mathf.RoundToInt(_playerTransform.position.x);
            if (currentX > _lastX) _isLeft = false; else if (currentX < _lastX) _isLeft = true;
            _lastX = Mathf.RoundToInt(_playerTransform.position.x);

            Vector3 target;

            if (_isLeft)
            {
                target = new Vector3(_playerTransform.position.x - _offset.x, _playerTransform.position.y - _offset.y, transform.position.z);
            }
            else
            {
                target = new Vector3(_playerTransform.position.x + _offset.x, _playerTransform.position.y + _offset.y, transform.position.z);
            }

            Vector3 currentPosition = Vector3.Lerp(transform.position, target, _dumping * Time.deltaTime);
            transform.position = currentPosition;
        }










        _camera.transform.position = new Vector3(Mathf.Clamp(_camera.transform.position.x, _leftLimit, _rightLimit), 
            Mathf.Clamp(_camera.transform.position.x, _bottomLimit, _upperLimit),_camera.transform.position.z);
    }

    private void OnDrawGizmos()
    {
        if( _gizmozShowLimit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector2(_leftLimit, _upperLimit), new Vector2(_rightLimit, _upperLimit));
            Gizmos.DrawLine(new Vector2(_leftLimit, _bottomLimit), new Vector2(_rightLimit, _bottomLimit));
            Gizmos.DrawLine(new Vector2(_leftLimit, _upperLimit), new Vector2(_leftLimit, _bottomLimit));
            Gizmos.DrawLine(new Vector2(_rightLimit, _upperLimit), new Vector2(_rightLimit, _bottomLimit));
        }
    }
}
