using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BasicPropId
{
    DamageUpgrade = 1,
    HpUpgrade = 2,
    CharacterSpeedUpgrade = 3,
    AttackSpeedUpgrade = 4,
    ProjectileUpgrade = 5,
    BallisticSpeedUpgrade = 6,
    DamageRangeUpgrade = 7
}
public class UIManager : MonoSingleton<UIManager>
{
    private GameObject _basicPropUIObj;

    private GameObject _skillListUIObj;
    
    private GameObject _giftUIObj;

    private GameObject _phoneButtonObj;
    
    private  BasicPropId basicPropId;
    
    // BasicPropUI
    private Button _bpButton1;
    private Button _bpButton2;
    private Button _bpButton3;
    
    // SkillListUI
    private Button _skillButton1;
    private Button _skillButton2;
    private Button _skillButton3;

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
        
        if (GameObject.Find("SkillListUI"))
        {
            _skillListUIObj = GameObject.Find("SkillListUI");
            _skillListUIObj.SetActive(false);
            Image image = _skillListUIObj.transform.Find("Upgrade/Description").GetComponent<Image>();
            image.gameObject.SetActive(false);
            
            _skillButton1 = _skillListUIObj.transform.Find("Upgrade/Skill_Button1").GetComponent<Button>();
            _skillButton2 = _skillListUIObj.transform.Find("Upgrade/Skill_Button2").GetComponent<Button>();
            _skillButton3 = _skillListUIObj.transform.Find("Upgrade/Skill_Button3").GetComponent<Button>();
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
    
    private void DamageUpgrade()
    {
        PlayerController.Instance.AttackLevelUp();
        PlayerController.Instance.updateParameters();
    }
    
    private void AttackSpeedUpgrade()
    {
        PlayerController.Instance.AttackSpeedUpgrade();
        PlayerController.Instance.updateParameters();
    }
    
    private void SpeedUpgrade()
    {
        PlayerController.Instance.MoveSpeedLevelUp();
        PlayerController.Instance.updateParameters();
    }

    private void HpUpgrade()
    {
        PlayerController.Instance.HealthLevelUp();
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

    public void ShowSkillListUI()
    {
        _skillListUIObj.SetActive(true);
        _skillListUIObj.GetComponent<Animator>().SetBool("isVisable", true);
        _phoneButtonObj.SetActive(false);
        
        InitSkillListUI();
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
            InitBasicPropButton(bpButton, list[i - 1], basicPropId);
            //Image bpImage = bpButton.transform.Find(imagePath).GetComponent<Image>();
            Text skillNameText = bpButton.transform.Find(skillNamePath).GetComponent<Text>();
            skillNameText.text = list[i - 1].KeyName;
            Text skillDesText = bpButton.transform.Find(skillDesPath).GetComponent<Text>();
            skillDesText.text = list[i - 1].Description + " " + list[i - 1].Value;
        }
    }

    private void InitSkillListUI()
    {
        List<SkillListJson> list = EnemyDetector.GetRandomElements(JsonManager.Instance.skillList, 3);
        for (int i = 1; i <= list.Count; i++)
        {
            string skillButtonPath = "Upgrade/Skill_Button" + i;
            string skillNamePath = "Skill_Name" + i;
            string desButtonPath = "Des_Button" + i;
            Button skillButton = _skillListUIObj.transform.Find(skillButtonPath).GetComponent<Button>();
            
            Text skillNameText = skillButton.transform.Find(skillNamePath).GetComponent<Text>();
            skillNameText.text = list[i - 1].KeyName;
            Button desButton = skillButton.transform.Find(desButtonPath).GetComponent<Button>();
            InitSkillListButton(skillButton, desButton, list[i - 1]);
        }
    }
    private void InitBasicPropButton(Button button, BasicPropJson obj, BasicPropId id)
    {
        switch (obj.Id)
        {
            case (int)BasicPropId.DamageUpgrade:
                button.onClick.AddListener(DamageUpgrade);
                button.onClick.AddListener(CloseBasicPropUI);
                return;
            case (int)BasicPropId.HpUpgrade:
                button.onClick.AddListener(HpUpgrade);
                button.onClick.AddListener(CloseBasicPropUI);
                return;
            case (int)BasicPropId.ProjectileUpgrade:
                button.onClick.AddListener(CloseBasicPropUI);
                return;
            case (int)BasicPropId.AttackSpeedUpgrade:
                button.onClick.AddListener(AttackSpeedUpgrade);
                button.onClick.AddListener(CloseBasicPropUI);
                return;
            case (int)BasicPropId.BallisticSpeedUpgrade:
                button.onClick.AddListener(CloseBasicPropUI);
                return;
            case (int)BasicPropId.CharacterSpeedUpgrade:
                button.onClick.AddListener(SpeedUpgrade);
                button.onClick.AddListener(CloseBasicPropUI);
                return;
            case (int)BasicPropId.DamageRangeUpgrade:
                button.onClick.AddListener(CloseBasicPropUI);
                return;
        }
    }
    
    private void InitSkillListButton(Button skillButton, Button desButton, SkillListJson obj)
    {
        switch (obj.Category)
        {
            case "Bullet":
                SkillManager.Instance.CreateBulletSkill("Prefab/Skill/Bullet/" + obj.Id, obj.Id);
                return;
            case "Scope":
                return;
            case "Field":
                return;
            case "MultTarget":
                return;
        }
    }
    
    
    public void ShowGiftUI()
    {
        _giftUIObj.SetActive(true);
        _giftUIObj.GetComponent<Animator>().SetBool("isVisable", true);
        _phoneButtonObj.SetActive(false);
        PauseGame();
    }

    public void CloseBasicPropUI()
    {
        StartCoroutine(CloseBasicPropUI_C());
    }

    public void CloseGiftUI()
    {
        StartCoroutine(CloseGiftUI_C());
    }
    
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator CloseBasicPropUI_C()
    {
        _basicPropUIObj.GetComponent<Animator>().SetBool("isVisable", false);
        _phoneButtonObj.SetActive(true);
        RestartGame();
        yield return new WaitForSeconds(0.5f);
        _basicPropUIObj.SetActive(false);
        StopCoroutine(nameof(CloseBasicPropUI_C));
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
