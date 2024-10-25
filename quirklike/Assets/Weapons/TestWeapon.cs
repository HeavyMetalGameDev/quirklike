using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : WeaponBase
{
    [SerializeField] float damage;
    [SerializeField] float fireRate;
    public override void OnInputClicked()
    {
        Debug.Log("TEST WEAPON CLICKED!!!");
    }

    public override void OnInputHeld()
    {
        Debug.Log("TEST WEAPON HELD!!!");

    }

    public override void OnInputReleased()
    {
        Debug.Log("TEST WEAPON RELEASED!!!");

    }
    public override float GetDamage()
    {
        return damage;
    }

    public override float GetFireRate()
    {
        return fireRate;
    }
}
