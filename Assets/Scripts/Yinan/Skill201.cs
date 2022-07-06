using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill201 : MultTargetSkillBase
{
    public static int doubleStrike;
    //private Rigidbody2D _rb;
    //投射物数量
    //public int amount = 4;
    // Start is called before the first frame update
    public override void Start()
    {
        doubleStrike += doubleStrike;
        //_rb = GetComponent<Rigidbody2D>();
        //_rb.transform.position = skillobj.transform.position;
        Invoke("SelfDestory", 0.3f);
    }

    // Update is called once per frame
    public override void Update()
    {
       

    }
    private void SelfDestory()
    {
        Destroy(gameObject);
    }

    //private void searchTarget()
    //{
    //    var visibleenemies = Monster.VisibleEnemies;
    //    int targetnum = Mathf.Min(visibleenemies.Count, amount);
    //    var targets = visibleenemies.GetRandomElements(targetnum);
    //    foreach (var e in targets)
    //    {
    //        _rb = GetComponent<Rigidbody2D>();
    //        _rb.transform.position = e.transform.position;
    //        Invoke("SelfDestory", skillTime);
    //    }
    //}
    
}
