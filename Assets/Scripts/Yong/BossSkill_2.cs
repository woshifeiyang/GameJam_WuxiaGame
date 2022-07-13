using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill_2 : MonoBehaviour
{
    public float cd;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ScopeSkill());
    }

    // Update is called once per frame
    IEnumerator ScopeSkill()
    {
        string assertPath = "Prefab/Enemy/Boss/2";
        while (true)
        {
            yield return new WaitForSeconds(cd);
            GameObject skill = (GameObject)Instantiate(Resources.Load(assertPath));
            skill.transform.position = PlayerController.Instance.GetPlayerPosition();
            
        }
    }
}
