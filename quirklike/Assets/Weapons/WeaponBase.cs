using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this is the most basic weapon class, which has very simple functionality.
//there are other weapon classes that derive from this which may be more appropriate, unless you want to make a weapon from scratch.


public enum WeaponWorldState
{
    ON_GROUND,
    IN_HAND
}

[RequireComponent(typeof(WeaponWorldHover),typeof(Animator), typeof(Rigidbody))]
[RequireComponent(typeof(WeaponPickupHandler))]
public class WeaponBase : MonoBehaviour
{
    public Transform _cameraTransform;
    protected WeaponWorldState _state;
    [SerializeField] protected Transform _firePoint; //this is the position where "bullets" should come out (if appropriate)
    protected Animator _animator;

    public event System.Action OnWeaponPickup;
    public event System.Action OnWeaponDrop;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
        _state = WeaponWorldState.ON_GROUND;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public virtual void OnInputClicked() //probably useful for sniper like weapons or semi auto
    {

    }

    public virtual void OnInputHeld()
    {

    }

    public virtual void OnInputReleased()
    {

    }


    public virtual float GetDamage()
    {
        return 0;
    }
    public virtual float GetFireRate()
    {
        return 0;
    }

    public void PickUpWeapon()
    {
        ConfirmPickup(); //later in development, PickUpWeapon() should bring up the UI to swap weapon and ConfirmPickup should do the actual swapping.
    }

    public void ConfirmPickup()
    {
        _animator.enabled = true;
        _state = WeaponWorldState.IN_HAND;

        OnWeaponPickup?.Invoke();
    }

    public void DropWeapon()
    {
        ConfirmDrop();
    }

    public void ConfirmDrop()
    {
        _animator.enabled = false;
        _state = WeaponWorldState.ON_GROUND;

        OnWeaponDrop?.Invoke();
    }
}
