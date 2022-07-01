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

    private float _curExperience;

    private float _totalExperience;

    private int _level;
    
    private MMProgressBar _mmProgressBar;

    private MMProgressBar _expBar;

    public Transform skill;

    // player parameters
    // speed
    public float moveSpeed = 1f;
    public float moveSpeedLevelUpFactor = 0.3f;
    public int moveSpeedLevel;
    public float moveSpeedFinal;

    // skillcd
    public float skillCd = 1f;
    public float skillCdLevelUpFactor = 0.85f;
    public int skillCdLevel;
    public float skillCdFinal;

    public float curHealth;
    
    // max health
    public float maxHealth = 20f;
    public float maxHealthLevelUpFactor = 5f;
    public int maxHealthLevel;
    public float maxHealthFinal;
    
    // attack
    public float maxAttackLevelUpFactor = 5f;
    public int attackLevel;
    public float attackFinal;
    
    // import manager objects
    public SpriteManager spriteManager;

    private Vector3 _importedLocalScale;

    private GameObject _floatingJoystick;

    public GameObject detector;
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
        detector = transform.Find("EnemyDetector").gameObject;
        
        string assertPath = "Prefab/Skill/Bullet/102";
        
        SkillManager.Instance.CreateBulletSkill(assertPath, 102, gameObject);
        
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
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * moveSpeedFinal * Time.fixedDeltaTime);

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
            if(curHealth - col.gameObject.GetComponent<Monster>().damage > 0.0f)
            {
                curHealth -= col.gameObject.GetComponent<Monster>().damage;
                _mmProgressBar.UpdateBar01(curHealth / maxHealth);
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
        //
        // //skillCd;
        // float tempcoolDownReduce = spriteManager.spriteManagerProperty["coolDownReduce"] == 0 ? 1 : spriteManager.spriteManagerProperty["coolDownReduce"];
        // skillCdFinal = (skillCd * Mathf.Pow(skillCdLevelUpFactor, skillCdLevel)) * (1 - tempcoolDownReduce);
        //
        // //maxHealth;
        // float tempcoolhealthBonus = spriteManager.spriteManagerProperty["healthBonus"] == 0 ? 1 : spriteManager.spriteManagerProperty["healthBonus"];
        // maxHealthFinal = (maxHealth + (maxHealthLevelUpFactor * maxHealthLevel)) * tempcoolhealthBonus;
        
        moveSpeedFinal = (moveSpeed + (moveSpeedLevel * moveSpeedLevelUpFactor));
        
        //skillCd;
        skillCdFinal = (skillCd * Mathf.Pow(skillCdLevelUpFactor, skillCdLevel));

        //maxHealth;
        maxHealthFinal = (maxHealth + (maxHealthLevelUpFactor * maxHealthLevel));
        
        //attack
        attackFinal = skill.gameObject.GetComponent<Skill101>().damage + maxAttackLevelUpFactor * attackLevel;
        
        Debug.Log("moveSpeedFinal: " + moveSpeedFinal);
        Debug.Log("skillCdFinal: " + skillCdFinal);
        Debug.Log("maxHealthFinal: " + maxHealthFinal);
    }
}
