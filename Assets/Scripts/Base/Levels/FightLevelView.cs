using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class FightLevelView : MonoBehaviour
{
    //[SerializeField] private SceneAsset sceneView;
    //[SerializeField] private string Name;
    //[SerializeField] private int IdLevel;
    [SerializeField] private bool _statusOpened;

    [SerializeField] private Color _ColorOpened;
    [SerializeField] private Color _ColorClose;

    [SerializeField] private TMP_Text _label;
    [SerializeField] private Button _button;

    private Color _currentColorButton;

    private void OnEnable()
    {
       // _button.onClick.AddListener(OnButtonClick());
        
    }

    private void OnDisable()
    {
        //_button.onClick.RemoveListener(OnButtonClick());
        
    }


    //private void Start()
    //{
    //    _currentColorButton = _button.GetComponent<Image>().color;
    //}



    public void Render(FightLevel level)
    {
        

        _label.text = level.IdLevel.ToString();
        _statusOpened = level.StatusOpened;
        if (_statusOpened)
        {
            _button.interactable = true;
            _button.GetComponent<Image>().color = _ColorOpened;
        }
        else
        {
            _button.interactable = false;
            _button.GetComponent<Image>().color = _ColorClose;
        }
    }

    public void OnButtonClick()
    {
        if (_statusOpened)
        {
            LevelManager.sceneToStart = _label.text;

        }
    }
}
