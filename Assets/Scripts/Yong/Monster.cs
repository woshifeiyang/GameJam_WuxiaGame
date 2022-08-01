using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public string monsterId;
    
    public float health;

    public float moveSpeed;

    public float damage;

    private float _moveSpeed;
    
    public bool isDead;
    //怪物是否能够移动
    public bool canMove; 

    public string poolBelongTo = null;

    public Animation deathAnimation;

    public DamagePopupManager DamagePopupManager;


    private Rigidbody2D _rb;

    private Animator _anim;
    
    private Vector3 _importedLocalScale;

    private float _distance;

    private SpriteRenderer _spriteRenderer;

    private Material _material;

    private void OnEnable()
    {
        SetAlive();
        _material.DisableKeyword("_BEATTACK");
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1);
    }
    // 如果怪物在摄像机内可见，添加进敌人探测器的可视怪物列表中
    private void OnBecameVisible()
    {
        EnemyDetector.Instance.enemyList.Add(gameObject);
    }
    
    private void OnBecameInvisible()
    {
        if(gameObject & EnemyDetector.Instance.enemyList.Contains(gameObject))
            EnemyDetector.Instance.enemyList.Remove(gameObject);
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _material = _spriteRenderer.material;
    }

    void Start()
    {
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1);
        DamagePopupManager = GameObject.FindWithTag("DamagePopupManager").GetComponent<DamagePopupManager>();
        
        _importedLocalScale = transform.localScale;
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        
        SetMoveSpeed();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        Vector3 playerPosition = PlayerController.Instance.GetPlayerPosition();
        if (canMove)
        {
            _rb.MovePosition(transform.position + (playerPosition - transform.position).normalized * Time.fixedDeltaTime * _moveSpeed );
        }

        if (transform.position.x - playerPosition.x > 0)
        {
            transform.localScale = new Vector3(-1f * _importedLocalScale.x, _importedLocalScale.y, _importedLocalScale.y);
        }
        else transform.localScale = _importedLocalScale;
        
    }
    
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Skill") && isDead == false)
        {
            /*if (obj.gameObject.transform.childCount != 0)
            {
                if (obj.gameObject.transform.Find("Skilltag").CompareTag("Storm"))
                {
                    Statistic.Instance.Dps202(CalcuDps(obj.gameObject));     
                }
                else if (obj.gameObject.transform.Find("Skilltag").CompareTag("201"))
                {
                    Statistic.Instance.Dps201(CalcuDps(obj.gameObject));
                }
                else if (obj.gameObject.transform.Find("Skilltag").CompareTag("101"))
                {
                    Statistic.Instance.Dps101(CalcuDps(obj.gameObject));
                }
                else if (obj.gameObject.transform.Find("Skilltag").CompareTag("102"))
                {
                    Statistic.Instance.Dps102(CalcuDps(obj.gameObject));
                }
                else if (obj.gameObject.transform.Find("Skilltag").CompareTag("103"))
                {
                    Statistic.Instance.Dps103(CalcuDps(obj.gameObject));
                }
                else if (obj.gameObject.transform.Find("Skilltag").CompareTag("301"))
                {
                    Statistic.Instance.Dps301(CalcuDps(obj.gameObject));
                }
                else if (obj.gameObject.transform.Find("Skilltag").CompareTag("303"))
                {
                    Statistic.Instance.Dps303(CalcuDps(obj.gameObject));
                }
                else if (obj.gameObject.transform.Find("Skilltag").CompareTag("401"))
                {
                    Statistic.Instance.Dps401(CalcuDps(obj.gameObject));
                }
                else if (obj.gameObject.transform.Find("Skilltag").CompareTag("402"))
                {
                    Statistic.Instance.Dps402(CalcuDps(obj.gameObject));
                }
            }*/
          
            GetDamaged(obj.gameObject);
            StartCoroutine(HitFeedback());
        }
    }

    public void SetMoveSpeed()
    {
        _moveSpeed = moveSpeed;
    }
    private void SwitchAnim()
    {
        _anim.SetBool("isDead", true);
    }

    private void PutObjectInPool()
    {
        EnemyObjectPool.EnemyObjectPoolInstance.PutObjectInPool(this.gameObject);
    }

    public void SetAlive()
    {
        isDead = false;
        SetMoveSpeed();
        GetComponent<Collider2D>().isTrigger = false;
    }

    public void SetDead()
    {
        ActiveSKill302();
        isDead = true;
        _moveSpeed = 0.0f;
        SwitchAnim();

        //test if the child has children
        if (monsterId == "102" || monsterId == "302")
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        GetComponent<Collider2D>().isTrigger = true;
    }

    public void RecycleMonster()
    {
        if (monsterId[0] == '3')
        {
            EventListener.Instance.SendMessage(EventListener.MessageEvent.Message_GetSkill);
        }
        if (poolBelongTo != "")
        {
            //Debug.Log("poolRecycle");
            Invoke(nameof(PutObjectInPool), 0f);
            // 从敌人探测器列表中移除该对象
            EnemyDetector.Instance.enemyList.Remove(gameObject);
            // 杀敌数增加
            Main.Instance.AddEnemyKills();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GetDamaged(GameObject damageMaker)
    {
        float totalDamage = damageMaker.GetComponent<MonoSkillBase>().damage;
        
        DamagePopupManager.Create(transform.position, (int)totalDamage);
        if (health - totalDamage > 0.0f)
        {
            health -= totalDamage;
            //Debug.Log("Monster health = " + health);
        }
        else
        {
            //Debug.Log("Attack damage = " + totalDamage);
            PlayerController.Instance.IncreaseExperience();
            SetDead();
        }
    }
    
    public void GetDamaged(float skillDamage)
    {
        DamagePopupManager.Create(transform.position, (int)skillDamage);
        if (health - skillDamage > 0.0f)
        {
            health -= skillDamage;
            //Debug.Log("Monster health = " + health);
        }
        else
        {
            //Debug.Log("Attack damage = " + totalDamage);
            PlayerController.Instance.IncreaseExperience();
            SetDead();
        }
    }
    
    public void ActiveSKill302()
    {
        //Debug.Log("技能302激活");
        GameObject.Find("SkillManager").GetComponent<Cure>().CurePlayer(GetDistance());
    }

    private float GetDistance()
    {
        //Debug.Log("获取的距离为"+ Vector3.Distance(this.transform.position, PlayerController.Instance.GetPlayerPosition()));
        return Vector3.Distance(this.transform.position, PlayerController.Instance.GetPlayerPosition());
    }
    
    // Hit Animation
    IEnumerator HitFeedback()
    {
        _material.EnableKeyword("_BEATTACK");

        yield return new WaitForSeconds(0.1f);
        
        _material.DisableKeyword("_BEATTACK");

    }

    private float CalcuDps(GameObject obj)
    {
        float damage = obj.GetComponent<MonoSkillBase>().damage;
        if (damage > health)
            return health;
        else
            return damage;
    }

}
