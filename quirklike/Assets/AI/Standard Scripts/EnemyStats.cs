using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pretty naive implementation which maybe will cause problems in 
//the future but do i care? yes 
//but it is a problem for future me to worry about

public class EnemyStats : MonoBehaviour, IDamageable 
{
    [Tooltip("Health the enemy starts with. Default 100")]
    [SerializeField] public float StartingHP;
    private float TotalHP;
    [SerializeField]
    private float CurrentHP;
    [SerializeField]  
    private AttackRadius AttackRadius;

    private void Awake() {
        TotalHP = 100;


        AttackRadius.OnAttack += OnAttack;
    }
    
    private void OnAttack(IDamageable Target) 
    {
        Debug.Log("Enemy Attacks"); 
    }

    public void DoDamage(float incDamage){

        if(CurrentHP - incDamage < 0){
            CurrentHP = 0;
        }
        CurrentHP -= incDamage;
    }

    public void TakeDamage(int incDamage)
    {
        if(TotalHP - incDamage < 0){
            TotalHP = 0;
        }
        TotalHP -= incDamage;
    }
    
    public Transform GetTransform(){
        return transform;
    }
}
