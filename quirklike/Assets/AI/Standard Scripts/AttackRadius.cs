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

    private void Awake() {
        Collider = GetComponent<SphereCollider>();    
    }

    private void OnTriggerEnter(Collider other) 
    {

        IDamageable damageable = other.GetComponent<IDamageable>();
        //This makes it attack only the player
        PlayerController player = other.GetComponent<PlayerController>();

        if(damageable != null)
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

        if(Damageables[0]!= null){
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
