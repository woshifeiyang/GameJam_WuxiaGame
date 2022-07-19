using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    private Rigidbody2D _rb;

    private Animator _anim;

    private Vector2 _movement;
   
    private MMProgressBar _mmProgressBar;

    private MMProgressBar _expBar;
    
    public MMFeedbacks PlayerDamageFeedback;

    public bool canMove = true;

    public Vector2 lastVelocity;

    // player parameters
    // experience
    private float _curExperience;
    private float _totalExperience;
    private int _level = 1;
    
    // moveSpeed
    public float moveSpeed = 1f;
    public float moveSpeedRatio = 0.3f;
    private int _moveSpeedLevel = 0;
    public float _moveSpeedFinal;

    // skillCd
    public float skillCdRatio = 0.85f;
    private int _skillCdLevel = 0;
    private float _skillCdFinal;

    // health
    public float curHealth;
    public float maxHealth = 20f;
    public float healthRatio = 5f;
    private int _healthLevel = 0;
    private float _healthFinal;
    
    // attack
    public float attackRatio = 5.0f;
    private int _attackLevel = 0;

    // Projectiles
    private int _projectileLevel = 0;
    public int projectileRatio;

    // SkillRange
    private float _skillRangeLevel = 0;
    public float skillRangeRatio = 0.5f;
    
    // Ballistic Speed
    private int _skillSpeedLevel = 0;
    public float skillSpeedRatio = 40.0f;

    // import manager objects
    public SpriteManager spriteManager;

    private Vector3 _importedLocalScale;

    private GameObject _floatingJoystick;

    private FloatingJoystick floatingJoystick;
    protected override void InitAwake()
    {
        base.InitAwake();
        Application.targetFrameRate = 90;
        _floatingJoystick = GameObject.Find("FloatingJoystick");

        // get sprite manager
        spriteManager = GameObject.Find("SpriteManager").GetComponent<SpriteManager>();

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

        floatingJoystick = _floatingJoystick.GetComponent<FloatingJoystick>();
        canMove = true;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_movement.x > 0)
        {
            transform.localScale = new Vector3(1 * _importedLocalScale.x, _importedLocalScale.y, _importedLocalScale.z);
        }
        else if (_movement.x < 0)
        {
            transform.localScale = new Vector3(-1 * _importedLocalScale.x, _importedLocalScale.y, _importedLocalScale.z);
        }

        SwitchAnim();
        
        _mmProgressBar.UpdateBar01(Mathf.Clamp(curHealth / _healthFinal, 0f, 1f));
        _expBar.UpdateBar01(Mathf.Clamp(_curExperience / _totalExperience, 0f, 1f));

    }

    private void FixedUpdate()
    {
        lastVelocity = _rb.velocity;
        
        _movement = floatingJoystick.Direction.normalized;
        if (canMove)
        {
            _rb.MovePosition(_rb.position + _movement * _moveSpeedFinal * Time.deltaTime);
        }
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
        if ((col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("EliteEnemy") || col.gameObject.CompareTag("Boss"))
            && col.gameObject.GetComponent<Monster>().isDead == false)
        {
            if (curHealth - col.gameObject.GetComponent<Monster>().damage > 0.0f)
            {
                curHealth -= col.gameObject.GetComponent<Monster>().damage;
                PlayerDamageFeedback?.PlayFeedbacks();
            }
            else
            {
                EventListener.Instance.SendMessage(EventListener.MessageEvent.Message_GameOver);
            }
        }
    }

    public void GetDamaged(float damageAmount)
    {
        if (curHealth - damageAmount > 0.0f)
        {
            curHealth -= damageAmount;
            PlayerDamageFeedback?.PlayFeedbacks();
        }
        else
        {
            EventListener.Instance.SendMessage(EventListener.MessageEvent.Message_GameOver);
        }
    }

    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }

    public void LevelUp()
    {
        ++_level;
        _curExperience = 0;
        _totalExperience += 5;
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
        if (curHealth < maxHealth + (healthRatio * _healthLevel))
        {
            curHealth += healthRatio;
        }
    }

    public void SkillSpeedUp()
    {
        ++_skillSpeedLevel;
    }
    public void ProjectilesUp()
    {
        ++_projectileLevel;
    }
    public void SkillRangeUp()
    {
        ++_skillRangeLevel;
    }

    public void AttackSpeedUpgrade()
    {
        ++_skillCdLevel;
    }
    public void IncreaseExperience()
    {
        if (_totalExperience - _curExperience <= 1.0f)
        {
            EventListener.Instance.SendMessage(EventListener.MessageEvent.Message_BasicPropLevelUp);
        }
        else if (_curExperience < _totalExperience)
        {
            ++_curExperience;
        }
    }

    public float GetPlayerAttack()
    {
        return _attackLevel * attackRatio;
    }

    public float GetPlayerSkillCd()
    {
        return (float)Math.Pow(skillCdRatio, _skillCdLevel);
    }

    public float GetPlayerSkillSpeed()
    {
        return _skillSpeedLevel * skillSpeedRatio;
    }

    public float GetPlayerSkillRange()
    {
        return _skillRangeLevel * skillRangeRatio;
    }

    public int GetPlayerProjectileNum()
    {
        return _projectileLevel * projectileRatio;
    }

    public float GetPlayerHealthFinal()
    {
        return _healthFinal;
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
    
    // update parameters to the value recorder variables
    public void updateParameters()
    {
        // moveSpeed;
        _moveSpeedFinal = moveSpeed + (_moveSpeedLevel * moveSpeedRatio);
        // skillCd;
        _skillCdFinal = Mathf.Pow(skillCdRatio, _skillCdLevel);
        // Health;
        _healthFinal = maxHealth + (healthRatio * _healthLevel);

        //Debug.Log("moveSpeedLevel: " + _moveSpeedLevel);
        //Debug.Log("skillCdLevel: " + _skillCdLevel);
        //Debug.Log("HealthLevel: " + _healthLevel);
    }
}
