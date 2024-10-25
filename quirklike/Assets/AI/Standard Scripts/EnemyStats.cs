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
        if(StartingHP <= 0 ){
            TotalHP = 100;
            Debug.LogWarning("Some Enemy's starting HP is missing or 0. Setting it to default value: " + this);
        }
        CurrentHP = StartingHP;
    }

    public void DoDamage(float incDamage){

        CurrentHP -= incDamage;

        if (CurrentHP < 0)
        {
            CurrentHP = 0;
            Callbacks.CallEvent(CallbackEvent.EnemyKilled);
            //enemy has died, do something here
            //for now we will just disable the enemy
            gameObject.SetActive(false);
        }
        Debug.Log(CurrentHP);
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
