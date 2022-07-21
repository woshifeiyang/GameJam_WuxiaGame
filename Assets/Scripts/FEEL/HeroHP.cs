using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHP : MonoBehaviour
{

    private Material material;
    private SpriteRenderer _spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        material = _spriteRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            material.EnableKeyword("_BEATTACK");
        }
        material.DisableKeyword("_BEATTACK");
    }
    
}