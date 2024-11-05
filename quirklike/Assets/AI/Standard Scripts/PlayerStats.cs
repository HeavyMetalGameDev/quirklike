using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    
    [SerializeField]
    private float maxHealth = 100;
    private float currentHealth = 100;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float Damage)
    {
        currentHealth -= Damage;
        CallbackFloat callbackNewHealth = new CallbackFloat(currentHealth);
        Callbacks.CallEvent(CallbackEvent.PlayerHurt, callbackNewHealth);

        if(currentHealth <=0){
            Debug.Log("Player Is Dead");
            Callbacks.CallEvent(CallbackEvent.PlayerKilled);
        }
    }

    public Transform GetTransform(){
        return transform;
    }
}
