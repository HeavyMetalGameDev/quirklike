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
    [SerializeField] private float BarFillSpeed =0.5f;
    [SerializeField] private Gradient BarGradient;
    public int TextCountFPS = 30;
    public float TextDuration = 10f;

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
        WaitForSeconds wait = new WaitForSeconds(1/TextCountFPS);
        int prevVal = _value;
        int stepAmt;

        if(NewValue - prevVal < 0){
            stepAmt = Mathf.FloorToInt((NewValue - prevVal) / (TextCountFPS * TextDuration));
        }
        else{
            stepAmt = Mathf.CeilToInt((NewValue - prevVal) / (TextCountFPS * TextDuration));
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

                HealthTxt.text = prevVal.ToString("N0") + "%";

                yield return wait;
            }
        }
    }

    private void UpdateHealthBarFill(){
       
        float amt = CurrentHealth / MaxHealth;
        HealthBarFill.DOFillAmount(amt, BarFillSpeed);
        HealthBarFill.DOColor(BarGradient.Evaluate(amt), BarFillSpeed);

    }

    public void UpdateHealthUI(float curHP)
    {

        CurrentHealth = curHP;
        if(CurrentHealth > MaxHealth){
            CurrentHealth = MaxHealth;
            return;
        }
        UpdateHealthBarFill();
        UpdateHText((int)CurrentHealth);
    }


    void Start()
    {
        CurrentHealth = MaxHealth;
        UpdateHealthUI(CurrentHealth);
    }
    
    //Debug Stuff
    void Update()
    {
        if(Input.GetKeyDown("left")){
            CurrentHealth -= 10;
            UpdateHealthUI(CurrentHealth);

        }
        if(Input.GetKeyDown("right")){
            CurrentHealth +=10;
            UpdateHealthUI(CurrentHealth);
        }
    }
}
