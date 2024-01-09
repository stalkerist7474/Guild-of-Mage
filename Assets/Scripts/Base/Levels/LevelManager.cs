using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public static string sceneToStart;
    [SerializeField] private FightLevelView _LevelTemplate;
    [SerializeField] private GameObject _itemContainer;
    [SerializeField] public List<FightLevel> _LevelsList;

    private void Awake()
    {

        if (!instance)      //гарантия что экземпляр будет один
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }


    }
    private void Start()
    {
        for (int i = 0; i < _LevelsList.Count; i++)
        {
            AddLevel(_LevelsList[i]);
        }
    }


    //добавление кнопки в окно контента
    private void AddLevel(FightLevel level)
    {
        var view = Instantiate(_LevelTemplate, _itemContainer.transform);

        view.Render(level);
    }



    public void CompleteLevel(string currentLevel)
    {
        int numLevel = int.Parse(currentLevel) + 1;
        for (int i = 0; i < _LevelsList.Count; i++) 
        {
            if (_LevelsList[i].IdLevel == numLevel)
            {
                _LevelsList[i].StatusOpened = true;
                return;
            }
        
        }

    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneToStart);
    }
}
