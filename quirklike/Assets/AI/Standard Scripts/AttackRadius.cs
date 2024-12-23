using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class AttackRadius : MonoBehaviour
{
    public SphereCollider Collider;
    public int Damage = 10;
    public float AttackDelay = 0.5f;
    public delegate void AttackEvent(IDamageable Target);
    private List<IDamageable> Damageables = new List<IDamageable>();
    public AttackEvent OnAttack;
    private Coroutine AttackCoroutine;

    public bool attacking = true;

    private void Awake() {
        Collider = GetComponent<SphereCollider>();    
    }

    private void OnTriggerEnter(Collider other) 
    {

        IDamageable damageable = other.GetComponent<IDamageable>();
        PlayerController player = other.GetComponent<PlayerController>();

        if(damageable != null && player != null && attacking)
        {
            Damageables.Add(damageable);

            if(AttackCoroutine==null){
                AttackCoroutine = StartCoroutine(Attack());
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        
        IDamageable damageable = other.GetComponent<IDamageable>();

        if(damageable!=null)
        {
            Damageables.Remove(damageable);

            if(Damageables.Count == 0 && AttackCoroutine!=null)
            {
                StopCoroutine(AttackCoroutine);
                AttackCoroutine = null;
            }
        }
    }

    private IEnumerator Attack()
    {
        
        WaitForSeconds Wait = new WaitForSeconds(AttackDelay);
        yield return Wait;

        float dist = Vector3.Distance(transform.position, Damageables[0].GetTransform().position);

        if(Damageables[0]!= null && dist < 6.0f){
            OnAttack?.Invoke(Damageables[0]);
            Damageables[0].TakeDamage(Damage);

            yield return Wait;
            Damageables.RemoveAll(DisabledDamageables);
        }
        
        AttackCoroutine = null;
        
    }

    private bool DisabledDamageables(IDamageable damageable)
    {
        return damageable !=null && !damageable.GetTransform().gameObject.activeSelf;
    }
}
