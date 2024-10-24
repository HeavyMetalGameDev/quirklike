using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pretty naive implementation which maybe will cause problems in 
//the future but do i care? yes 
//but it is a problem for future me to worry about

public class EnemyStats : MonoBehaviour {
    [Tooltip("Health the enemy starts with. Default 100")]
    [SerializeField] public float StartingHP;
    private float TotalHP;
    private float CurrentHP;  

    private void Awake() {
        TotalHP = StartingHP;

        if(StartingHP <= 0 ){
            TotalHP = 100;
            Debug.LogWarning("Some Enemy's starting HP is missing or 0. Setting it to default value: " + this);
        }
    }

    //DoDamager: simply minuses given damage from total health
    public void DoDamage(float incDamage){

        if(CurrentHP - incDamage < 0){
            CurrentHP = 0;
        }
        CurrentHP -= incDamage;
    }


    
}
