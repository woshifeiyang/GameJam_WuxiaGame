using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager UIManagerInstance;

    private GameObject _rogueUIObj;
    
    private GameObject _giftUIObj;

    private GameObject _phoneButtonObj;

    private void Awake()
    {
        UIManagerInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rogueUIObj = GameObject.Find("RogueUI");
        _rogueUIObj.SetActive(false);
        
        _phoneButtonObj = GameObject.Find("PhoneButton");
        _phoneButtonObj.SetActive(true);

        _giftUIObj = GameObject.Find("GiftUI");
        _giftUIObj.SetActive(false);
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
        _rogueUIObj.SetActive(true);
        _rogueUIObj.GetComponent<Animator>().SetBool("isVisable", true);
        _phoneButtonObj.SetActive(false);
        PauseGame();
    }

    public void ShowGiftUI()
    {
        _giftUIObj.SetActive(true);
        _giftUIObj.GetComponent<Animator>().SetBool("isVisable", true);
        _phoneButtonObj.SetActive(false);
        PauseGame();
    }

    public void CloseRogueUI()
    {
        StartCoroutine(CloseRogueUI_C());
    }

    public void CloseGiftUI()
    {
        StartCoroutine(CloseGiftUI_C());
    }
    
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator CloseRogueUI_C()
    {
        _rogueUIObj.GetComponent<Animator>().SetBool("isVisable", false);
        _phoneButtonObj.SetActive(true);
        RestartGame();
        yield return new WaitForSeconds(0.5f);
        _rogueUIObj.SetActive(false);
        StopCoroutine(nameof(CloseRogueUI_C));
    }
    
    IEnumerator CloseGiftUI_C()
    {
        _giftUIObj.GetComponent<Animator>().SetBool("isVisable", false);
        _phoneButtonObj.SetActive(true);
        RestartGame();
        yield return new WaitForSeconds(0.5f);
        _giftUIObj.SetActive(false);
        StopCoroutine(nameof(CloseGiftUI_C));
    }
}
