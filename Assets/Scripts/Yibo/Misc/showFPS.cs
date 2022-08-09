using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class showFPS : MonoBehaviour {
    public Text fpsText;
    public float deltaTime;

    public float timeGap = 0.5f;
    private float tempTime;
    private float timer = 0;
    //void Update () 
    //{
    //    deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    //    float fps = 1.0f / deltaTime;

    //    tempTime += Time.deltaTime;
    //    if (tempTime > timeGap)
    //    {
    //        fpsText.text = "FPS:"+Mathf.Ceil(fps).ToString();
    //        tempTime = 0f;
    //    }
    //}

    private void Update()
    {
        timer += Time.deltaTime;
        fpsText.text = Mathf.Ceil(timer).ToString();
    }
}
