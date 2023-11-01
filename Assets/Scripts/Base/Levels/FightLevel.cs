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
    [SerializeField] private bool _statusOpened;
}
