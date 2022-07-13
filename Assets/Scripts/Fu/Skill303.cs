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
        DestroyImmediate(circleLeftGameobject, true);
        DestroyImmediate(circleRightGameobject, true);
        DestroyImmediate(circleTopGameobject, true);
        DestroyImmediate(circleBottomGameobject, true);
    }


}
