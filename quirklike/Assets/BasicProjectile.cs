using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    [SerializeField] float _travelSpeed;
    [SerializeField] float _damage;
    DamageAlignment _alignment = DamageAlignment.NEUTRAL_SOURCE;
    Vector3 _targetPosition;

    public void SetAlignment(DamageAlignment alignment)
    {
        _alignment = alignment;
        gameObject.layer = LayerMask.NameToLayer("NeutralDamageSource") + (int)_alignment;
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public void SetTargetPosition(Vector3 target)
    {
        _targetPosition = target;
        transform.LookAt(_targetPosition);
    }

    private void Update()
    {
        UpdateProjectile();
    }

    protected virtual void UpdateProjectile() //can be overriden by other subclasses
    {
        transform.position += _travelSpeed * Time.deltaTime * transform.forward;
    }

    private void OnEnable()
    {
        //maybe call some event? not sure
    }

    private void OnDisable()
    {
       //spawn some visual effect 
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION!!!");

        if (collision.gameObject.CompareTag("Enemy"))
        {
            IDamageable enemy = collision.gameObject.GetComponent<IDamageable>();
            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
            }
        }
        gameObject.SetActive(false);
    }
}
