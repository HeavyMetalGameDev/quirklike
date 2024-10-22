using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EStats : MonoBehaviour, IDamageable
{
    [SerializeField]
    private AttackRadius AttackRadius;

    [SerializeField]
    private int Health = 100;
    private void Awake(){
        AttackRadius.OnAttack += OnAttack;
    }

    private void OnAttack(IDamageable Target)
    {
        //animator = true etc etc
        Debug.Log("Enemy Attacks");
    }

    public void TakeDamage(int Damage){
        Health -= Damage;

        if(Health <=0){
            gameObject.SetActive(false);
        }
    }

    public Transform GetTransform(){
        return transform;
    }
}
