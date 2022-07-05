using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    private GameObject _basicPropUIObj;
    
    private GameObject _giftUIObj;

    private GameObject _phoneButtonObj;
    
    // BasicPropUI
    private Button _bpButton1;
    private Button _bpButton2;
    private Button _bpButton3;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("BasicPropUI"))
        {
            _basicPropUIObj = GameObject.Find("BasicPropUI");
            _basicPropUIObj.SetActive(false);
            
            _bpButton1 = _basicPropUIObj.transform.Find("Upgrade/BP_Button1").GetComponent<Button>();
            _bpButton2 = _basicPropUIObj.transform.Find("Upgrade/BP_Button2").GetComponent<Button>();
            _bpButton3 = _basicPropUIObj.transform.Find("Upgrade/BP_Button3").GetComponent<Button>();
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

    public void ShowBasicPropUI()
    {
        _basicPropUIObj.SetActive(true);
        _basicPropUIObj.GetComponent<Animator>().SetBool("isVisable", true);
        _phoneButtonObj.SetActive(false);
        
        InitBasicPropUI();
        
        PauseGame();
    }

    private void InitBasicPropUI()
    {
        List<BasicPropJson> list = EnemyDetector.GetRandomElements(JsonManager.Instance.basicPropList, 3);
        for (int i = 1; i <= list.Count; i++)
        {
            string buttonPath = "Upgrade/BP_Button" + i;
            string imagePath = "BP_Image" + i;
            string skillNamePath = "BP_SkillName" + i;
            string skillDesPath = "BP_SkillDes" + i;
            Button bpButton = _basicPropUIObj.transform.Find(buttonPath).GetComponent<Button>();
            Image bpImage = bpButton.transform.Find(imagePath).GetComponent<Image>();
            Text skillNameText = bpButton.transform.Find(skillNamePath).GetComponent<Text>();
            skillNameText.text = list[i - 1].KeyName;
            Text skillDesText = bpButton.transform.Find(skillDesPath).GetComponent<Text>();
            skillDesText.text = list[i - 1].Description + " " + list[i - 1].Value;
        }
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
        _basicPropUIObj.GetComponent<Animator>().SetBool("isVisable", false);
        _phoneButtonObj.SetActive(true);
        RestartGame();
        yield return new WaitForSeconds(0.5f);
        _basicPropUIObj.SetActive(false);
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
