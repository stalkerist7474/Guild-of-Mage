using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    //public static LevelManager instance;
    //private string sceneToStart;


    //private void Awake()
    //{

    //    if (!instance)      //гарантия что экземпляр будет один
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(this);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }


    //}


    public void ChangeSceneToStart(FightLevel level)
    {
        LevelManager.sceneToStart = level.IdLevel.ToString();
       
    }




    //public void ChangeScene()
    //{
    //    SceneManager.LoadScene(sceneToStart);
    //}


}
