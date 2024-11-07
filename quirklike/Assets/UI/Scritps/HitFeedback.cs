using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HitFeedback : MonoBehaviour
{
    public Image[] HitFeedbackLines;

    [SerializeField] private float FadeTime     = 0.2f;
    [SerializeField] private Color DefaultColor = new Color(0,0,0,0);
    [SerializeField] private Color HitColor     = new Color(0,0,0,1);
    [SerializeField] private float cooldownTime = 0.1f;
    private bool feedbackViz;
    private float cooldown;

    private void Awake()
    {
        foreach(Image H in HitFeedbackLines)
        {
            H.color = DefaultColor;
        }
        cooldown = cooldownTime;
    }

    private void ToggleHitFeedback(bool viz)
    {
        if(viz)
        {
            foreach(Image hit in HitFeedbackLines){
                hit.DOColor(HitColor, FadeTime);
            }
        } else {
            foreach(Image hit in HitFeedbackLines){
                hit.DOColor(DefaultColor, FadeTime);
            }
        }
    }
    
    public void SetHitFeedback(bool hit){
        feedbackViz = hit;
        ToggleHitFeedback(hit);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("hit-feedback");
            SetHitFeedback(true);
        }

        if(feedbackViz)
        {
            cooldown -= Time.deltaTime;
        }

        if(cooldown< 0 && feedbackViz)
        {
            cooldown = cooldownTime;
            feedbackViz = false;
            ToggleHitFeedback(feedbackViz);
        }
    }

    void OnPlayerHitEnemy(float damage, bool isCritical, GameObject enemyHit, int playerID)
    {
        //this should probably be more complicated.
        SetHitFeedback(true);
    }

    private void OnEnable()
    {
        Callbacks.PlayerHitEnemy += OnPlayerHitEnemy;
    }

    private void OnDisable()
    {
        Callbacks.PlayerHitEnemy -= OnPlayerHitEnemy;
    }
}
