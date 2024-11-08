using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EStats : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float Health = 100;
    
    private void Awake(){
        
        
    }

    private void OnAttack(IDamageable Target)
    {
        
        Debug.Log("Enemy Attacks");
    }

    public void TakeDamage(float Damage){
        Health -= Damage;
        Debug.Log("DAMAGED????");

        CallbackPlayerHitEnemyData callbackData = new CallbackPlayerHitEnemyData(Damage, false, gameObject, 0); //this will need to be changed in the future
        Callbacks.CallEvent(CallbackEvent.PlayerHitEnemy, callbackData);

        if(Health <=0){
            Callbacks.CallEvent(CallbackEvent.EnemyKilled);
            gameObject.SetActive(false);
        }
    }

    public Transform GetTransform(){
        return transform;
    }
}
