using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class HealthBarUpdate : MonoBehaviour
{
    private float MaxHealth = 100;
    private float CurrentHealth;

    [Header("Variables")]
    [SerializeField] private float FillSpeed;
    [SerializeField] private Gradient healthGrad;
    public int CountFps = 30;
    public float Duration = 10f;

    [Header("References")]
    [SerializeField] private Image HealthBarFill;
    [SerializeField] private TMP_Text HealthTxt; 

    private Coroutine CountRoutine;
    private int _value;


    private void UpdateHText(int NewValue)
    {
        if (CountRoutine != null) {
            StopCoroutine(CountRoutine);
        }
        CountRoutine = StartCoroutine(CountHealth(NewValue));
    }


    private IEnumerator CountHealth(int NewValue)
    {
        WaitForSeconds wait = new WaitForSeconds(1/CountFps);
        int prevVal = _value;
        int stepAmt;

        if(NewValue - prevVal < 0){
            stepAmt = Mathf.FloorToInt((NewValue - prevVal) / (CountFps * Duration));
        }
        else{
            stepAmt = Mathf.CeilToInt((NewValue - prevVal) / (CountFps * Duration));
        }

        if(prevVal < NewValue)
        {
            while(prevVal < NewValue)
            {
                prevVal += stepAmt;
                
                if(prevVal > NewValue)
                {
                    prevVal = NewValue;
                }

                HealthTxt.text = prevVal.ToString("N0");

                yield return wait;
            }
        }
    }

    private void UpdateHealthBarFill(){
       
        float amt = CurrentHealth / MaxHealth;
        HealthBarFill.DOFillAmount(amt, FillSpeed);
        HealthBarFill.DOColor(healthGrad.Evaluate(amt), FillSpeed);

    }

    public void UpdateHealthUI(float curHP)
    {

        CurrentHealth = curHP;
        UpdateHealthBarFill();
        UpdateHText((int)CurrentHealth);
    }


    void Start()
    {
        UpdateHealthUI(CurrentHealth);
    }
    //Debug Stuff
    void Update()
    {
        if(Input.GetKeyDown("right")){
            CurrentHealth -= 10;
            UpdateHealthUI(30);

        }
        if(Input.GetKeyDown("left")){
            CurrentHealth +=10;
            UpdateHealthUI(CurrentHealth);
        }
    }
}
