using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class AttackHitBox : MonoBehaviour
{
    private CapsuleCollider WeaponHitbox;
    void Awake()
    {
        WeaponHitbox = GetComponent<CapsuleCollider>();
    }


    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("HIT");
        IDamageable damageable = other.GetComponent<IDamageable>();
        PlayerController player = other.GetComponent<PlayerController>();

        if(player != null && damageable != null)
        {
            damageable.TakeDamage(10);
        }
    }
}
