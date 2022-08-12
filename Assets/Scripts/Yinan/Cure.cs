using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cure : FieldSkillBase
{
    public bool cure = false;
    public GameObject particle;
    public void EnableCure()
    {
        cure = true;
    }

    
    public void CurePlayer(float distance)
    {
        if (cure)
        {
            if (distance < (range+2.6))
            {
                GameObject tempParticle = Instantiate(particle, PlayerController.Instance.GetPlayerPosition(),transform.rotation);
                StartCoroutine(DestoryParticle(tempParticle));
                Debug.Log("cure suscss");
                if (PlayerController.Instance.curHealth < PlayerController.Instance.maxHealth)
                {
                    if(PlayerController.Instance.curHealth < PlayerController.Instance.maxHealth*0.15f){
                        PlayerController.Instance.curHealth += PlayerController.Instance.maxHealth * 0.03f;
                    }
                    else if (PlayerController.Instance.curHealth < PlayerController.Instance.maxHealth * 0.3f)
                    {
                        PlayerController.Instance.curHealth += PlayerController.Instance.maxHealth * 0.01f;
                    }
                    else
                    {
                        PlayerController.Instance.curHealth += PlayerController.Instance.maxHealth *0.005f;
                    }
                    
                }
                    
                //Debug.Log("cure sucs&distace=" + distance);
            }
        }
    }

    IEnumerator DestoryParticle(GameObject target)
    {
        yield return new WaitForSeconds(target.GetComponent<ParticleSystem>().duration);
        Destroy(target);
    }
}
