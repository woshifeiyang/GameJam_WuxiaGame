using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoSingleton<UIManager>
{
    private GameObject _rogueUIObj;
    
    private GameObject _giftUIObj;

    private GameObject _phoneButtonObj;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("RogueUI"))
        {
            _rogueUIObj = GameObject.Find("RogueUI");
            _rogueUIObj.SetActive(false);
        }

        if (GameObject.Find("PhoneButton"))
        {
            _phoneButtonObj = GameObject.Find("PhoneButton");
            _phoneButtonObj.SetActive(true);
        }

        if (GameObject.Find("GiftUI"))
        {
            _giftUIObj = GameObject.Find("GiftUI");
            _giftUIObj.SetActive(false);
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
    
    public void AttackUp()
    {
        PlayerController.Instance.AttackLevelUp();
        PlayerController.Instance.updateParameters();
    }
    
    public void CdDown()
    {
        PlayerController.Instance.SkillCdUp();
        PlayerController.Instance.updateParameters();
    }
    
    public void SpeedUp()
    {
        PlayerController.Instance.MoveSpeedLevelUp();
        PlayerController.Instance.updateParameters();
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
