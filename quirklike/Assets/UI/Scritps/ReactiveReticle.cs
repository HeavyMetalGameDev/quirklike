using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ReactiveReticle : MonoBehaviour
{
    private RectTransform ReticleTransform;

    [Range(10f, 250f)]
    public float size;
    private float currentSize;
    void Awake()
    {
        ReticleTransform = GetComponent<RectTransform>();    
    }

    public void ReticleBloom(){
        ReticleTransform.DOSizeDelta(new Vector2(size, size), 2f, false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U)){
            ReticleBloom();
        } 
    }
}
