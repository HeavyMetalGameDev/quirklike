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
    public float Duration = 1f;

    [Header("References")]
    [SerializeField] private Image HealthBarFill;
    [SerializeField] private TMP_Text HealthTxt; 

   
    void Start()
    {
        UpdateHealthUI(50);
    }

    public void UpdateHealthUI(float curHP)
    {

        CurrentHealth = curHP;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);
        UpdateHealthBarFill();
        UpdateHealthTxt();
    }

    private void UpdateHealthBarFill(){
       
        float amt = CurrentHealth / MaxHealth;
        HealthBarFill.DOFillAmount(amt, FillSpeed);
        HealthBarFill.DOColor(healthGrad.Evaluate(amt), FillSpeed);

    }
    private void UpdateHealthTxt(){
        HealthTxt.text = "" +CurrentHealth + "%";
    }

    void Update()
    {
        if(Input.GetKeyDown("right")){
            CurrentHealth += 10;
            UpdateHealthUI(CurrentHealth);

        }
        if(Input.GetKeyDown("left")){
            CurrentHealth -=10;
            UpdateHealthUI(CurrentHealth);
        }
    }
}
