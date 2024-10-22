using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    
    [SerializeField]
    private int Health = 100;
    private void Awake()
    {
        
    }

    private void OnAttack(IDamageable Target)
    {
        Debug.Log("Player Attacks" ); 
    }

    public void TakeDamage(int Damage){
        Health -= Damage;

        if(Health <=0){
            Debug.LogError("Player Is Dead");
            // throw new System.NotImplementedException();
        }
    }

    public Transform GetTransform(){
        return transform;
    }
}
