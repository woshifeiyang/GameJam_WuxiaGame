using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManage : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button1;
    void Start()
    {
        button1.onClick.AddListener(switchScene);
    }

    // Update is called once per frame
   void switchScene()
    {
        SceneManager.LoadScene("AndroidTest");
    }
}
