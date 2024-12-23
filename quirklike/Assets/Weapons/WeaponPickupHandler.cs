using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script should go on weapons to seperate the functionality of picking up weapons from the main weapon script.
//it does NOT manage the ANimator component as the main weapon script will need to access this.

public class WeaponPickupHandler : MonoBehaviour
{
    static float WEAPON_DROP_VELOCITY = 10.0f;
    [SerializeField] GameObject _worldHitboxObject;
    WeaponBase _weapon;
    Rigidbody _weaponRigidbody;
    Collider _weaponCollider;
    WeaponWorldHover _hoverComponent;


    public void SetWeapon(WeaponBase weaponToSet)
    {
        _weapon = weaponToSet;
    }

    private void Awake()
    {
        _weapon = GetComponent<WeaponBase>();
        _hoverComponent = GetComponent<WeaponWorldHover>();
        _weaponRigidbody = GetComponent<Rigidbody>();
        _weaponCollider = GetComponentInChildren<Collider>();
        _hoverComponent.enabled = true;

    }

    private void OnEnable()
    {
        _weapon.OnWeaponPickup += OnWeaponPickup;
        _weapon.OnWeaponDrop += OnWeaponDrop;
    }

    private void OnDisable()
    {
        _weapon.OnWeaponPickup -= OnWeaponPickup;
        _weapon.OnWeaponDrop -= OnWeaponDrop;
    }

    private void OnWeaponPickup()
    {
        _hoverComponent.enabled = false;
        _weaponRigidbody.isKinematic = true;
        _weaponCollider.enabled = false;
        _worldHitboxObject.SetActive(false);
    }

    private void OnWeaponDrop()
    {
        _hoverComponent.enabled = true;
        _weaponRigidbody.isKinematic = false;
        _weaponCollider.enabled = true;
        transform.localRotation = Quaternion.identity;
        _worldHitboxObject.SetActive(true);

        _weaponRigidbody.velocity += _weapon._cameraTransform.forward * WEAPON_DROP_VELOCITY;
    }


}
