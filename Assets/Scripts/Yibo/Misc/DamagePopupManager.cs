using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamagePopupManager : MonoBehaviour
{

    public DamagePopup damagePopupPrefab;
    // Start is called before the first frame update
    public void Create(Vector3 position, int damageAmount)
    {
        DamagePopup DMP = Instantiate(damagePopupPrefab, position, Quaternion.identity);
        DMP.Setup(damageAmount);
    }
    
}
