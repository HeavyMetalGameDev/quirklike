using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    
    [SerializeField]
    private float Health = 100;
    [SerializeField]
    GameObject enemy;
    
    //very unsafe way of doing this yet a way of doing it nonetheless
    private void Update() 
    {

        if(Input.GetKeyDown(KeyCode.H)){
            Debug.Log("testing");
            IDamageable E = enemy.GetComponent<IDamageable>();
            E.TakeDamage(10);
        }
    }

    private void OnAttack(IDamageable Target)
    {
        Debug.Log("Player Attacks" ); 
    }

    public void TakeDamage(float Damage)
    {
        Health -= Damage;

        if(Health <=0){
            Debug.Log("Player Is Dead");
            // throw new System.NotImplementedException();
        }
    }

    public Transform GetTransform(){
        return transform;
    }
}
