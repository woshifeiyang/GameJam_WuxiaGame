using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static int language;

    void Start() {
        GameObject.DontDestroyOnLoad(gameObject);
    }

    public void GetLanguage(int a)
    {
        language = a;
    }
}