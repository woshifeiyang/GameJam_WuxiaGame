using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public int language;

    void Start() {
        GameObject.DontDestroyOnLoad(gameObject);
    }

    public void getlanguage(int a)
    {
        language = a;
    }
}