using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightLevel : MonoBehaviour
{
    [SerializeField] private SceneAsset sceneView;
    [SerializeField] public string Name;
    [SerializeField] public int IdLevel;
    [SerializeField] public bool StatusOpened;

    [SerializeField] private Color _ColorOpened;
    [SerializeField] private Color _ColorClose;
}
