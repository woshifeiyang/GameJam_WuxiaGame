using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill303 : FieldSkillBase
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float skillRadius;
    private float skill303Timer;
    [SerializeField]
    private float skill303LastSeconds;

    [Header("Prefab Place")]
    [SerializeField]
    private GameObject circleLeft;
    [SerializeField]
    private GameObject circleRight;
    [SerializeField]
    private GameObject circleTop;
    [SerializeField]
    private GameObject circleBottom;

    private GameObject circleLeftGameobject;
    private GameObject circleRightGameobject;
    private GameObject circleTopGameobject;
    private GameObject circleBottomGameobject;


    private Vector3 playerPosition;
    // Start is called before the first frame update
    public override void Start()
    {
        List<GameObject> circleList = new List<GameObject>() { circleLeft, circleRight, circleTop, circleBottom };
    }

    // Update is called once per frame
    public override void Update()
    {
        playerPosition = PlayerController.Instance.GetPlayerPosition();
        //if (PlayerController.Instance.curHealth < 0.1 * PlayerController.Instance.GetPlayerHealthFinal())
        //{
        //    cd *= 0.3f;
        //    Debug.Log("0.1mode");
        //}
        //else if(PlayerController.Instance.curHealth < 0.25 * PlayerController.Instance.GetPlayerHealthFinal())
        //{
        //    cd *= 0.5f;
        //    Debug.Log("0.25mode");
        //}
        //else if(PlayerController.Instance.curHealth < 0.5 * PlayerController.Instance.GetPlayerHealthFinal())
        //{
        //    cd *= 0.7f;
        //    Debug.Log("0.5mode");
        //}
        skill303Timer += Time.deltaTime;
        if (skill303Timer > cd)
        {
            
            skill303Timer = 0;
            circleLeftGameobject = Instantiate(circleLeft, new Vector2((playerPosition.x - skillRadius), playerPosition.y), Quaternion.identity).gameObject;
            circleLeft.GetComponent<CircleCollider2D>().radius = 1;
            circleRightGameobject = Instantiate(circleRight, new Vector2((playerPosition.x + skillRadius), playerPosition.y), Quaternion.identity);
            circleRight.GetComponent<CircleCollider2D>().radius = 1;
            circleTopGameobject = Instantiate(circleTop, new Vector2(playerPosition.x, (playerPosition.y + skillRadius)), Quaternion.identity);
            circleTop.GetComponent<CircleCollider2D>().radius = 1;
            circleBottomGameobject = Instantiate(circleTop, new Vector2(playerPosition.x, (playerPosition.y - skillRadius)), Quaternion.identity);
            circleBottom.GetComponent<CircleCollider2D>().radius = 1;
            //StartCoroutine("SkillLastSeconds");
            Invoke(nameof(SkillLastSeconds), skill303LastSeconds);
        }

    }

    private void SkillLastSeconds()
    {
        Destroy(circleLeftGameobject);
        Destroy(circleRightGameobject);
        Destroy(circleTopGameobject);
        Destroy(circleBottomGameobject);
    }


}
