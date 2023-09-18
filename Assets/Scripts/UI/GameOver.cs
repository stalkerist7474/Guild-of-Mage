using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private CanvasGroup _gameOverCanvas;

    private void OnEnable()
    {
        Player.OnLevelGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        Player.OnLevelGameOver -= OnGameOver;
    }

    private void Awake()
    {
        _gameOverCanvas = GetComponent<CanvasGroup>();
        _gameOverCanvas.alpha = 0.0f;
    }


    //Когда победил
    public void OnGameOver()
    {
        _gameOverCanvas.alpha = 1.0f;
    }
}
