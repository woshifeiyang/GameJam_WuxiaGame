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

    [Header ("Prefab Place")]
    [SerializeField]
    private GameObject circleLeft;
    [SerializeField]
    private GameObject circleRight;
    [SerializeField]
    private GameObject circleTop;
    [SerializeField]
    private GameObject circleBottom;

    private Vector3 playerPosition; 
    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> circleList = new List<GameObject>() {circleLeft, circleRight, circleTop, circleBottom};
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = PlayerController.Instance.GetPlayerPosition();
        skill303Timer += Time.deltaTime;
        if (skill303Timer > cd)
        {
            skill303Timer = 0;
            Instantiate(circleLeft, new Vector2((playerPosition.x - skillRadius), playerPosition.y), Quaternion.identity);
            circleLeft.GetComponent<CircleCollider2D>().radius = range;
            Instantiate(circleRight, new Vector2((playerPosition.x + skillRadius), playerPosition.y), Quaternion.identity);
            circleRight.GetComponent<CircleCollider2D>().radius = range;
            Instantiate(circleTop, new Vector2(playerPosition.x, (playerPosition.y + skillRadius)), Quaternion.identity);
            circleTop.GetComponent<CircleCollider2D>().radius = range;
            Instantiate(circleTop, new Vector2(playerPosition.x, (playerPosition.y - skillRadius)), Quaternion.identity);
            circleBottom.GetComponent<CircleCollider2D>().radius = range;
            Destroy(circleLeft);
            Destroy(circleRight);
            Destroy(circleTop);
            Destroy(circleBottom);
        }

    }
}
