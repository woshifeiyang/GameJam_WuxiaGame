using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void change1() {
        SceneManager.LoadScene("UI");
    }


    public void change2()
    {
        SceneManager.LoadScene("UI_test");
    }
}
