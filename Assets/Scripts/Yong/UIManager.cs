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

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("BasicPropUI"))
        {
            _basicPropUIObj = GameObject.Find("BasicPropUI");
            _basicPropUIObj.SetActive(false);
        }
        
        if (GameObject.Find("SkillListUI"))
        {
            _skillListUIObj = GameObject.Find("SkillListUI");
            _skillListUIObj.SetActive(false);
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
            InitBasicPropButton(bpButton, list[i - 1]);
            //Image bpImage = bpButton.transform.Find(imagePath).GetComponent<Image>();
            Text skillNameText = bpButton.transform.Find(skillNamePath).GetComponent<Text>();
            skillNameText.text = list[i - 1].KeyName;
            Text skillDesText = bpButton.transform.Find(skillDesPath).GetComponent<Text>();
            skillDesText.text = list[i - 1].Description + " " + list[i - 1].Value;
        }
    }

    private void InitSkillListUI()
    {
        Image descriptionPage = _skillListUIObj.transform.Find("Upgrade/Description").GetComponent<Image>();
        descriptionPage.gameObject.SetActive(false); 
        
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
    private void InitBasicPropButton(Button button, BasicPropJson obj)
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
                skillButton.onClick.AddListener(()=>{ChooseBulletSkill(obj);});
                desButton.onClick.AddListener(()=>{ShowSkillInformation(obj);});
                return;
            case "Scope":
                skillButton.onClick.AddListener(()=>{ChooseScopeSkill(obj);});
                desButton.onClick.AddListener(()=>{ShowSkillInformation(obj);});
                return;
            case "Field":
                skillButton.onClick.AddListener(()=>{ChooseFieldSkill(obj);});
                desButton.onClick.AddListener(()=>{ShowSkillInformation(obj);});
                return;
            case "MultTarget":
                skillButton.onClick.AddListener(()=>{ChooseMultTargetSkill(obj);});
                desButton.onClick.AddListener(()=>{ShowSkillInformation(obj);});
                return;
        }
    }

    private void ShowSkillInformation(SkillListJson obj)
    {
        Image descriptionPage = _skillListUIObj.transform.Find("Upgrade/Description").GetComponent<Image>();
        descriptionPage.gameObject.SetActive(true); 
        
        Text skillDesText = _skillListUIObj.transform.Find("Upgrade/Description/SkillDescription").GetComponent<Text>();
        Text buffDesText = _skillListUIObj.transform.Find("Upgrade/Description/BuffDescription").GetComponent<Text>();

        skillDesText.text = obj.Description;
        buffDesText.text = obj.Buff;
        
    }
    private void ChooseBulletSkill(SkillListJson obj)
    {
        // 加载技能
        SkillManager.Instance.CreateBulletSkill("Prefab/Skill/Bullet/" + obj.Id, obj.Id);
        // 从JsonManager的SkillList删除该技能
        JsonManager.Instance.skillList.Remove(obj);
        CloseSkillListUI();
    }
    
    private void ChooseScopeSkill(SkillListJson obj)
    {
        // 加载技能
        SkillManager.Instance.CreateScopeSkill("Prefab/Skill/Scope/" + obj.Id, obj.Id);
        // 从JsonManager的SkillList删除该技能
        JsonManager.Instance.skillList.Remove(obj);
        CloseSkillListUI();
    }
    
    private void ChooseFieldSkill(SkillListJson obj)
    {
        // 加载技能
        SkillManager.Instance.CreateFieldSkill("Prefab/Skill/Field/" + obj.Id, obj.Id);
        // 从JsonManager的SkillList删除该技能
        JsonManager.Instance.skillList.Remove(obj);
        CloseSkillListUI();
    }
    
    private void ChooseMultTargetSkill(SkillListJson obj)
    {
        // 加载技能
        SkillManager.Instance.CreateMultTargetSkill("Prefab/Skill/MultTarget/" + obj.Id, obj.Id);
        // 从JsonManager的SkillList删除该技能
        JsonManager.Instance.skillList.Remove(obj);
        CloseSkillListUI();
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

    public void CloseSkillListUI()
    {
        StartCoroutine(CloseSkillListUI_C());
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
    
    IEnumerator CloseSkillListUI_C()
    {
        _skillListUIObj.GetComponent<Animator>().SetBool("isVisable", false);
        _phoneButtonObj.SetActive(true);
        RestartGame();
        yield return new WaitForSeconds(0.5f);
        _skillListUIObj.SetActive(false);
        StopCoroutine(nameof(CloseSkillListUI_C));
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
