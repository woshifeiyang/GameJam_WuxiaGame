using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{

    public GameObject offspring;
    
    private TextMeshPro TMP;

    private void Awake()
    {
        TMP = offspring.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount)
    {
        TMP.SetText(damageAmount.ToString());
        StartCoroutine(DamagePopupDestroy(1f));
    }
    
    public IEnumerator DamagePopupDestroy(float Time)
    {
        yield return new WaitForSeconds(Time);
        Destroy(gameObject);
    }
}
