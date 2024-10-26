using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ReactiveReticle : MonoBehaviour
{
    private RectTransform ReticleTransform;

    [Range(10f, 250f)]
    public float BloomedSize = 25f;
    public float DefaultSize = 10f;
    bool blooming;
    public float BloomCooldown = 0.3f;
    private float cooldown;
    void Awake()
    {
        cooldown = BloomCooldown;
        ReticleTransform = GetComponent<RectTransform>();  
        ReticleTransform.sizeDelta = new Vector2(DefaultSize, DefaultSize);  
    }

    public void ReticleBloom(){
        ReticleTransform.DOSizeDelta(new Vector2(BloomedSize, BloomedSize), 0.1f, false);
        blooming = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            if(!blooming){
                ReticleBloom();
            } 
        } 
        if(blooming){
            cooldown -= Time.deltaTime;
        }

        if(cooldown < 0 && blooming){
            cooldown = BloomCooldown;
            blooming = false;
            ReticleTransform.DOSizeDelta (new Vector2(DefaultSize, DefaultSize), 0.1f, false);  
        }
    }
}
