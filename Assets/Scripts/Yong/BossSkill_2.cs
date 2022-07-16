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
        string assertPath = "Prefab/Enemy/Special/BossSkill/2";
        while (true)
        {
            yield return new WaitForSeconds(cd);
            //Debug.Log("spawn skill 2");
            GameObject skill = (GameObject)Instantiate(Resources.Load(assertPath));
            Vector3 tempPosition = PlayerController.Instance.GetPlayerPosition();
            skill.transform.position = new Vector3(tempPosition.x, tempPosition.y - 1f, tempPosition.z);
        }
    }

    
}
