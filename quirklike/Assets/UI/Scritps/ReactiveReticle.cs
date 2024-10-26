using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactiveReticle : MonoBehaviour
{
    private RectTransform ReticleTransform;

    [Range(50f, 50f)]
    public float size;
    void Awake()
    {
        ReticleTransform = GetComponent<RectTransform>();    
    }
    void Update()
    {
        ReticleTransform.sizeDelta = new Vector2(size, size); 
    }
}
