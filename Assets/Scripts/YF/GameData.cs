using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public static int language;

    private GameObject _narrativeUI;

    private GameObject _ComfirmButton;

    public AsyncOperation Operation;
    
    

    private void Awake()
    {
        _narrativeUI = GameObject.Find("NarrativeUI");
        _ComfirmButton = _narrativeUI.transform.Find("Button").gameObject;
    }

    void Start()
    {
        _narrativeUI.SetActive(false);
        _ComfirmButton.SetActive(false);
    }

    public void GetLanguage(int a)
    {
        language = a;
    }

    public void LoadSceneAsync()
    {
        _narrativeUI.SetActive(true);
        
        if (language == 1)
        {
            _narrativeUI.transform.Find("Text_English").gameObject.SetActive(false);
            _narrativeUI.transform.Find("Text_Chinese").gameObject.SetActive(true);
        }
        else
        {
            _narrativeUI.transform.Find("Text_English").gameObject.SetActive(true);
            _narrativeUI.transform.Find("Text_Chinese").gameObject.SetActive(false);
        }
        
        Operation = SceneManager.LoadSceneAsync("Main");
        
        Operation.allowSceneActivation = false;
        
        StartCoroutine(LoadSceneAsync_C());
    }

    public void AllowLoadScene()
    {
        Operation.allowSceneActivation = true;
    }
    IEnumerator LoadSceneAsync_C()
    {
        while (!Operation.isDone)
        {
            if (Operation.progress >= 0.9f)
            {
                _ComfirmButton.SetActive(true);
            }
            yield return null;
        }
    }
}