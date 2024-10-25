using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EStats : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int Health = 100;
    private void Awake(){
        
        
    }

    private void OnAttack(IDamageable Target)
    {
        
        Debug.Log("Enemy Attacks");
    }

    public void TakeDamage(int Damage){
        Health -= Damage;
        Debug.Log("DAMAGED????");
        if(Health <=0){
            gameObject.SetActive(false);
        }
    }

    public Transform GetTransform(){
        return transform;
    }
}
