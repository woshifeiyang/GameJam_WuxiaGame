using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class showFPS : MonoBehaviour {
    public Text fpsText;
    public float deltaTime;

    public float timeGap = 0.5f;
    private float tempTime;
    void Update () {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        tempTime += Time.deltaTime;
        if (tempTime > timeGap)
        {
            fpsText.text = Mathf.Ceil(fps).ToString();
            tempTime = 0f;
        }
    }
}
