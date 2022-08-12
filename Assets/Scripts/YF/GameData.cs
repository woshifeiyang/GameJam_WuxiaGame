using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static int language;

    private GameObject _narrativeUI;

    private void Awake()
    {
        _narrativeUI = GameObject.Find("NarrativeUI");
    }

    void Start()
    {
        _narrativeUI.SetActive(false);
    }

    public void GetLanguage(int a)
    {
        language = a;
    }
}