using UnityEngine;
using UnityEngine.UI;

public class HealthBarUpdate : MonoBehaviour
{
    private float MaxHealth = 100;
    private float CurrentHealth;
    [SerializeField] private Image HealthBarFill;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = 50;
        UpdateHealthBarFill();
    }

    private void UpdateHealthBarFill(){
        float amt = CurrentHealth / MaxHealth;
        HealthBarFill.fillAmount = amt;
    }

    void Update()
    {
        if(Input.GetKeyDown("right")){
            CurrentHealth += 10;
            UpdateHealthBarFill();
        }
        if(Input.GetKeyDown("left")){
            UpdateHealthBarFill();
            CurrentHealth -= 10;
        }
    }
}
