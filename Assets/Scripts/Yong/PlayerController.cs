using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    private Rigidbody2D _rb;

    private Animator _anim;

    private Vector2 _movement;
   
    private MMProgressBar _mmProgressBar;

    private MMProgressBar _expBar;

    public Transform skill;

    public static bool attackByEnemy = false;

    // player parameters
    // experience
    private float _curExperience;
    private float _totalExperience;
    private int _level;
    
    // moveSpeed
    public float moveSpeed = 1f;
    public float moveSpeedRatio = 0.3f;
    private int _moveSpeedLevel = 1;
    private float _moveSpeedFinal;

    // skillCd
    public float skillCd = 1f;
    public float skillCdRatio = 0.85f;
    private int _skillCdLevel = 1;
    private float _skillCdFinal;

    // health
    public float curHealth;
    public float maxHealth = 20f;
    public float healthRatio = 5f;
    private int _healthLevel = 1;
    private float _healthFinal;
    
    // attack
    public float curAttack;
    public float attackRatio = 5.0f;
    private int _attackLevel = 1;

    // import manager objects
    public SpriteManager spriteManager;

    private Vector3 _importedLocalScale;

    private GameObject _floatingJoystick;
    
    protected override void InitAwake()
    {
        base.InitAwake();
        _floatingJoystick = GameObject.Find("FloatingJoystick");

        // get sprite manager
        // spriteManager = GameObject.Find("SpriteManager").GetComponent<SpriteManager>();
        _mmProgressBar = GameObject.Find("HPBar").GetComponent<MMProgressBar>();
        _expBar = GameObject.Find("ExpBar").GetComponent<MMProgressBar>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _totalExperience = 5;
        _importedLocalScale = this.transform.localScale;
        
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        string assertPath = "Prefab/Skill/Bullet/402";
        string assertPath1 = "Prefab/Skill201";
        string assertPath2 = "Prefab/Skill202";
        SkillManager.Instance.CreateBulletSkill(assertPath, 402, gameObject);
        SkillManager.Instance.CreateMultTargetSkill(assertPath1, 201);
        SkillManager.Instance.CreateScopeSkill(assertPath2, 202);


    }

    // Update is called once per frame
    void Update()
    {
        if (_movement.x != 0)
        {
            transform.localScale = new Vector3(_movement.x * _importedLocalScale.x, _importedLocalScale.y, _importedLocalScale.z);
        }
        SwitchAnim();
        
        _mmProgressBar.UpdateBar01(Mathf.Clamp(curHealth / maxHealth, 0f, 1f));
        _expBar.UpdateBar01(Mathf.Clamp(_curExperience / _totalExperience, 0f, 1f));
        _movement = _floatingJoystick.GetComponent<FloatingJoystick>().Direction;
        _rb.MovePosition(_rb.position + _movement * _moveSpeedFinal * Time.deltaTime);
    }

    private void SwitchAnim()
    {
        _anim.SetFloat("speed", _movement.magnitude);
        if (curHealth <= 0.0f)
        {
            _anim.SetBool("isDead", true);
        }
    }
    // 被怪物攻击
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy") && col.gameObject.GetComponent<Monster>().isDead == false)
        {
            Debug.Log("main character was attacked by enemy");
            attackByEnemy = true;
            if (curHealth - col.gameObject.GetComponent<Monster>().damage > 0.0f)
            {
                curHealth -= col.gameObject.GetComponent<Monster>().damage;
            }
            else
            {
                ExitGame();
            }
        }
    }

    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }

    public void AttackLevelUp()
    {
        ++_attackLevel;
    }
    
    public void MoveSpeedLevelUp()
    {
        ++_moveSpeedLevel;
    }
    
    public void HealthLevelUp()
    {
        ++_healthLevel;
    }

    public void SkillCdUp()
    {
        ++_skillCdLevel;
    }
    public void IncreaseExperience()
    {
        if (_totalExperience - _curExperience < 0.1f)
        {
            ++_level;
            _curExperience = 0;
            _totalExperience += 5;
            UIManager.Instance.ShowRogueUI();
        }else if (_curExperience < _totalExperience)
        {
            ++_curExperience;
        }
    }

    public float GetPlayerAttack()
    {
        return curAttack + _attackLevel * attackRatio;
    }

    public float GetPlayerSkillCd()
    {
        return _skillCdLevel * skillCdRatio;
    }
    
    private void ExitGame()
    {
        //预处理
    #if UNITY_EDITOR    //在编辑器模式下
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    public void updateParameters()
    {
        // update parameters to the value recorder variables
        // moveSpeed;
        // float tempMovingSpeedBonus = spriteManager.spriteManagerProperty["movingSpeedBonus"] == 0 ? 1 : spriteManager.spriteManagerProperty["movingSpeedBonus"];
        // moveSpeedFinal = (moveSpeed + (moveSpeedLevel * moveSpeedLevelUpFactor)) * tempMovingSpeedBonus;
        //
        // //skillCd;
        // float tempcoolDownReduce = spriteManager.spriteManagerProperty["coolDownReduce"] == 0 ? 1 : spriteManager.spriteManagerProperty["coolDownReduce"];
        // skillCdFinal = (skillCd * Mathf.Pow(skillCdLevelUpFactor, skillCdLevel)) * (1 - tempcoolDownReduce);
        //
        // //maxHealth;
        // float tempcoolhealthBonus = spriteManager.spriteManagerProperty["healthBonus"] == 0 ? 1 : spriteManager.spriteManagerProperty["healthBonus"];
        // maxHealthFinal = (maxHealth + (maxHealthLevelUpFactor * maxHealthLevel)) * tempcoolhealthBonus;
        
        _moveSpeedFinal = (moveSpeed + (_moveSpeedLevel * moveSpeedRatio));
        
        //skillCd;
        _skillCdFinal = (skillCd * Mathf.Pow(skillCdRatio, _skillCdLevel));

        //Health;
        _healthFinal = (maxHealth + (healthRatio * _healthLevel));

        Debug.Log("moveSpeedFinal: " + _moveSpeedFinal);
        Debug.Log("skillCdFinal: " + _skillCdFinal);
        Debug.Log("maxHealthFinal: " + _healthFinal);
    }
}
