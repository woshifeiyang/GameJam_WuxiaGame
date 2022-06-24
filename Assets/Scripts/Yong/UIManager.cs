using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager UIManagerInstance;

    private GameObject rogueUIGameObject;

    private GameObject phoneButtonObject;

    private void Awake()
    {
        UIManagerInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("RogueUI"))
        {
            rogueUIGameObject = GameObject.Find("RogueUI");
            rogueUIGameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Can not find RogueUI, please check if the name of object is right");
        }

        if (GameObject.Find("PhoneButton"))
        {
            phoneButtonObject = GameObject.Find("PhoneButton");
            phoneButtonObject.SetActive(true);
        }
        else
        {
            Debug.Log("Can not find PhoneButton, please check if the name of object is right");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
    }
    
    public void HpUp()
    {
        ++PlayerController.PlayerControllerInstance.maxHealthLevel;
        PlayerController.PlayerControllerInstance.curHealth += PlayerController.PlayerControllerInstance.maxHealthLevelUpFactor;
        PlayerController.PlayerControllerInstance.updateParameters();
    }
    
    public void CdDown()
    {
        PlayerController.PlayerControllerInstance.skillCdLevel++;
        PlayerController.PlayerControllerInstance.updateParameters();
    }
    
    public void SpeedUp()
    {
        PlayerController.PlayerControllerInstance.moveSpeedLevel++;
        PlayerController.PlayerControllerInstance.updateParameters();
    }

    public void ShowRogueUI()
    {
        rogueUIGameObject.SetActive(true);
        rogueUIGameObject.GetComponent<Animator>().SetBool("isVisable", true);
        phoneButtonObject.SetActive(false);
        PauseGame();
    }

    public void CloseRogueUI()
    {
        StartCoroutine(CloseRogueUI_C());
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator CloseRogueUI_C()
    {
        rogueUIGameObject.GetComponent<Animator>().SetBool("isVisable", false);
        phoneButtonObject.SetActive(true);
        RestartGame();
        yield return new WaitForSeconds(0.5f);
        rogueUIGameObject.SetActive(false);
        StopCoroutine(nameof(CloseRogueUI_C));
    }
}
