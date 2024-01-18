using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelPlayerPrefs : MonoBehaviour
{
    
    public void DelPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
