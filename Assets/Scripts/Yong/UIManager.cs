using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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

    private GameObject _evaluationUIObj;

    private GameObject _firstChooseUIObj;
    
    private GameObject _giftUIObj;

    private GameObject _phoneButtonObj;

    protected override void InitAwake()
    {
        base.InitAwake();
        _basicPropUIObj = GameObject.Find("BasicPropUI");
        _skillListUIObj = GameObject.Find("SkillListUI");
        _evaluationUIObj = GameObject.Find("EvaluationUI");
        _firstChooseUIObj = GameObject.Find("FirstChooseUI");
        _phoneButtonObj = GameObject.Find("PhoneButton");
        _giftUIObj = GameObject.Find("GiftUI");
    }

    public void InitUIManager()
    {
        _basicPropUIObj.SetActive(false);
        
        _skillListUIObj.SetActive(false);
        
        _evaluationUIObj.SetActive(false);

        _firstChooseUIObj.SetActive(false);

        _phoneButtonObj.SetActive(true);

        _giftUIObj.SetActive(false);
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

    private void ProjectilesUpgrade()
    {
        PlayerController.Instance.ProjectilesUp();
    }

    private void BallisticSpeedUpgrade()
    {
        PlayerController.Instance.SkillSpeedUp();
    }
    private void HpUpgrade()
    {
        PlayerController.Instance.HealthLevelUp();
        PlayerController.Instance.updateParameters();
    }

    private void SKillRangeUpgrade()
    {
        PlayerController.Instance.SkillRangeUp();
    }
    
    public void ShowBasicPropUI()
    {
        if (_basicPropUIObj.activeSelf) return;
        
        _basicPropUIObj.SetActive(true);
        _basicPropUIObj.GetComponent<Animator>().SetBool("isVisable", true);
        _phoneButtonObj.SetActive(false);
        
        InitBasicPropUI();
        PauseGame();
    }

    public void ShowSkillListUI()
    {
        if (_skillListUIObj.activeSelf) return;
        
        _skillListUIObj.SetActive(true);
        _skillListUIObj.GetComponent<Animator>().SetBool("isVisable", true);
        _phoneButtonObj.SetActive(false);
        
        InitSkillListUI();
        PauseGame();
    }

    public void ShowFirstChooseUI()
    {
        if (_firstChooseUIObj.activeSelf) return;
        
        Debug.Log("FirstChoose");
        _firstChooseUIObj.SetActive(true);
        _firstChooseUIObj.GetComponent<Animator>().SetBool("isVisable", true);
        _phoneButtonObj.SetActive(false);
        
        InitFirstChooseUI();
        PauseGame();
    }
    public void ShowEvaluationUI()
    {
        if (_evaluationUIObj.activeSelf) return;
        
        _evaluationUIObj.SetActive(true);
        _phoneButtonObj.SetActive(false);

        InitEvaluationUI();
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
            Image bpImage = bpButton.transform.Find(imagePath).GetComponent<Image>();
            UnityEngine.Sprite sprite = Resources.Load(list[i - 1].ResAddress, typeof(UnityEngine.Sprite)) as UnityEngine.Sprite;
            bpImage.sprite = sprite;
            TextMeshProUGUI skillNameText = bpButton.transform.Find(skillNamePath).GetComponent<TextMeshProUGUI>();
            //Text skillNameText = bpButton.transform.Find(skillNamePath).GetComponent<Text>();
            skillNameText.text = list[i - 1].KeyName;
            TextMeshProUGUI skillDesText = bpButton.transform.Find(skillDesPath).GetComponent<TextMeshProUGUI>();
            //Text skillDesText = bpButton.transform.Find(skillDesPath).GetComponent<Text>();
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
            string imagePath = "Skill_Image" + i;
            Button skillButton = _skillListUIObj.transform.Find(skillButtonPath).GetComponent<Button>();
            
            Image image = skillButton.transform.Find(imagePath).GetComponent<Image>();
            UnityEngine.Sprite sprite = Resources.Load(list[i - 1].ResAddress, typeof(UnityEngine.Sprite)) as UnityEngine.Sprite;
            image.sprite = sprite;
            Text skillNameText = skillButton.transform.Find(skillNamePath).GetComponent<Text>();
            skillNameText.text = list[i - 1].KeyName;
            Button desButton = skillButton.transform.Find(desButtonPath).GetComponent<Button>();
            InitSkillListButton(skillButton, desButton, list[i - 1]);
        }
    }

    private void InitFirstChooseUI()
    {
        Dictionary<int, SkillListJson> dictionary = new Dictionary<int, SkillListJson>();

        foreach (var skill in JsonManager.Instance.skillList)
        {
            if (skill.Id == 101) dictionary.Add(1, skill);
            else if (skill.Id == 201) dictionary.Add(2, skill);
            else if (skill.Id == 301) dictionary.Add(3, skill);
            else if (skill.Id == 401) dictionary.Add(4, skill);
        }

        for (int i = 1; i < 5; i++)
        {
            string buttonPath = "FC_Button" + i;
            Button skillButton = _firstChooseUIObj.transform.Find(buttonPath).GetComponent<Button>();
            InitFirstChooseButton(skillButton, i, dictionary[i]);
        }
    }

    private void InitEvaluationUI()
    {
        Button mainMenuButton = _evaluationUIObj.transform.Find("MainMenuButton").GetComponent<Button>();
        mainMenuButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.AddListener(() => {ChangeScene("UI");});
    }
    private void InitBasicPropButton(Button button, BasicPropJson obj)
    {
        switch (obj.Id)
        {
            case (int)BasicPropId.DamageUpgrade:
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(DamageUpgrade);
                button.onClick.AddListener(CloseBasicPropUI);
                return;
            case (int)BasicPropId.HpUpgrade:
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(HpUpgrade);
                button.onClick.AddListener(CloseBasicPropUI);
                return;
            case (int)BasicPropId.ProjectileUpgrade:
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(ProjectilesUpgrade);
                button.onClick.AddListener(CloseBasicPropUI);
                return;
            case (int)BasicPropId.AttackSpeedUpgrade:
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(AttackSpeedUpgrade);
                button.onClick.AddListener(CloseBasicPropUI);
                return;
            case (int)BasicPropId.BallisticSpeedUpgrade:
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(BallisticSpeedUpgrade);
                button.onClick.AddListener(CloseBasicPropUI);
                return;
            case (int)BasicPropId.CharacterSpeedUpgrade:
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(SpeedUpgrade);
                button.onClick.AddListener(CloseBasicPropUI);
                return;
            case (int)BasicPropId.DamageRangeUpgrade:
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(SKillRangeUpgrade);
                button.onClick.AddListener(CloseBasicPropUI);
                return;
        }
    }
    
    private void InitSkillListButton(Button skillButton, Button desButton, SkillListJson obj)
    {
        switch (obj.Category)
        {
            case "Bullet":
                skillButton.onClick.RemoveAllListeners();
                skillButton.onClick.AddListener(()=>{ChooseBulletSkill(obj);});
                skillButton.onClick.AddListener(()=>{CloseSkillListUI();});
                desButton.onClick.AddListener(()=>{ShowSkillInformation(obj);});
                return;
            case "Scope":
                skillButton.onClick.RemoveAllListeners();
                skillButton.onClick.AddListener(()=>{ChooseScopeSkill(obj);});
                skillButton.onClick.AddListener(()=>{CloseSkillListUI();});
                desButton.onClick.AddListener(()=>{ShowSkillInformation(obj);});
                return;
            case "Field":
                skillButton.onClick.RemoveAllListeners();
                skillButton.onClick.AddListener(()=>{ChooseFieldSkill(obj);});
                skillButton.onClick.AddListener(()=>{CloseSkillListUI();});
                desButton.onClick.AddListener(()=>{ShowSkillInformation(obj);});
                return;
            case "MultTarget":
                skillButton.onClick.RemoveAllListeners();
                skillButton.onClick.AddListener(()=>{ChooseMultTargetSkill(obj);});
                skillButton.onClick.AddListener(()=>{CloseSkillListUI();});
                desButton.onClick.AddListener(()=>{ShowSkillInformation(obj);});
                return;
        }
    }

    private void InitFirstChooseButton(Button skillButton, int num, SkillListJson skill)
    {
        switch (num)
        {
            case 1:
                skillButton.onClick.RemoveAllListeners();
                skillButton.onClick.AddListener(()=>{ChooseBulletSkill(skill);});
                skillButton.onClick.AddListener(()=>{CloseFirstChooseUI();});
                skillButton.onClick.AddListener(() => {EnemySpawner.Instance.SpawnEnemy();});
                return;
            case 2:
                skillButton.onClick.RemoveAllListeners();
                skillButton.onClick.AddListener(()=>{ChooseMultTargetSkill(skill);});
                skillButton.onClick.AddListener(()=>{CloseFirstChooseUI();});
                skillButton.onClick.AddListener(() => {EnemySpawner.Instance.SpawnEnemy();});
                return;
            case 3:
                skillButton.onClick.RemoveAllListeners();
                skillButton.onClick.AddListener(()=>{ChooseFieldSkill(skill);});
                skillButton.onClick.AddListener(()=>{CloseFirstChooseUI();});
                skillButton.onClick.AddListener(() => {EnemySpawner.Instance.SpawnEnemy();});
                return;
            case 4:
                skillButton.onClick.RemoveAllListeners();
                skillButton.onClick.AddListener(()=>{ChooseScopeSkill(skill);});
                skillButton.onClick.AddListener(()=>{CloseFirstChooseUI();});
                skillButton.onClick.AddListener(() => {EnemySpawner.Instance.SpawnEnemy();});
                return;
        }
    }

    private void ShowSkillInformation(SkillListJson obj)
    {
        Image descriptionPage = _skillListUIObj.transform.Find("Upgrade/Description").GetComponent<Image>();
        descriptionPage.gameObject.SetActive(true);

        TextMeshProUGUI skillDesText = _skillListUIObj.transform.Find("Upgrade/Description/SkillDescription").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI buffDesText = _skillListUIObj.transform.Find("Upgrade/Description/BuffDescription").GetComponent<TextMeshProUGUI>();

        skillDesText.text = obj.Description;
        buffDesText.text = obj.Buff;
        
    }
    private void ChooseBulletSkill(SkillListJson obj)
    {
        // 加载技能
        SkillManager.Instance.CreateBulletSkill("Prefab/Skill/Bullet/" + obj.Id, obj.Id);
        //Debug.Log("Id of skill is:" + obj.Id);
        //Debug.Log("the num of skillList is: " + JsonManager.Instance.skillList.Count);
        // 从JsonManager的SkillList删除该技能
        JsonManager.Instance.skillList.Remove(obj);
        //Debug.Log(JsonManager.Instance.skillList.Contains(obj));
        //Debug.Log("After delete the num of skillList is: " + JsonManager.Instance.skillList.Count);
    }
    
    private void ChooseScopeSkill(SkillListJson obj)
    {
        // 加载技能
        SkillManager.Instance.CreateScopeSkill("Prefab/Skill/Scope/" + obj.Id, obj.Id);
        // 从JsonManager的SkillList删除该技能
        JsonManager.Instance.skillList.Remove(obj);
    }
    
    private void ChooseFieldSkill(SkillListJson obj)
    {
        // 加载技能
        SkillManager.Instance.CreateFieldSkill("Prefab/Skill/Field/" + obj.Id, obj.Id);
        // 从JsonManager的SkillList删除该技能
        JsonManager.Instance.skillList.Remove(obj);
    }
    
    private void ChooseMultTargetSkill(SkillListJson obj)
    {
        // 加载技能
        SkillManager.Instance.CreateMultTargetSkill("Prefab/Skill/MultTarget/" + obj.Id, obj.Id);
        // 从JsonManager的SkillList删除该技能
        JsonManager.Instance.skillList.Remove(obj);
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

    public void CloseFirstChooseUI()
    {
        StartCoroutine(CloseFirstChooseUI_C());
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

    IEnumerator CloseFirstChooseUI_C()
    {
        _firstChooseUIObj.GetComponent<Animator>().SetBool("isVisable", false);
        _phoneButtonObj.SetActive(true);
        RestartGame();
        yield return new WaitForSeconds(0.5f);
        _firstChooseUIObj.SetActive(false);
        StopCoroutine(nameof(CloseFirstChooseUI_C));
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
